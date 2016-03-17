namespace Reverie.Server
{
	using Artemis;
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
	}
}