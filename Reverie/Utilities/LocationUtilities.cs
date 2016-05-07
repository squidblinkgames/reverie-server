namespace Reverie.Utilities
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using PrimitiveEngine;
	using Reverie.Components;


	public static class LocationUtilities
	{
		public static IntegerVector3 GetEasternCoordinate(this Location location)
		{
			return new IntegerVector3(location.X + 1, location.Y, location.Z);
		}


		public static IReadOnlyList<MapDirection> GetExits(this Location location)
		{
			// TODO: Look for obstacles at each exit.

			Map map = location.GetMap();
			List<MapDirection> exits = new List<MapDirection>();

			if (map.Nodes.ContainsKey(location.GetNorthernCoordinate()))
				exits.Add(MapDirection.North);
			if (map.Nodes.ContainsKey(location.GetNorthEasternCoordinate()))
				exits.Add(MapDirection.NorthEast);
			if (map.Nodes.ContainsKey(location.GetEasternCoordinate()))
				exits.Add(MapDirection.East);
			if (map.Nodes.ContainsKey(location.GetSouthEasternCoordinate()))
				exits.Add(MapDirection.SouthEast);
			if (map.Nodes.ContainsKey(location.GetSouthernCoordinate()))
				exits.Add(MapDirection.South);
			if (map.Nodes.ContainsKey(location.GetSouthWesternCoordinate()))
				exits.Add(MapDirection.SouthWest);
			if (map.Nodes.ContainsKey(location.GetWesternCoordinate()))
				exits.Add(MapDirection.West);
			if (map.Nodes.ContainsKey(location.GetNorthWesternCoordinate()))
				exits.Add(MapDirection.NorthWest);
			if (map.Nodes.ContainsKey(location.GetUpperCoordinate()))
				exits.Add(MapDirection.Up);
			if (map.Nodes.ContainsKey(location.GetLowerCoordinate()))
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


		public static IntegerVector3 GetLowerCoordinate(this Location location)
		{
			return new IntegerVector3(location.X, location.Y, location.Z - 1);
		}


		public static Map GetMap(this Location location)
		{
			EntityWorld entityWorld = location.EntityWorld;
			Map map = entityWorld
				.GetEntityByUniqueId(location.MapId)
				.GetComponent<Map>();

			return map;
		}


		public static IntegerVector3 GetNorthEasternCoordinate(this Location location)
		{
			return new IntegerVector3(location.X + 1, location.Y + 1, location.Z);
		}


		public static IntegerVector3 GetNorthernCoordinate(this Location location)
		{
			return new IntegerVector3(location.X, location.Y + 1, location.Z);
		}


		public static IntegerVector3 GetNorthWesternCoordinate(this Location location)
		{
			return new IntegerVector3(location.X - 1, location.Y + 1, location.Z);
		}


		public static IntegerVector3 GetPosition(this Entity entity)
		{
			Location location = entity.GetComponent<Location>();
			if (location == null)
				return null;

			return location.Position;
		}


		public static IntegerVector3 GetSouthEasternCoordinate(this Location location)
		{
			return new IntegerVector3(location.X + 1, location.Y - 1, location.Z);
		}


		public static IntegerVector3 GetSouthernCoordinate(this Location location)
		{
			return new IntegerVector3(location.X, location.Y - 1, location.Z);
		}


		public static IntegerVector3 GetSouthWesternCoordinate(this Location location)
		{
			return new IntegerVector3(location.X - 1, location.Y - 1, location.Z);
		}


		public static IntegerVector3 GetUpperCoordinate(this Location location)
		{
			return new IntegerVector3(location.X, location.Y, location.Z + 1);
		}


		public static IntegerVector3 GetWesternCoordinate(this Location location)
		{
			return new IntegerVector3(location.X - 1, location.Y, location.Z);
		}


		public static bool Move(this Entity entity, MapDirection direction)
		{
			Location location = entity.GetComponent<Location>();
			if (location == null)
				throw new ArgumentException("Entity does not have a Location component.");

			IReadOnlyList<MapDirection> exits = location.GetExits();
			if (!exits.Contains(direction))
				return false;

			switch (direction)
			{
				default:
				case MapDirection.None:
					break;
				case MapDirection.North:
					location.Position = location.GetNorthernCoordinate();
					break;
				case MapDirection.NorthEast:
					location.Position = location.GetNorthEasternCoordinate();
					break;
				case MapDirection.East:
					location.Position = location.GetEasternCoordinate();
					break;
				case MapDirection.SouthEast:
					location.Position = location.GetSouthEasternCoordinate();
					break;
				case MapDirection.South:
					location.Position = location.GetSouthernCoordinate();
					break;
				case MapDirection.SouthWest:
					location.Position = location.GetSouthWesternCoordinate();
					break;
				case MapDirection.West:
					location.Position = location.GetWesternCoordinate();
					break;
				case MapDirection.NorthWest:
					location.Position = location.GetNorthWesternCoordinate();
					break;
				case MapDirection.Up:
					location.Position = location.GetUpperCoordinate();
					break;
				case MapDirection.Down:
					location.Position = location.GetLowerCoordinate();
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