namespace Reverie.Items
{
	using System.Collections.Generic;
	using System.Linq;
	using PrimitiveEngine;
	using Reverie.Entities;
	using Reverie.Items.Components;
	using Reverie.Items.Models;
	using Reverie.State;


	public static class Inventory
	{
		#region Enums
		public enum LoadOptions
		{
			Recursive,
			LowercaseNames,
			Flatten
		}
		#endregion


		/// <summary>
		/// Gets the inventory of an Entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>A list of item models representing inventory contents.</returns>
		public static List<ContainerModel> GetContainerContents(
			this Entity entity,
			params LoadOptions[] options)
		{
			List<ContainerModel> inventory = new List<ContainerModel>();
			bool recursive = options.Contains(LoadOptions.Recursive);

			ContainerComponent containerComponent = entity.GetComponent<ContainerComponent>();
			foreach (long id in containerComponent.ChildEntityIds)
			{
				Entity item = entity.GetEntityByUniqueId(id);
				ContainerModel containerData = GetItemModel(item, options);
				inventory.Add(containerData);
			}

			return inventory;
		}


		public static List<Entity> GetContainerEntities(this Entity entity)
		{
			ContainerComponent inventory = entity.GetComponent<ContainerComponent>();
			if (inventory == null)
				return null;

			List<Entity> inventoryItems = new List<Entity>();
			if (inventory.ChildEntityIds == null)
				return inventoryItems;

			EntityWorld world = entity.EntityWorld;
			foreach (long entityId in inventory.ChildEntityIds)
			{
				inventoryItems.Add(world.GetEntityByUniqueId(entityId));
			}

			return inventoryItems;
		}


		/// <summary>
		/// Gets an item model representation of an entity.
		/// </summary>
		/// <param name="itemEntity">The item entity.</param>
		/// <returns>The item model.</returns>
		public static ContainerModel GetItemModel(this Entity itemEntity, LoadOptions[] options)
		{
			bool recursive = options.Contains(LoadOptions.Recursive);

			EntityWorld world = itemEntity.EntityWorld;
			PrototypeComponent prototype = itemEntity.GetComponent<PrototypeComponent>();
			ContainerModel containerModel = SaturateBasicItemModel(itemEntity, prototype, options);

			// If a container, get its contents, too.
			if (!recursive)
				return containerModel;

			ContainerComponent containerComponent = itemEntity.GetComponent<ContainerComponent>();
			if (containerComponent != null
				&& containerComponent.ChildEntityIds != null)
			{
				containerModel.ItemIds = new List<long>();
				containerModel.Entities = new List<ContainerModel>();

				foreach (long childId in containerComponent.ChildEntityIds)
				{
					Entity childEntity = world.GetEntityByUniqueId(childId);
					ContainerModel childContainerModel = GetItemModel(childEntity, options);
					containerModel.ItemIds.Add(childContainerModel.Id);
					containerModel.Entities.Add(childContainerModel);
				}
			}

			return containerModel;
		}


		#region Helper Methods
		private static ContainerModel SaturateBasicItemModel(
			Entity itemEntity,
			PrototypeComponent prototype,
			LoadOptions[] options)
		{
			bool toLowercase = options.Contains(LoadOptions.LowercaseNames);

			EntityWorld world = itemEntity.EntityWorld;
			PrototypeCache prototypes = WorldCache.GetCache(world).Prototypes;
			ContainerModel containerModel = new ContainerModel();

			// Get basic item details.
			if (toLowercase)
				containerModel.Name = prototype.Name.ToLower();
			else
				containerModel.Name = prototype.Name;
			containerModel.Id = itemEntity.UniqueId;
			containerModel.PrototypeId = prototype.Id;
			containerModel.Type = prototypes.GetBasePrototype(itemEntity).Name;

			// Get quantity.
			StackComponent stack = itemEntity.GetComponent<StackComponent>();
			if (stack == null)
				containerModel.Quantity = 1;
			else
				containerModel.Quantity = stack.Quantity;

			return containerModel;
		}
		#endregion
	}
}