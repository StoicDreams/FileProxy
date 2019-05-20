using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoicDreams.FileProxy.Interface;
using StoicDreams.FileProxy.Filter;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("XUnitTests")]

namespace StoicDreams.FileProxy
{
	/// <summary>
	/// This class is the main entry point for running a proxy server
	/// </summary>
	public class Server : IServer
	{
		private readonly Dictionary<string, IRoute> Routes;
		public Server(IServerConfig serverConfig)
		{
			Routes = new Dictionary<string, IRoute>();
			foreach (IRoute route in serverConfig.Routes)
			{
				route.ValidateSetup();
				string key = route.RequestedPath.FilterURLToRoutePath().ToLower();
				if (Routes.ContainsKey(key))
				{
					throw new Exception("Duplicate route encountered. IRoute.RequestedPath values must not be reused for varying IRoute routes.");
				}
				Routes.Add(key, route);
			}
		}
		public bool RequestMatchesRouting(string request, out IRoute route)
		{
			route = default;
			string requestKey = request.FilterURLToRoutePath().ToLower();
			if (Routes.ContainsKey(requestKey))
			{
				route = Routes[requestKey];
				return true;
			}
			foreach(string key in Routes.Keys)
			{
				if (Routes[key].RequestMatchesPath(requestKey))
				{
					route = Routes[key];
					return true;
				}
			}
			return false;
		}
	}
}
