using System.Threading.Tasks;

namespace StoicDreams.FileProxy.Interface
{
	public interface IServer
	{
		bool RequestMatchesRouting(string request, out IRoute route);
	}
}
