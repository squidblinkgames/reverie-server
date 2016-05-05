namespace Reverie.Utilities
{
	using PrimitiveEngine;
	using Reverie.Cache;
	using Reverie.Components;


	public static class MapUtilities
	{
		public static Entity GetRoom(this Entity entity)
		{
			Location location = entity.GetComponent<Location>();
			if (location == null)
				return null;

			MapCache maps = WorldCache.GetCache(entity.EntityWorld).Maps;
			Map map = maps[location.Map];

			return map[location.Position];
		}
	}
}