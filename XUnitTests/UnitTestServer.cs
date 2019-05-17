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
			IServer server = new Server();
		}
	}
}
