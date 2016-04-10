namespace Reverie.Utilities
{
	using System.Collections.Generic;
	using System.Linq;
	using PrimitiveEngine;
	using Reverie.Entities;
	using Reverie.Maps;
	using Reverie.State;


	public static class EntityExtensions
	{
		public static MapNodeComponent GetMapNode(this Entity entity)
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
			PrototypeComponent prototype = entity.GetComponent<PrototypeComponent>();
			if (prototype == null)
				return null;

			return prototype.Name;
		}


		public static IntegerVector3 GetPosition(this Entity entity)
		{
			LocationComponent location = entity.GetComponent<LocationComponent>();
			if (location == null)
				return null;

			return location.Position;
		}


		public static Entity FindByName(this IEnumerable<Entity> entityList, string expression)
		{
			// TODO: Parse indexes for finding specific items out of duplicates.
			return entityList.FirstOrDefault(
				item => item
							.GetName()
							.ToLower()
							.Equals(expression));
		}
	}
}