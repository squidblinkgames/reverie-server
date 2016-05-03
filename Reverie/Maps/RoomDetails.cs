namespace Reverie.Maps
{
	public sealed class RoomDetails
	{
		#region Fields
		private string name;
		private string description;
		private string metaData;
		#endregion


		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}


		public string Description
		{
			get { return this.description; }
			set { this.description = value; }
		}


		public string MetaData
		{
			get { return this.metaData; }
			set { this.metaData = value; }
		}
	}
}