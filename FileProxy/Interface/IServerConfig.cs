
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoicDreams.FileProxy.Interface
{
	public interface IServerConfig
	{
		IRoute[] Routes { get; }
		Func<string, Task<FileData>> HandleLocalFile { get; }
		Func<string, IDictionary<string, object>, Task<FileData>> HandleRemoteFile { get; }
	}
}
