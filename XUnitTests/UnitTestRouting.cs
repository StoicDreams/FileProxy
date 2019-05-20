using System;
using System.Threading.Tasks;
using StoicDreams.FileProxy;
using StoicDreams.FileProxy.Interface;
using StoicDreams.FileProxy.Routing;
using Xunit;

namespace XUnitTests
{
	public class UnitTestRouting
	{
		[Fact]
		public void TestRoutingInit()
		{
			IRoute fileRoute = new FileRoute();
			IRoute folderRoute = new FolderRoute();
		}
		[Theory]
		[InlineData("/a/test.json", "/b/test.json")]
		[InlineData("/a/test.json?result=1", "/b/test.json?result=1")]
		public async Task TestFolderRouting(string incomingPath, string expectedRoute)
		{
			IRoute folderRoute = new FolderRoute()
			{
				RequestedPath = "/a/",
				RoutedPath = "/b/"
			};
			Assert.True(folderRoute.RequestMatchesPath(incomingPath));
			Assert.Equal(expectedRoute, folderRoute.TranslateRequestToRoutedPath(incomingPath));
		}
		[Theory]
		[InlineData("/a/test.json", "/b/test.json")]
		[InlineData("/a/test.json?result=1", "/b/test.json?result=1")]
		public async Task TestFileRouting(string incomingPath, string expectedRoute)
		{
			IRoute fileRoute = new FileRoute()
			{
				RequestedPath = "/a/test.json",
				RoutedPath = "/b/test.json"
			};
			Assert.True(fileRoute.RequestMatchesPath(incomingPath));
			Assert.Equal(expectedRoute, fileRoute.TranslateRequestToRoutedPath(incomingPath));
		}
		[Theory]
		[InlineData("", "")]
		[InlineData("/a/", "")]
		[InlineData("/a/", "/a/")]
		[InlineData("/A/", "/a/")]
		[InlineData("http://bad.com/a/", "/a/")]
		public void TestBadFolderRouting(string requestedPath, string routedPath)
		{
			Assert.Throws<Exception>(() => {
				IRoute route = new FolderRoute()
				{
					RequestedPath = requestedPath,
					RoutedPath = routedPath
				};
				route.ValidateSetup();
			});
		}
		[Theory]
		[InlineData("", "")]
		[InlineData("/a/", "")]
		[InlineData("/a/", "/a/")]
		[InlineData("/A/", "/a/")]
		[InlineData("http://bad.com/a/", "/a/")]
		public void TestBadPageRouting(string requestedPath, string routedPath)
		{
			Assert.Throws<Exception>(() => {
				IRoute route = new FileRoute()
				{
					RequestedPath = requestedPath,
					RoutedPath = routedPath
				};
				route.ValidateSetup();
			});
		}
	}
}
