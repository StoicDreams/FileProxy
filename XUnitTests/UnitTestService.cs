using StoicDreams.FileProxy;
using StoicDreams.FileProxy.Interface;
using StoicDreams.FileProxy.Routing;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTests
{
	public class UnitTestService
	{
		[Fact]
		public async Task TestLocalFileProxy()
		{
			IService service = Service.StandardService(new IRoute[2]
				{
					new FileRoute("/a/test", "/b/test.json")
					, new FolderRoute("/a/", "/b/")
				});
			var result = await service.HandleProxyIfMatched("/a/test");
			Assert.True(result.IsMatched);
			byte[] buffer = result.data;
			Assert.Equal(@"{""test"": ""b""}", Encoding.UTF8.GetString(buffer, 0, buffer.Length).Replace("\r\n", ""));
			var loadImage = await service.HandleProxyIfMatched("/a/logo.png");
			Assert.True(loadImage.IsMatched);
			Assert.Equal(8386, loadImage.data.Length);
		}
		[Fact]
		public async Task TestRemoteFileProxy()
		{
			IService service = Service.StandardService(new IRoute[1] {
				new FileRoute("/a/test.png", "https://www.myfi.ws/img/sd/icon-48x48.png")
			});
			var result = await service.HandleProxyIfMatched("/a/test.png");
			Assert.True(result.IsMatched);
			Assert.Equal(3141, result.data.Length);
		}
	}
}
