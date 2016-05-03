namespace Reverie.Extensions
{
	using System;
	using System.Collections.Generic;
	using PrimitiveEngine;


	public static class EntityExtensions
	{
		/// <summary>
		/// Get a collection of Entities from a collection of unique Entity id's.
		/// </summary>
		/// <param name="entityIds"></param>
		/// <param name="entityWorld"></param>
		/// <returns></returns>
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


		public static IReadOnlyCollection<Guid> GetUniqueIds(
			this IEnumerable<Entity> entities)
		{
			List<Guid> entityIds = new List<Guid>();
			foreach (Entity entity in entities)
			{
				entityIds.Add(entity.UniqueId);
			}

			return entityIds.AsReadOnly();
		}
	}
}