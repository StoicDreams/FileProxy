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
	}
}
