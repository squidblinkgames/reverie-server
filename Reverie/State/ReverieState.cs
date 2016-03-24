namespace Reverie.State
{
	public class ReverieState
	{
		#region Fields
		private readonly PrototypeCache prototypes;
		#endregion


		#region Constructors
		public ReverieState()
		{
			this.prototypes = new PrototypeCache();
		}
		#endregion


		#region Properties
		public PrototypeCache Prototypes
		{
			get { return this.prototypes; }
		}
		#endregion
	}
}