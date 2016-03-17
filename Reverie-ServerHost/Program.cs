namespace Reverie_ServerHost
{
	using Reverie.Server;


	public class Program
	{
		static void Main(string[] args)
		{
			ReverieWebSocketServer server = new ReverieWebSocketServer();
			server.Start();
			System.Console.ReadKey();
		}
	}
}
