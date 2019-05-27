using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
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
		internal static async Task<FileData> GetLocalFile(string relativePath)
		{
			string filepath = $"{Path.GetFullPath(".")}{relativePath}";
			if (!File.Exists(filepath)) { return default; }
			return new FileData()
			{
				Data = await File.ReadAllBytesAsync(filepath)
				, StatusCode = System.Net.HttpStatusCode.OK
			};
		}
		internal static async Task<FileData> GetRemoteFile(string urlPath, Dictionary<string, object> headers)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					if(headers != null)
					{
						foreach(string key in headers.Keys)
						{
							client.DefaultRequestHeaders.Add(key, headers[key].ToString());
						}
					}
					HttpResponseMessage response = await client.GetAsync(urlPath);
					return new FileData()
					{
						Data = await response.Content.ReadAsByteArrayAsync()
						, ContentType = response.Content.Headers.ContentType.ToString()
						, StatusCode = response.StatusCode
					};
				}
			}
			catch { }
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
