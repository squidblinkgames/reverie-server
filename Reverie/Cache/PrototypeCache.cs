namespace Reverie.Cache
{
	using System.Collections.Generic;
	using Reverie.Entities.Components;


	public class PrototypeCache
	{
		#region Fields
		private readonly Dictionary<long, Prototype> prototypes;
		#endregion


		#region Constructors
		public PrototypeCache()
		{
			this.prototypes = new Dictionary<long, Prototype>();
		}


		public PrototypeCache(Dictionary<long, Prototype> prototypes)
		{
			this.prototypes = prototypes;
		}
		#endregion


		#region Indexers
		public Prototype this[long id]
		{
			get
			{
				if (this.prototypes.ContainsKey(id))
					return this.prototypes[id];
				return null;
			}
		}
		#endregion
	}
}