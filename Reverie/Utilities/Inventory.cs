namespace Reverie.Utilities
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using PrimitiveEngine;
	using Reverie.Components;
	using Reverie.Models;
	using Reverie.Cache;


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
		public static List<EntityModel> GetContainerContents(
			this Entity entity,
			params LoadOptions[] options)
		{
			List<EntityModel> inventory = new List<EntityModel>();
			
			ContainerComponent containerComponent = entity.GetComponent<ContainerComponent>();
			foreach (Guid id in containerComponent.ChildEntityIds)
			{
				Entity item = entity.GetEntityByUniqueId(id);
				EntityModel entityData = GetItemModel(item, options);
				inventory.Add(entityData);
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
			foreach (Guid entityId in inventory.ChildEntityIds)
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
		public static EntityModel GetItemModel(this Entity itemEntity, LoadOptions[] options)
		{
			bool recursive = options.Contains(LoadOptions.Recursive);

			EntityWorld world = itemEntity.EntityWorld;
			EntityDataComponent entityData = itemEntity.GetComponent<EntityDataComponent>();
			EntityModel entityModel = SaturateBasicItemModel(itemEntity, entityData, options);

			// If a container, get its contents, too.
			if (!recursive)
				return entityModel;

			ContainerComponent containerComponent = itemEntity.GetComponent<ContainerComponent>();
			if (containerComponent != null
				&& containerComponent.ChildEntityIds != null)
			{
				
				entityModel.Entities = new List<EntityModel>();

				foreach (Guid childId in containerComponent.ChildEntityIds)
				{
					Entity childEntity = world.GetEntityByUniqueId(childId);
					EntityModel childEntityModel = GetItemModel(childEntity, options);
					entityModel.Entities.Add(childEntityModel);
				}
			}

			return entityModel;
		}


		#region Helper Methods
		private static EntityModel SaturateBasicItemModel(
			Entity itemEntity,
			EntityDataComponent entityData,
			LoadOptions[] options)
		{
			bool toLowercase = options.Contains(LoadOptions.LowercaseNames);

			EntityWorld world = itemEntity.EntityWorld;
			EntityDataCache entityDatas = WorldCache.GetCache(world).EntityDatas;
			EntityModel entityModel = new EntityModel();

			// Get basic item details.
			if (toLowercase)
				entityModel.Name = entityData.Name.ToLower();
			else
				entityModel.Name = entityData.Name;
			entityModel.Id = itemEntity.UniqueId;
			// entityModel.Type = entityDatas.GetBasePrototype(itemEntity).Name;

			// Get quantity.
			StackComponent stack = itemEntity.GetComponent<StackComponent>();
			if (stack == null)
				entityModel.Quantity = 1;
			else
				entityModel.Quantity = stack.Quantity;

			return entityModel;
		}
		#endregion
	}
}