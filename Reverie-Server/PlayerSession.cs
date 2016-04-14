namespace Reverie.Server
{
	using PrimitiveEngine;
	using Reverie.Components;
	using vtortola.WebSockets;


	public class PlayerSession
	{
		#region Fields
		private readonly WebSocket webSocket;
		private readonly Entity playerEntity;
		#endregion


		#region Constructors
		public PlayerSession(WebSocket webSocket, Entity playerEntity)
		{
			this.webSocket = webSocket;
			this.playerEntity = playerEntity;

			Player player = this.playerEntity.GetComponent<Player>();
			if (player != null)
				player.Updated += OnPlayerEntityUpdated;
		}
		#endregion


		#region Properties
		public Entity PlayerEntity
		{
			get { return this.playerEntity; }
		}


		public WebSocket WebSocket
		{
			get { return this.webSocket; }
		}
		#endregion


		public void OnPlayerEntityUpdated(string message)
		{
			this.webSocket.WriteString(message);
		}
	}
}