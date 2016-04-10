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

			PlayerComponent playerComponent = this.playerEntity.GetComponent<PlayerComponent>();
			if (playerComponent != null)
				playerComponent.Updated += OnPlayerEntityUpdated;
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