namespace Reverie.State
{
	public class ReverieState
	{
		#region Constructors
		public ReverieState()
		{
			this.Prototypes = new PrototypeCache();
		}
		#endregion


		#region Properties
		public PrototypeCache Prototypes { get; set; }
		#endregion
	}
}