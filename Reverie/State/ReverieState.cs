namespace Reverie.State
{
	public sealed class ReverieState
	{
		#region Constructors
		public ReverieState()
		{
			this.Prototypes = new PrototypeCache();
			this.Maps = new MapCache();
		}
		#endregion


		#region Properties
		public PrototypeCache Prototypes { get; set; }
		public MapCache Maps { get; set; }
		#endregion
	}
}