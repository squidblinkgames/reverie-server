namespace Reverie.Server
{
	using System;
	using System.Net;
	using System.Threading;
	using System.Threading.Tasks;
	using vtortola.WebSockets;
	using vtortola.WebSockets.Rfc6455;


	public class WebSocketServer :
		IDisposable
	{
		#region Fields
		private readonly WebSocketListener listener;
		private Task process;
		#endregion


		#region Constructors
		public WebSocketServer(IPEndPoint endpoint)
			: this(endpoint, new WebSocketListenerOptions()) {}


		public WebSocketServer(IPEndPoint endpoint, WebSocketListenerOptions options)
		{
			this.listener = new WebSocketListener(endpoint, options);
			this.listener.Standards.RegisterStandard(new WebSocketFactoryRfc6455(this.listener));
		}
		#endregion


		#region Events
		public event Action<WebSocket> ClientConnected;
		public event Action<WebSocket> ClientDisconnected;
		public event Action<WebSocket, Exception> ErrorThrown;
		public event Action<WebSocket, string> MessageReceived;
		#endregion


		// TODO: Use proper dispose pattern.
		public void Dispose()
		{
			this.listener.Dispose();
		}


		public void Start()
		{
			this.listener.Start();
			this.process = Task.Run((Func<Task>)ListenForConnections);
		}


		public void Stop()
		{
			this.listener.Stop();
		}


		#region Helper Methods
		private async Task HandleMessage(WebSocket websocket)
		{
			try
			{
				if (ClientConnected != null)
					ClientConnected.Invoke(websocket);

				while (websocket.IsConnected)
				{
					string message = await websocket
												.ReadStringAsync(CancellationToken.None)
												.ConfigureAwait(false);
					//Console.WriteLine("Websocket: Got message.");
					if (message != null
						&& MessageReceived != null)
						MessageReceived.Invoke(websocket, message);
				}

				if (ClientDisconnected != null)
					ClientDisconnected.Invoke(websocket);
			}

			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex);
				if (ErrorThrown != null)
					ErrorThrown.Invoke(websocket, ex);
			}

			finally
			{
				websocket.Dispose();
			}
		}


		private async Task ListenForConnections()
		{
			if (this.listener.IsStarted)
			{
				try
				{
					WebSocket websocket = await this.listener
													.AcceptWebSocketAsync(CancellationToken.None)
													.ConfigureAwait(false);

					if (websocket != null)
						Task.Run(() => HandleMessage(websocket));
				}

				catch (Exception ex)
				{
					if (ErrorThrown != null)
						ErrorThrown.Invoke(null, ex);
				}
			}
		}


		// TODO: Thread safe event raising method.
		#endregion
	}
}