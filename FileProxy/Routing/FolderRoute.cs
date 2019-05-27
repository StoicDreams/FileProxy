using System.Collections.Generic;
using System.Threading.Tasks;
using StoicDreams.FileProxy.Filter;
using StoicDreams.FileProxy.Interface;

namespace StoicDreams.FileProxy.Routing
{
	public class FolderRoute : IRoute
	{
		public bool RouteIsRemote { get; private set; }
		public Dictionary<string, object> Headers { get; private set; }
		public FolderRoute(string requestedPath, string routedPath, Dictionary<string, object> headers = null)
		{
			RequestedPath = requestedPath;
			RoutedPath = routedPath;
			Headers = headers;
			ValidateSetup();
			RouteIsRemote = Common.RouteIsRemote(routedPath);
		}
		/// <summary>
		/// Relative folder path to match incoming requests from client.
		/// </summary>
		public string RequestedPath { get; }
		/// <summary>
		/// Folder path to replace incoming requests from client to route to.
		/// Note: Relative paths will be translated to file paths.
		/// May also use full http paths.
		/// </summary>
		public string RoutedPath { get; }
		public bool RequestMatchesPath(string request)
		{
			request = request.FilterURLToRoutePath();
			if(request.Length <= RoutedPath.Length) { return false; }
			return request.Substring(0, RequestedPath.Length).ToLower() == RequestedPath.ToLower();
		}
		public string TranslateRequestToRoutedPath(string request)
		{
			return request.Replace(request.Substring(0, RequestedPath.Length), RoutedPath);
		}
		/// <summary>
		/// Run after initializing and setting path values to validate path values.
		/// </summary>
		/// <returns></returns>
		public bool ValidateSetup()
		{
			Common.ValidateSetup(RequestedPath, RoutedPath, "FolderRoute");
			return true;
		}
	}
}
