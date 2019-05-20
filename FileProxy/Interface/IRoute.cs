using System.Threading.Tasks;

namespace StoicDreams.FileProxy.Interface
{
	/// <summary>
	/// Interface for routing rules
	/// </summary>
	public interface IRoute
	{
		/// <summary>
		/// Relative url path - complete or partial - as requested by the client
		/// </summary>
		string RequestedPath { get; }
		/// <summary>
		/// Translated path that will replace the RequestedPath value of the requested file path
		/// Should expect full URL or relative url.
		/// Note: Relative URLs should be translated to file paths.
		/// </summary>
		string RoutedPath { get; }
		bool RequestMatchesPath(string request);
		string TranslateRequestToRoutedPath(string rquest);
		Task GetRoutedFile(string requestedPath);
		/// <summary>
		/// Use this method to validate paths meet whatever requirements.
		/// It is expected this would be setup correctly and tested during development, so it is recommended to throw errors here instead of relying on a boolean result.
		/// </summary>
		/// <returns></returns>
		bool ValidateSetup();
	}
}
