using StoicDreams.FileProxy;
using StoicDreams.FileProxy.Interface;
using StoicDreams.FileProxy.Routing;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTests
{
	public class Unit_Service
	{
		[Fact]
		public async Task TestLocalFileProxy()
		{
			IService service = Service.StandardService(new IRoute[2]
				{
					new FileRoute("/a/test", "/b/test.json")
					, new FolderRoute("/a/", "/b/")
				});
			var result = await service.HandleProxyIfMatchedAsync("/a/test");
			Assert.True(result.IsMatched);
			byte[] buffer = result.fileData.Data;
			Assert.Equal(@"{""test"": ""b""}", Encoding.UTF8.GetString(buffer, 0, buffer.Length).Replace("\r\n", ""));
			var (IsMatched, fileData) = await service.HandleProxyIfMatchedAsync("/a/logo.png");
			Assert.True(IsMatched);
			Assert.Equal(8386, fileData.Data.Length);
		}
		[Fact]
		public async Task TestRemoteFileProxyPng()
		{
			IService service = Service.StandardService(new IRoute[1] {
				new FileRoute("/a/test.png", "https://www.myfi.ws/img/sd/icon-48x48.png")
			});
			var (IsMatched, fileData) = await service.HandleProxyIfMatchedAsync("/a/test.png");
			Assert.True(IsMatched);
			Assert.Equal(3141, fileData.Data.Length);
		}
		[Fact]
		public async Task TestRemoteFolderProxyPng()
		{
			IService service = Service.StandardService(new IRoute[1] {
				new FolderRoute("/a", "https://www.myfi.ws/img/sd")
			});
			var (IsMatched, fileData) = await service.HandleProxyIfMatchedAsync("/a/icon-48x48.png");
			Assert.True(IsMatched);
			Assert.Equal(3141, fileData.Data.Length);
		}
		[Fact]
		public async Task TestRemoteFileProxyIco()
		{
			IService service = Service.StandardService(new IRoute[1] {
				new FileRoute("/a/test.ico", "https://www.myfi.ws/favicon.ico", new System.Collections.Generic.Dictionary<string, object>(){
					{"x-test", "test" }
				})
			});
			var (IsMatched, fileData) = await service.HandleProxyIfMatchedAsync("/a/test.ico");
			Assert.True(IsMatched);
			Assert.Equal(894, fileData.Data.Length);
		}
	}
}
