namespace Reverie.Utilities
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using PrimitiveEngine;
	using Reverie.Components;
	using Reverie.Maps;
	using Reverie.Cache;
	using Reverie.Models;


	public static class EntityExtensions
	{
		public static Entity FindByName(this IEnumerable<Entity> entityList, string expression)
		{
			// TODO: Parse indexes for finding specific items out of duplicates.
			return entityList.FirstOrDefault(
				item => item
							.GetName()
							.ToLower()
							.Equals(expression));
		}


		public static IReadOnlyCollection<Entity> GetEntitiesFromWorld(
			this IEnumerable<Guid> entityIds,
			EntityWorld entityWorld)
		{
			List<Entity> entities = new List<Entity>();
			foreach (Guid entityId in entityIds)
			{
				if (entityId == default(Guid))
					continue;
				Entity entity = entityWorld.GetEntityByUniqueId(entityId);
				if (entity != null)
					entities.Add(entity);
			}

			return entities;
		}


		/// <summary>
		/// Gets an item model representation of an entity.
		/// </summary>
		/// <param name="itemEntity">The item entity.</param>
		/// <returns>The item model.</returns>
		public static EntityModel GetItemModel(
			this Entity itemEntity,
			bool recursive,
			bool toLowercase)
		{
			EntityWorld world = itemEntity.EntityWorld;
			EntityData entityData = itemEntity.GetComponent<EntityData>();
			EntityModel entityModel = SaturateBasicItemModel(itemEntity, entityData, toLowercase);

			// If a container, get its contents, too.
			if (!recursive)
				return entityModel;

			Container container = itemEntity.GetComponent<Container>();
			if (container != null
				&& container.ChildEntityIds != null)
			{
				entityModel.Entities = new List<EntityModel>();

				foreach (Guid childId in container.ChildEntityIds)
				{
					Entity childEntity = world.GetEntityByUniqueId(childId);
					EntityModel childEntityModel = GetItemModel(childEntity, recursive, toLowercase);
					entityModel.Entities.Add(childEntityModel);
				}
			}

			return entityModel;
		}


		public static MapNode GetMapNode(this Entity entity)
		{
			Location location = entity.GetComponent<Location>();
			if (location == null)
				return null;

			MapCache maps = WorldCache.GetCache(entity.EntityWorld).Maps;
			Map map = maps[location.Map];

			return map[location.Position];
		}


		public static string GetName(this Entity entity)
		{
			EntityData entityData = entity.GetComponent<EntityData>();
			if (entityData == null)
				return null;

			return entityData.Name;
		}


		public static IntegerVector3 GetPosition(this Entity entity)
		{
			Location location = entity.GetComponent<Location>();
			if (location == null)
				return null;

			return location.Position;
		}


		public static IReadOnlyCollection<Guid> GetUniqueIds(this IEnumerable<Entity> entities)
		{
			List<Guid> entityIds = new List<Guid>();
			foreach (Entity entity in entities)
			{
				entityIds.Add(entity.UniqueId);
			}

			return entityIds.AsReadOnly();
		}


		#region Helper Methods
		private static EntityModel SaturateBasicItemModel(
			Entity itemEntity,
			EntityData entityData,
			bool toLowercase)
		{
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
			EntityStack entityStack = itemEntity.GetComponent<EntityStack>();
			if (entityStack == null)
				entityModel.Quantity = 1;
			else
				entityModel.Quantity = entityStack.Quantity;

			return entityModel;
		}
		#endregion
	}
}