using System;
using Xunit;
using StoicDreams.FileProxy;
using StoicDreams.FileProxy.Interface;

namespace XUnitTests
{
	public class UnitTestServer
	{
		[Fact]
		public void TestInit()
		{
			IServerConfig config = new ServerConfig() {

			};
			IServer server = new Server(config);
		}
	}
}
