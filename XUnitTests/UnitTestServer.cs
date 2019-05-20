using System;
using Xunit;
using StoicDreams.FileProxy;
using StoicDreams.FileProxy.Interface;
using System.Collections.Generic;
using StoicDreams.FileProxy.Routing;

namespace XUnitTests
{
	public class UnitTestServer
	{
		[Fact]
		public void TestInit()
		{
			IRoute[] routes = new IRoute[0];
			IServer server = new Server(routes);
		}
		[Theory]
		[InlineData("/A/test.png")]
		[InlineData("/A/test.json")]
		[InlineData("/a/test.json")]
		public void TestRequestMatchesRouting(string clientRequest)
		{
			IRoute[] routes = new IRoute[2]
			{
				new FolderRoute(){RequestedPath = "/a/", RoutedPath = "/b/"}
				, new FileRoute(){RequestedPath = "/a/test.png", RoutedPath = "/b/Logo.png"}
			};
			IServer server = new Server(routes);
			Assert.True(server.RequestMatchesRouting(clientRequest, out IRoute route));
		}
	}
}
