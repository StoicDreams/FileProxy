using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace StoicDreams.FileProxy.Filter
{
	public static class Filters
	{
		public static Regex RemoveFromPath = new Regex(@"[A-Za-z]+\:\/\/[^\/]+", RegexOptions.IgnoreCase & RegexOptions.Singleline);
		public static string FilterURLToRoutePath(this string input)
		{
			string result = input.Split('?')[0];
			if (result.Contains('#')) { result = result.Split('#')[0]; }
			return RemoveFromPath.Replace(result, "");
		}
	}
}
