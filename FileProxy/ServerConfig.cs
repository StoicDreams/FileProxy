using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoicDreams.FileProxy.Interface;

namespace StoicDreams.FileProxy
{
	public class ServerConfig : IServerConfig
	{
		public IRoute[] Routes { get; set; }

		public Func<string, Task<FileData>> HandleLocalFile { get; set; }
		public Func<string, Dictionary<string, object>, Task<FileData>> HandleRemoteFile { get; set; }
	}
}
