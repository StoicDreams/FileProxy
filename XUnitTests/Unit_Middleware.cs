using Xunit;
using Microsoft.AspNetCore.Http;
using StoicDreams.Middleware;
using System.Threading.Tasks;
using StoicDreams.FileProxy.Interface;
using StoicDreams.FileProxy.Routing;

namespace XUnitTests
{
	public class Unit_Middleware
	{
		[Fact]
		public void VerifyMiddleware()
		{
			var context = new DefaultHttpContext();
			context.Request.Path = "/";
			var middleware = new FileProxyMiddleware(next: (context) => Task.FromResult(0), options: new FileProxyOptions()
			{
				Routes = new IRoute[] 
				{
					new FileRoute("/", "/routed")
				}
			});
			middleware.InvokeAsync(context).GetAwaiter().GetResult();
			Assert.Equal("text/plain", context.Response.ContentType);
			Assert.Equal(404, context.Response.StatusCode);
		}
	}
}
