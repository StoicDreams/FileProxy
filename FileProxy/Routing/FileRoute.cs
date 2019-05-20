using System;
using System.Threading.Tasks;
using StoicDreams.FileProxy.Interface;
using StoicDreams.FileProxy.Filter;

namespace StoicDreams.FileProxy.Routing
{
	public class FileRoute : IRoute
	{
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
		public async Task GetRoutedFile(string requestedPath)
		{

		}
		/// <summary>
		/// Run after initializing and setting path values to validate path values.
		/// </summary>
		/// <returns></returns>
		public bool ValidateSetup()
		{
			if (string.IsNullOrWhiteSpace(RequestedPath))
			{
				throw new Exception("FileRoute.RequestedPath cannot be empty or null.");
			}
			if (string.IsNullOrWhiteSpace(RoutedPath))
			{
				throw new Exception("FileRoute.RoutedPath cannot be empty or null.");
			}
			if (RequestedPath.ToLower() == RoutedPath.ToLower())
			{
				throw new Exception("FileRoute: RoutedPath and RequestedPath cannot be equal.");
			}
			if(RequestedPath != RequestedPath.FilterURLToRoutePath())
			{
				throw new Exception("FileRoute: Invalid RequestedPath.");
			}
			return true;
		}
	}
}
