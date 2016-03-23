namespace Reverie
{
	using System.Collections.Generic;
	using PrimitiveEngine.Artemis;
	using Reverie.Entities;


	public class ReverieState
	{
		private readonly EntityWorld entityWorld;
		public Dictionary<int, Prototype> Prototypes { get; private set; }


		public ReverieState(EntityWorld entityWorld)
		{
			this.entityWorld = entityWorld;
			this.Prototypes = new Dictionary<int, Prototype>();
			// TODO: Saturate prototypes from database.
		}
	}
}
