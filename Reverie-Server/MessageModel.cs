namespace Reverie.Server
{
	public sealed class MessageModel
	{
		#region Constructors
		public MessageModel() {}


		public MessageModel(object payload)
		{
			this.Payload = payload;
		}
		#endregion


		#region Properties
		public bool? Error { get; set; }
		public MetaData Meta { get; set; }
		public object Payload { get; set; }
		#endregion


		public sealed class MetaData
		{
			#region Properties
			public bool Initial { get; set; }
			#endregion
		}
	}
}