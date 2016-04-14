namespace Reverie.Components
{
	using System;
	using System.Collections.Generic;
	using Newtonsoft.Json;
	using PrimitiveEngine;
	using PrimitiveEngine.Components;


	[JsonObject(MemberSerialization.OptIn)]
	public class Container : Component
	{
		#region Fields
		private int? capacity;
		private List<Guid> childEntityIds;
		#endregion


		#region Constructors
		public Container(int? capacity = null, List<Guid> childEntityIds = null)
		{
			this.capacity = capacity;
			this.childEntityIds = childEntityIds;
		}
		#endregion


		#region Properties
		/// <summary>
		/// Gets or sets the capacity of the container.
		/// </summary>
		/// <value>
		/// The capacity.
		/// </value>
		[JsonProperty]
		public int? Capacity
		{
			get { return this.capacity; }
			set
			{
				if (value < 0)
					value = 0;
				this.capacity = value;
			}
		}


		/// <summary>
		/// Gets the child entity ids.
		/// </summary>
		/// <value>
		/// The child entity ids.
		/// </value>
		[JsonProperty]
		public IReadOnlyCollection<Guid> ChildEntityIds
		{
			get
			{
				if (this.childEntityIds == null)
					return null;
				return this.childEntityIds.AsReadOnly();
			}
		}
		#endregion


		/// <summary>
		/// Adds an entity to the container.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>How many of the entity was added.</returns>
		public int AddEntity(Entity entity)
		{
			if (entity == null)
				return 0;

			if (this.childEntityIds == null)
				this.childEntityIds = new List<Guid>();

			// TODO: Check if capacity is breached.
			// TODO: Don't allow child containers to add containers they're inside. Just compare child id's.
			if (this.childEntityIds.Contains(entity.UniqueId))
				return 0;

			this.childEntityIds.Add(entity.UniqueId);

			return 1;
		}


		/// <summary>
		/// Get all Entities contained by the container.
		/// </summary>
		/// <returns></returns>
		public IReadOnlyCollection<Entity> GetChildEntities()
		{
			List<Entity> inventoryItems = new List<Entity>();
			if (this.ChildEntityIds == null)
				return inventoryItems;

			foreach (Guid entityId in this.ChildEntityIds)
			{
				inventoryItems.Add(this.EntityWorld.GetEntityByUniqueId(entityId));
			}

			return inventoryItems.AsReadOnly();
		}


		/// <summary>
		/// Removes an entity from the container.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <param name="entity">The entity.</param>
		/// <returns>How many of the entity was removed.</returns>
		public int RemoveEntity(Entity entity)
		{
			if (this.childEntityIds == null)
				return 0;

			if (!this.childEntityIds.Contains(entity.UniqueId))
				return 0;

			this.childEntityIds.Remove(entity.UniqueId);

			return 1;
		}
	}


	public static class ContainerUtilities
	{
		public static IReadOnlyCollection<Entity> GetChildEntities(this Entity entity)
		{
			Container container = entity.GetComponent<Container>();
			if (container == null)
				return null;

			return container.GetChildEntities();
		}
	}
}