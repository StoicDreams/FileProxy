using System.Text.RegularExpressions;

namespace StoicDreams.FileProxy.Filter
{
	public static class Matches
	{
		private static readonly Regex ProtocolFormat = new Regex(@"^[A-Za-z]+\:\/\/[^\/]+\/[A-Za-z0-9]+.*$");
		public static bool IsProtocolFormat(string input)
		{
			return ProtocolFormat.IsMatch(input);
		}
	}
}
