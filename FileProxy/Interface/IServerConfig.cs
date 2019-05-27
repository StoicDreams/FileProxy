
using System;
using System.Threading.Tasks;

namespace StoicDreams.FileProxy.Interface
{
	public interface IServerConfig
	{
		IRoute[] Routes { get; }
		delegate Task HandleGetFile(string filePath, Func<object> handleResult);
		Func<string, Task<FileData>> HandleLocalFile { get; }
		Func<string, Task<FileData>> HandleRemoteFile { get; }
	}
}
