using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using StoicDreams.FileProxy.Filter;

namespace StoicDreams.FileProxy.Routing
{
	/// <summary>
	/// Shared routing features
	/// </summary>
	internal static class Common
	{
		internal static string WebRootBase { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
		internal static bool RouteIsRemote(string route)
		{
			if (Matches.IsProtocolFormat(route)) { return true; }
			return false;
		}
		internal static Task<FileData> GetLocalFile(string relativePath)
		{
			if (!CheckLocalWebRootFileExists(relativePath, out string filepath)
				&& !CheckLocalProjectFileExists(relativePath, out filepath)
				)
			{
				return Task.FromResult(new FileData()
				{
					ContentType = "text/plain",
					Data = Encoding.UTF8.GetBytes("Content not found"),
					StatusCode = System.Net.HttpStatusCode.NotFound
				});
			}
			return Task.FromResult(new FileData()
			{
				Data = File.ReadAllBytes(filepath)
				,
				StatusCode = System.Net.HttpStatusCode.OK
			});
		}
		private static bool CheckLocalProjectFileExists(string relativePath, out string filePath)
		{
			filePath = $"{Path.GetFullPath(".")}{relativePath}";
			if (File.Exists(filePath))
			{
				return true;
			}
			return false;
		}
		private static bool CheckLocalWebRootFileExists(string relativePath, out string filePath)
		{
			filePath = $"{WebRootBase}{relativePath}";
			if (File.Exists(filePath))
			{
				return true;
			}
			return false;
		}
		internal static async Task<FileData> GetRemoteFile(string urlPath, IDictionary<string, object> headers)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					if (headers != null)
					{
						foreach (string key in headers.Keys)
						{
							client.DefaultRequestHeaders.Add(key, headers[key].ToString());
						}
					}
					HttpResponseMessage response = await client.GetAsync(urlPath);
					return new FileData()
					{
						Data = await response.Content.ReadAsByteArrayAsync()
						,
						ContentType = response.Content.Headers.ContentType.ToString()
						,
						StatusCode = response.StatusCode
					};
				}
			}
			catch { }
			return default;
		}
		public static void ValidateSetup(string requestedPath, string routedPath, string callingClass)
		{
			if (string.IsNullOrWhiteSpace(requestedPath))
			{
				throw new Exception($"{callingClass}.RequestedPath cannot be empty or null.");
			}
			if (string.IsNullOrWhiteSpace(routedPath))
			{
				throw new Exception($"{callingClass}.RoutedPath cannot be empty or null.");
			}
			if (requestedPath.ToLower() == routedPath.ToLower())
			{
				throw new Exception($"{callingClass}: RoutedPath and RequestedPath cannot be equal.");
			}
			if (requestedPath != requestedPath.FilterURLToRoutePath())
			{
				throw new Exception($"{callingClass}: Invalid RequestedPath.");
			}
		}
	}
}
