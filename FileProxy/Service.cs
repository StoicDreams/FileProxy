using StoicDreams.FileProxy.Interface;
using StoicDreams.FileProxy.Routing;
using System.Threading.Tasks;

namespace StoicDreams.FileProxy
{
	/// <summary>
	/// Main entry point for setting up and running basic proxy services for your web app.
	/// </summary>
	public class Service : IService
	{
		private readonly IServer Server;
		public static Service StandardService(IRoute[] routes)
		{
			IServerConfig config = new ServerConfig()
			{
				Routes = routes
				, HandleLocalFile = Common.GetLocalFile
				, HandleRemoteFile = Common.GetRemoteFile
			};
			IServer server = new Server(config);
			return new Service(server);
		}
		public Service(IServer server)
		{
			Server = server;
		}
		public async Task<(bool IsMatched, FileData fileData)> HandleProxyIfMatchedAsync(string requestPath)
		{
			bool IsMatched = false;
			FileData fileData = null;
			if(Server.RequestMatchesRouting(requestPath, out IRoute route))
			{
				IsMatched = true;
				if (route.RouteIsRemote)
				{
					fileData = await Common.GetRemoteFile(route.TranslateRequestToRoutedPath(requestPath));
				}
				else
				{
					fileData = await Common.GetLocalFile(route.TranslateRequestToRoutedPath(requestPath));
				}
			}

			return (IsMatched, fileData);
		}
	}
}
