namespace Reverie.Utilities
{
	using System.Collections.Generic;
	using System.Linq;
	using PrimitiveEngine;
	using Reverie.Components;
	using Reverie.Maps;
	using Reverie.Cache;


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
			this IEnumerable<long?> entityIds,
			EntityWorld entityWorld)
		{
			List<Entity> entities = new List<Entity>();
			foreach (long? entityId in entityIds)
			{
				if (entityId == null)
					continue;
				Entity entity = entityWorld.GetEntityByUniqueId((long)entityId);
				if (entity != null)
					entities.Add(entity);
			}

			return entities;
		}


		public static MapNode GetMapNode(this Entity entity)
		{
			LocationComponent location = entity.GetComponent<LocationComponent>();
			if (location == null)
				return null;

			MapCache maps = WorldCache.GetCache(entity.EntityWorld).Maps;
			Map map = maps[location.Map];

			return map[location.Position];
		}


		public static string GetName(this Entity entity)
		{
			EntityDataComponent entityData = entity.GetComponent<EntityDataComponent>();
			if (entityData == null)
				return null;

			return entityData.Name;
		}


		public static IntegerVector3 GetPosition(this Entity entity)
		{
			LocationComponent location = entity.GetComponent<LocationComponent>();
			if (location == null)
				return null;

			return location.Position;
		}


		public static IReadOnlyCollection<long> GetUniqueIds(this IEnumerable<Entity> entities)
		{
			List<long> entityIds = new List<long>();
			foreach (Entity entity in entities)
			{
				entityIds.Add(entity.UniqueId);
			}

			return entityIds.AsReadOnly();
		}
	}
}