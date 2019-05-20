using System;
using System.Threading.Tasks;
using StoicDreams.FileProxy.Filter;
using StoicDreams.FileProxy.Interface;

namespace StoicDreams.FileProxy.Routing
{
	public class FolderRoute : IRoute
	{
		/// <summary>
		/// Relative folder path to match incoming requests from client.
		/// </summary>
		public string RequestedPath { get; set; }
		/// <summary>
		/// Folder path to replace incoming requests from client to route to.
		/// Note: Relative paths will be translated to file paths.
		/// May also use full http paths.
		/// </summary>
		public string RoutedPath { get; set; }
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
				throw new Exception("FolderRoute.RequestedPath cannot be empty or null.");
			}
			if (string.IsNullOrWhiteSpace(RoutedPath))
			{
				throw new Exception("FolderRoute.RoutedPath cannot be empty or null.");
			}
			if (RequestedPath.ToLower() == RoutedPath.ToLower())
			{
				throw new Exception("FolderRoute: RoutedPath and RequestedPath cannot be equal.");
			}
			if (RequestedPath != RequestedPath.FilterURLToRoutePath())
			{
				throw new Exception("FolderRoute: Invalid RequestedPath.");
			}
			return true;
		}
	}
}
