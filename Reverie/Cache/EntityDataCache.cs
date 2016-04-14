namespace Reverie.Cache
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using PrimitiveEngine;
	using Reverie.Components;


	public class EntityDataCache : IEnumerable<EntityData>
	{
		#region Fields
		private readonly Dictionary<Guid, EntityData> prototypes;
		#endregion


		#region Constructors
		public EntityDataCache()
		{
			this.prototypes = new Dictionary<Guid, EntityData>();
		}


		public EntityDataCache(Dictionary<Guid, EntityData> prototypes)
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
		public EntityData this[Guid id]
		{
			get
			{
				if (this.prototypes.ContainsKey(id))
					return this.prototypes[id];
				throw new KeyNotFoundException("Prototype not found for key " + id);
			}
		}
		#endregion


		public void Add(EntityData entityData)
		{
			if (this.prototypes.ContainsKey(entityData.Id))
			{
				throw new ArgumentException(
					"Prototype Id " + entityData.Id +
					" already exists in cache.");
			}

			this.prototypes.Add(entityData.Id, entityData);
		}


		public EntityData GetBasePrototype(Entity entity)
		{
			EntityData entityData = entity.GetComponent<EntityData>();
			if (entityData == null)
				return null;

			return GetBasePrototype(entityData);
		}


		public EntityData GetBasePrototype(EntityData entityData)
		{
			if (entityData.ParentPrototypeId == null)
				return entityData;

			Guid parentId = entityData.ParentPrototypeId;
			if (this.prototypes.ContainsKey(parentId))
				return GetBasePrototype(this.prototypes[parentId]);

			throw new KeyNotFoundException("Missing parent prototype ID: " + parentId);
		}


		public IEnumerator<EntityData> GetEnumerator()
		{
			return this.prototypes.Values.GetEnumerator();
		}


		public EntityData Remove(EntityData entityData)
		{
			return Remove(entityData.Id);
		}


		public EntityData Remove(Guid id)
		{
			if (!this.prototypes.ContainsKey(id))
				return null;
			EntityData removedEntityData = this.prototypes[id];
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