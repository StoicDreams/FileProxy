using StoicDreams.FileProxy.Interface;
using StoicDreams.FileProxy.Filter;
using System.Collections.Generic;

namespace StoicDreams.FileProxy.Routing
{
	public class FileRoute : IRoute
	{
		public bool RouteIsRemote { get; private set; }
		public IDictionary<string, object> Headers { get; private set; }
		public FileRoute(string requestedPath, string routedPath, IDictionary<string, object> headers = null)
		{
			RequestedPath = requestedPath;
			RoutedPath = routedPath;
			Headers = headers;
			ValidateSetup();
			RouteIsRemote = Common.RouteIsRemote(routedPath);
		}
		/// <summary>
		/// Complete relative path to match incoming requests from client.
		/// Note: Requested URL must match exactly up to any query string data (not-case-sensitive)
		/// </summary>
		public string RequestedPath { get; set; }
		/// <summary>
		/// Complete path to replace incoming requests from client to route to.
		/// Note: Relative paths will be translated to file paths.
		/// May also use full http paths.
		/// </summary>
		public string RoutedPath { get; set; }
		public bool RequestMatchesPath(string request)
		{
			return RequestedPath.ToLower() == request.FilterURLToRoutePath().ToLower();
		}
		public string TranslateRequestToRoutedPath(string request)
		{
			if(request.Length > RequestedPath.Length)
			{
				return request.Replace(request.Substring(0, RequestedPath.Length), RoutedPath);
			}
			return RoutedPath;
		}
		/// <summary>
		/// Run after initializing and setting path values to validate path values.
		/// </summary>
		/// <returns></returns>
		public bool ValidateSetup()
		{
			Common.ValidateSetup(RequestedPath, RoutedPath, "FileRoute");
			return true;
		}
	}
}
