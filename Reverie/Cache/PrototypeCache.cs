namespace Reverie.Cache
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using PrimitiveEngine;
	using Reverie.Components;


	public class PrototypeCache : IEnumerable<EntityDataComponent>
	{
		#region Fields
		private readonly Dictionary<long?, EntityDataComponent> prototypes;
		#endregion


		#region Constructors
		public PrototypeCache()
		{
			this.prototypes = new Dictionary<long?, EntityDataComponent>();
		}


		public PrototypeCache(Dictionary<long?, EntityDataComponent> prototypes)
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
		public EntityDataComponent this[long? id]
		{
			get
			{
				if (this.prototypes.ContainsKey(id))
					return this.prototypes[id];
				return null;
			}
		}
		#endregion


		public void Add(EntityDataComponent entityData)
		{
			if (this.prototypes.ContainsKey(entityData.Id))
			{
				throw new ArgumentException(
					"Prototype Id " + entityData.Id +
					" already exists in cache.");
			}

			this.prototypes.Add(entityData.Id, entityData);
		}


		public EntityDataComponent GetBasePrototype(Entity entity)
		{
			EntityDataComponent entityData = entity.GetComponent<EntityDataComponent>();
			if (entityData == null)
				return null;

			return GetBasePrototype(entityData);
		}


		public EntityDataComponent GetBasePrototype(EntityDataComponent entityData)
		{
			if (entityData.ParentPrototypeId == null)
				return entityData;

			long parentId = (int)entityData.ParentPrototypeId;
			if (this.prototypes.ContainsKey(parentId))
				return GetBasePrototype(this.prototypes[parentId]);

			throw new KeyNotFoundException("Missing parent prototype ID: " + parentId);
		}


		public IEnumerator<EntityDataComponent> GetEnumerator()
		{
			return this.prototypes.Values.GetEnumerator();
		}


		public EntityDataComponent Remove(EntityDataComponent entityData)
		{
			return Remove(entityData.Id);
		}


		public EntityDataComponent Remove(long id)
		{
			if (!this.prototypes.ContainsKey(id))
				return null;
			EntityDataComponent removedEntityData = this.prototypes[id];
			this.prototypes.Remove(id);

			return removedEntityData;
		}


		#region Helper Methods
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		#endregion
	}
}