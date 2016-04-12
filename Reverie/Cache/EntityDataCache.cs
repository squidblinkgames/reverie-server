namespace Reverie.Cache
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using PrimitiveEngine;
	using Reverie.Components;


	public class EntityDataCache : IEnumerable<EntityDataComponent>
	{
		#region Fields
		private readonly Dictionary<Guid, EntityDataComponent> prototypes;
		#endregion


		#region Constructors
		public EntityDataCache()
		{
			this.prototypes = new Dictionary<Guid, EntityDataComponent>();
		}


		public EntityDataCache(Dictionary<Guid, EntityDataComponent> prototypes)
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
		public EntityDataComponent this[Guid id]
		{
			get
			{
				if (this.prototypes.ContainsKey(id))
					return this.prototypes[id];
				throw new KeyNotFoundException("Prototype not found for key " + id);
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

			Guid parentId = entityData.ParentPrototypeId;
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


		public EntityDataComponent Remove(Guid id)
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