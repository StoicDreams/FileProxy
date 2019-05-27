using System;
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
			_ = new FileRoute("/a/test", "/b/test");
			_ = new FolderRoute("/a/", "/b/");
		}
		[Theory]
		[InlineData("/a/test.json", "/b/test.json")]
		[InlineData("/a/test.json?result=1", "/b/test.json?result=1")]
		public void TestFolderRouting(string incomingPath, string expectedRoute)
		{
			IRoute folderRoute = new FolderRoute("/a/", "/b/");
			Assert.True(folderRoute.RequestMatchesPath(incomingPath));
			Assert.Equal(expectedRoute, folderRoute.TranslateRequestToRoutedPath(incomingPath));
		}
		[Theory]
		[InlineData("/a/test.json", "/b/test.json")]
		[InlineData("/a/test.json?result=1", "/b/test.json?result=1")]
		public void TestFileRouting(string incomingPath, string expectedRoute)
		{
			IRoute fileRoute = new FileRoute("/a/test.json", "/b/test.json");
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
				IRoute route = new FolderRoute(requestedPath, routedPath);
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
				IRoute route = new FileRoute(requestedPath, routedPath);
				route.ValidateSetup();
			});
		}
	}
}
