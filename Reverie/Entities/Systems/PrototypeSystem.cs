﻿namespace Reverie.Entities.Systems
{
	using System.Collections.Generic;
	using Artemis;
	using Artemis.Systems;
	using PrimitiveEngine.Artemis;
	using Reverie.Entities.Components;


	[ArtemisEntitySystem]
	public class PrototypeSystem : EntitySystem
	{
		#region Fields
		private Dictionary<long, Prototype> prototypes;
		#endregion


		#region Properties
		public Dictionary<long, Prototype> Prototypes
		{
			get
			{
				if (this.prototypes == null)
				{
					this.prototypes =
						this.BlackBoard.GetEntry<Dictionary<long, Prototype>>(Prototype.Key);
				}
				return this.prototypes;
			}
		}
		#endregion


		public Prototype GetBasePrototype(Entity entity)
		{
			Prototype prototype = entity.GetComponent<Prototype>();
			if (prototype == null)
				return null;

			return GetBasePrototype(prototype);
		}


		public Prototype GetBasePrototype(
			Prototype prototype)
		{
			if (prototype.ParentPrototypeId == null)
				return prototype;

			long parentId = (int)prototype.ParentPrototypeId;
			if (this.Prototypes.ContainsKey(parentId))
				return GetBasePrototype(this.Prototypes[parentId]);

			throw new KeyNotFoundException("Missing parent prototype ID: " + parentId);
		}
	}
}