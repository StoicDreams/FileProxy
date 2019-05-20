using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoicDreams.FileProxy.Interface;

namespace StoicDreams.FileProxy
{
	public class ServerConfig : IServerConfig
	{
		public IRoute[] Routes { get; set; }

		public Func<string, Task<byte[]>> HandleLocalFile { get; set; }
		public Func<string, Task<byte[]>> HandleRemoteFile { get; set; }
	}
}
