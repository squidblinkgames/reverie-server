namespace Reverie
{
	using System.Collections.Generic;
	using Artemis;
	using Reverie.Entities.Components;


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
