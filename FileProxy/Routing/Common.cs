using System;
using System.IO;
using System.Threading.Tasks;
using StoicDreams.FileProxy.Filter;

namespace StoicDreams.FileProxy.Routing
{
	/// <summary>
	/// Shared routing features
	/// </summary>
	internal static class Common
	{
		internal static bool RouteIsRemote(string route)
		{
			if (Matches.IsProtocolFormat(route)) { return true; }
			return false;
		}
		internal static async Task<byte[]> GetLocalFile(string relativePath)
		{
			string filepath = $"{Path.GetFullPath(".")}{relativePath}";
			if (!File.Exists(filepath)) { return default; }
			return await File.ReadAllBytesAsync(filepath);
		}
		internal static async Task<byte[]> GetRemoteFile(string urlPath)
		{
			return default;
		}
		public static void ValidateSetup(string requestedPath, string routedPath, string callingClass)
		{
			if (string.IsNullOrWhiteSpace(requestedPath))
			{
				throw new Exception($"{callingClass}.RequestedPath cannot be empty or null.");
			}
			if (string.IsNullOrWhiteSpace(routedPath))
			{
				throw new Exception($"{callingClass}.RoutedPath cannot be empty or null.");
			}
			if (requestedPath.ToLower() == routedPath.ToLower())
			{
				throw new Exception($"{callingClass}: RoutedPath and RequestedPath cannot be equal.");
			}
			if (requestedPath != requestedPath.FilterURLToRoutePath())
			{
				throw new Exception($"{callingClass}: Invalid RequestedPath.");
			}
		}
	}
}
