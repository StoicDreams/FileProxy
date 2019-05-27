using System;
using Xunit;
using StoicDreams.FileProxy;
using StoicDreams.FileProxy.Interface;
using StoicDreams.FileProxy.Routing;

namespace XUnitTests
{
	public class UnitTestServer
	{
		[Fact]
		public void TestInit()
		{
			IRoute[] routes = new IRoute[0];
			IServerConfig config = new ServerConfig() {
				Routes = routes
			};
			IServer server = new Server(config);
		}
		[Theory]
		[InlineData("/A/test.png")]
		[InlineData("/A/test.json")]
		[InlineData("/a/test.json")]
		public void TestRequestMatchesRouting(string clientRequest)
		{
			IRoute[] routes = new IRoute[2]
			{
				new FolderRoute("/a/", "/b/")
				, new FileRoute("/a/test.png", "/b/Logo.png")
			};
			IServerConfig config = new ServerConfig()
			{
				Routes = routes
			};
			IServer server = new Server(config);
			Assert.True(server.RequestMatchesRouting(clientRequest, out IRoute route));
		}
		[Theory]
		[InlineData("/a/", "/a/")]
		[InlineData("/A/", "/a/")]
		public void TestDuplicateKeys(string keyA, string keyB)
		{
			Assert.Throws<Exception>(() => {
				IRoute[] routes = new IRoute[2]
				{
				new FolderRoute(keyA, "/b/")
				, new FileRoute(keyB, "/b/Logo.png")
				};
				IServerConfig config = new ServerConfig()
				{
					Routes = routes
				};
				IServer server = new Server(config);
			});
		}
	}
}
