namespace Reverie.Cache
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using PrimitiveEngine.Artemis;
	using Reverie.Entities;


	public class PrototypeCache : IEnumerable<Prototype>
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


		#region Properties
		public int Count
		{
			get { return this.prototypes.Count; }
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


		public void Add(Prototype prototype)
		{
			if (this.prototypes.ContainsKey(prototype.Id))
			{
				throw new ArgumentException(
					"Prototype Id " + prototype.Id +
					" already exists in cache.");
			}

			this.prototypes.Add(prototype.Id, prototype);
		}


		public Prototype GetBasePrototype(Entity entity)
		{
			Prototype prototype = entity.GetComponent<Prototype>();
			if (prototype == null)
				return null;

			return GetBasePrototype(prototype);
		}


		public Prototype GetBasePrototype(Prototype prototype)
		{
			if (prototype.ParentPrototypeId == null)
				return prototype;

			long parentId = (int)prototype.ParentPrototypeId;
			if (this.prototypes.ContainsKey(parentId))
				return GetBasePrototype(this.prototypes[parentId]);

			throw new KeyNotFoundException("Missing parent prototype ID: " + parentId);
		}


		public IEnumerator<Prototype> GetEnumerator()
		{
			return this.prototypes.Values.GetEnumerator();
		}


		public Prototype Remove(Prototype prototype)
		{
			return Remove(prototype.Id);
		}


		public Prototype Remove(long id)
		{
			if (!this.prototypes.ContainsKey(id))
				return null;
			Prototype removedPrototype = this.prototypes[id];
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