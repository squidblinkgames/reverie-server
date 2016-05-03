namespace Reverie.Cache
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using PrimitiveEngine;
	using Reverie.Components;


	public class EntityDataCache : IEnumerable<EntityDetails>
	{
		#region Fields
		private readonly Dictionary<Guid, EntityDetails> prototypes;
		#endregion


		#region Constructors
		public EntityDataCache()
		{
			this.prototypes = new Dictionary<Guid, EntityDetails>();
		}


		public EntityDataCache(Dictionary<Guid, EntityDetails> prototypes)
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
		public EntityDetails this[Guid id]
		{
			get
			{
				if (this.prototypes.ContainsKey(id))
					return this.prototypes[id];
				throw new KeyNotFoundException("Prototype not found for key " + id);
			}
		}
		#endregion


		public void Add(EntityDetails entityDetails)
		{
			if (this.prototypes.ContainsKey(entityDetails.Id))
			{
				throw new ArgumentException(
					"Prototype Id " + entityDetails.Id +
					" already exists in cache.");
			}

			this.prototypes.Add(entityDetails.Id, entityDetails);
		}


		public EntityDetails GetBasePrototype(Entity entity)
		{
			EntityDetails entityDetails = entity.GetComponent<EntityDetails>();
			if (entityDetails == null)
				return null;

			return GetBasePrototype(entityDetails);
		}


		public EntityDetails GetBasePrototype(EntityDetails entityDetails)
		{
			if (entityDetails.ParentPrototypeId == null)
				return entityDetails;

			Guid parentId = entityDetails.ParentPrototypeId;
			if (this.prototypes.ContainsKey(parentId))
				return GetBasePrototype(this.prototypes[parentId]);

			throw new KeyNotFoundException("Missing parent prototype ID: " + parentId);
		}


		public IEnumerator<EntityDetails> GetEnumerator()
		{
			return this.prototypes.Values.GetEnumerator();
		}


		public EntityDetails Remove(EntityDetails entityDetails)
		{
			return Remove(entityDetails.Id);
		}


		public EntityDetails Remove(Guid id)
		{
			if (!this.prototypes.ContainsKey(id))
				return null;
			EntityDetails removedEntityDetails = this.prototypes[id];
			this.prototypes.Remove(id);

			return removedEntityDetails;
		}


		#region Helper Methods
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		#endregion
	}
}