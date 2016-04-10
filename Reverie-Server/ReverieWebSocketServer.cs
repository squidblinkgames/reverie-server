namespace Reverie.Server
{
	using System;
	using System.Collections.Generic;
	using System.Net;
	using System.Threading.Tasks;
	using Newtonsoft.Json;
	using PrimitiveEngine;
	using Reverie.Cache;
	using Reverie.Maps;
	using Reverie.Models;
	using Reverie.Utilities;
	using vtortola.WebSockets;


	public class ReverieWebSocketServer
	{
		private const int Port = 3000;
		private static readonly WebSocketListenerOptions serverOptions =
			new WebSocketListenerOptions { SubProtocols = new[] { "text" } };


		#region Fields
		private WebSocketServer server;
		private ReverieGame reverieGame;
		private Dictionary<WebSocket, PlayerSession> activePlayers;
		#endregion


		public ReverieWebSocketServer()
		{
			this.activePlayers = new Dictionary<WebSocket, PlayerSession>();
			this.reverieGame = new ReverieGame();
			this.server = 
				new WebSocketServer(new IPEndPoint(IPAddress.Any, Port), serverOptions);
		}


		public void Start()
		{
			Task task = Task.Run(() => this.reverieGame.Start());
			
			this.server.ClientConnected += OnClientConnected;
			this.server.ClientDisconnected += OnClientDisconnected;
			this.server.MessageReceived += OnMessageReceived;
			this.server.Start();
			Console.WriteLine("Started server.");
		}


		#region Helper Methods
		private void OnMessageReceived(WebSocket webSocket, string message)
		{
			Console.WriteLine("Incoming message.");
			if (message == null)
				return;

			if (!this.activePlayers.ContainsKey(webSocket))
			{
				webSocket.WriteString("No player session found for: " + webSocket.HttpRequest.Headers.Host);
				webSocket.Close();
			}
			
			Console.WriteLine("Got command: " + message);

			PlayerSession player = this.activePlayers[webSocket];

			string commandResult = 
				this.reverieGame.Interpreter.Interpret(player.PlayerEntity, message);
			Console.WriteLine(commandResult);
			webSocket.WriteString(commandResult);
		}


		private void OnClientConnected(WebSocket webSocket)
		{
			Console.WriteLine("Client connected.");
			Entity player = this.reverieGame.InsertPlayer();
			PlayerSession playerSession = new PlayerSession(webSocket, player);
			this.activePlayers.Add(webSocket, playerSession);
			
			
			MapNode playerRoom = player.GetMapNode();
			RoomModel roomModel = new RoomModel(playerRoom, player);
			
			webSocket.WriteString(roomModel.ToPrettyJson());
		}


		private void OnClientDisconnected(WebSocket webSocket)
		{
			Console.WriteLine("Client disconnected.");
			this.activePlayers.Remove(webSocket);
		}
		#endregion
	}
}