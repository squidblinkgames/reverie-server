namespace Reverie.Cache
{
	public sealed class ReverieState
	{
		#region Constructors
		public ReverieState()
		{
			this.EntityDatas = new EntityDataCache();
			this.Maps = new MapCache();
		}
		#endregion


		#region Properties
		public EntityDataCache EntityDatas { get; set; }
		public MapCache Maps { get; set; }
		#endregion
	}
}