using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using StoicDreams.FileProxy.Routing;

namespace StoicDreams.Middleware
{
	public class FileProxyMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly FileProxy.Service service;

		public FileProxyMiddleware(RequestDelegate next, IFileProxyOptions options)
		{
			Common.WebRootBase = options.WebRoot ?? AppDomain.CurrentDomain.BaseDirectory;
			_next = next;
			if (options?.Routes != null)
			{
				service = FileProxy.Service.StandardService(options.Routes);
			}
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			var (IsMatched, fileData) = await service.HandleProxyIfMatchedAsync(httpContext.Request.Path.Value);
			if (IsMatched)
			{
				httpContext.Response.ContentType = fileData.ContentType;
				await httpContext.Response.Body.WriteAsync(fileData.Data, 0, fileData.Data.Length);
				httpContext.Response.StatusCode = (int)fileData.StatusCode;
				return;
			}

			await _next(httpContext);
		}
	}

	public interface IFileProxyOptions
	{
		FileProxy.Interface.IRoute[] Routes { get; set; }
		string WebRoot { get; set; }
	}

	public class FileProxyOptions : IFileProxyOptions
	{
		public FileProxy.Interface.IRoute[] Routes { get; set; }
		public string WebRoot { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class FileProxyMiddlewareExtensions
	{
		public static IApplicationBuilder UseFileProxy(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<FileProxyMiddleware>();
		}

		public static void AddFileProxyOptions(this IServiceCollection services, Action<IFileProxyOptions> setupOptions)
		{
			IFileProxyOptions options = new FileProxyOptions();
			setupOptions(options);
			services.AddSingleton(options);
		}
	}
}
