namespace Reverie.Cache
{
	using System;
	using System.Collections.Generic;
	using PrimitiveEngine;


	public static class WorldCache
	{
		private readonly static Dictionary<EntityWorld, ReverieState> cache;


		#region Constructors
		static WorldCache()
		{
			cache = new Dictionary<EntityWorld, ReverieState>();
		}
		#endregion


		public static ReverieState CreateCache(EntityWorld world)
		{
			if (cache.ContainsKey(world))
				throw new ArgumentException("New world key already exists in WorldCache.");

			ReverieState newState = new ReverieState();
			cache.Add(world, newState);

			return newState;
		}


		public static ReverieState GetCache(EntityWorld world)
		{
			if (!cache.ContainsKey(world))
				throw new ArgumentException("World key not found in WorldCache.");

			return cache[world];
		}


		// TODO: Method to flush cache to database.
		// TODO: Method to read cache from database.
	}
}