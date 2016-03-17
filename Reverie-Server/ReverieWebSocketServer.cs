namespace Reverie.Server
{
	using System;
	using System.Net;
	using System.Threading.Tasks;
	using Artemis;
	using vtortola.WebSockets;


	public class ReverieWebSocketServer
	{
		private static readonly WebSocketListenerOptions serverOptions =
			new WebSocketListenerOptions { SubProtocols = new[] { "text" } };


		#region Fields
		private WebSocketServer server;
		private ReverieGame reverieGame;
		#endregion


		public void OnMessageReceived(WebSocket webSocket, string message)
		{
			Console.WriteLine("Got command.");
			if (message == null)
				return;

			Entity player = this.reverieGame.InsertPlayer();

			string commandResult = this.reverieGame.CommandInterpreter.Interpret(player, message);
			Console.WriteLine(commandResult);
			webSocket.WriteString(commandResult);
		}


		public void Start()
		{
			this.server = new WebSocketServer(new IPEndPoint(IPAddress.Any, 32666), serverOptions);
			this.server.Start();

			this.reverieGame = new ReverieGame();
			this.server.MessageReceived += OnMessageReceived;
			Task task = Task.Run(() => this.reverieGame.Start());
		}
	}
}