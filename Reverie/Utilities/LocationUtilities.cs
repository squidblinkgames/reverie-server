namespace Reverie.Utilities
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using PrimitiveEngine;
	using Reverie.Components;


	public static class LocationUtilities
	{
		public static IReadOnlyList<MapDirection> GetExits(this Location location)
		{
			// TODO: Look for obstacles at each exit.

			Map map = location.Map;
			List<MapDirection> exits = new List<MapDirection>();

			if (map.Nodes.ContainsKey(location.North))
				exits.Add(MapDirection.North);
			if (map.Nodes.ContainsKey(location.NorthEast))
				exits.Add(MapDirection.NorthEast);
			if (map.Nodes.ContainsKey(location.East))
				exits.Add(MapDirection.East);
			if (map.Nodes.ContainsKey(location.SouthEast))
				exits.Add(MapDirection.SouthEast);
			if (map.Nodes.ContainsKey(location.South))
				exits.Add(MapDirection.South);
			if (map.Nodes.ContainsKey(location.SouthWest))
				exits.Add(MapDirection.SouthWest);
			if (map.Nodes.ContainsKey(location.West))
				exits.Add(MapDirection.West);
			if (map.Nodes.ContainsKey(location.NorthWest))
				exits.Add(MapDirection.NorthWest);
			if (map.Nodes.ContainsKey(location.Up))
				exits.Add(MapDirection.Up);
			if (map.Nodes.ContainsKey(location.Down))
				exits.Add(MapDirection.Down);

			return exits.AsReadOnly();
		}


		public static IReadOnlyList<MapDirection> GetExits(this Entity entity)
		{
			Location location = entity.GetComponent<Location>();
			if (location == null)
				throw new ArgumentException("Entity does not have a Location component.");

			return location.GetExits();
		}


		public static IntegerVector3 GetPosition(this Entity entity)
		{
			Location location = entity.GetComponent<Location>();
			if (location == null)
				return null;

			return location.Position;
		}


		public static bool Move(this Entity entity, MapDirection direction)
		{
			Location location = entity.GetComponent<Location>();
			if (location == null)
				throw new ArgumentException("Entity does not have a Location component.");

			IReadOnlyList<MapDirection> exits = location.GetExits();
			if (!exits.Contains(direction))
				return false;

			// TODO: Need to check for change of container?

			switch (direction)
			{
				default:
				case MapDirection.None:
					break;
				case MapDirection.North:
					location.Position = location.North;
					break;
				case MapDirection.NorthEast:
					location.Position = location.NorthEast;
					break;
				case MapDirection.East:
					location.Position = location.East;
					break;
				case MapDirection.SouthEast:
					location.Position = location.SouthEast;
					break;
				case MapDirection.South:
					location.Position = location.South;
					break;
				case MapDirection.SouthWest:
					location.Position = location.SouthWest;
					break;
				case MapDirection.West:
					location.Position = location.West;
					break;
				case MapDirection.NorthWest:
					location.Position = location.NorthWest;
					break;
				case MapDirection.Up:
					location.Position = location.Up;
					break;
				case MapDirection.Down:
					location.Position = location.Down;
					break;
			}

			return true;
		}


		public static IntegerVector3 SetPosition(
			this Entity entity,
			int x,
			int y,
			int z)
		{
			Location location = entity.GetComponent<Location>();
			if (location == null)
				return null;

			location.Position = new IntegerVector3(x, y, z);

			return location.Position;
		}
	}
}