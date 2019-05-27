using System.Net;

namespace StoicDreams.FileProxy
{
	public class FileData
	{
		public byte[] Data;
		public string ContentType;
		public HttpStatusCode StatusCode;
	}
}
