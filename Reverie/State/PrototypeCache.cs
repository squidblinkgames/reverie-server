namespace Reverie.State
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using PrimitiveEngine;
	using Reverie.Entities;


	public class PrototypeCache : IEnumerable<PrototypeComponent>
	{
		#region Fields
		private readonly Dictionary<long?, PrototypeComponent> prototypes;
		#endregion


		#region Constructors
		public PrototypeCache()
		{
			this.prototypes = new Dictionary<long?, PrototypeComponent>();
		}


		public PrototypeCache(Dictionary<long?, PrototypeComponent> prototypes)
		{
			this.prototypes = prototypes;
		}
		#endregion


		#region Properties
		public int Count
		{
			get { return this.prototypes.Count; }
		}
		#endregion


		#region Indexers
		public PrototypeComponent this[long? id]
		{
			get
			{
				if (this.prototypes.ContainsKey(id))
					return this.prototypes[id];
				return null;
			}
		}
		#endregion


		public void Add(PrototypeComponent prototype)
		{
			if (this.prototypes.ContainsKey(prototype.Id))
			{
				throw new ArgumentException(
					"Prototype Id " + prototype.Id +
					" already exists in cache.");
			}

			this.prototypes.Add(prototype.Id, prototype);
		}


		public PrototypeComponent GetBasePrototype(Entity entity)
		{
			PrototypeComponent prototype = entity.GetComponent<PrototypeComponent>();
			if (prototype == null)
				return null;

			return GetBasePrototype(prototype);
		}


		public PrototypeComponent GetBasePrototype(PrototypeComponent prototype)
		{
			if (prototype.ParentPrototypeId == null)
				return prototype;

			long parentId = (int)prototype.ParentPrototypeId;
			if (this.prototypes.ContainsKey(parentId))
				return GetBasePrototype(this.prototypes[parentId]);

			throw new KeyNotFoundException("Missing parent prototype ID: " + parentId);
		}


		public IEnumerator<PrototypeComponent> GetEnumerator()
		{
			return this.prototypes.Values.GetEnumerator();
		}


		public PrototypeComponent Remove(PrototypeComponent prototype)
		{
			return Remove(prototype.Id);
		}


		public PrototypeComponent Remove(long id)
		{
			if (!this.prototypes.ContainsKey(id))
				return null;
			PrototypeComponent removedPrototype = this.prototypes[id];
			this.prototypes.Remove(id);

			return removedPrototype;
		}


		#region Helper Methods
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		#endregion
	}
}