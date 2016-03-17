namespace Reverie.Geography
{
	using System;


	/// <summary>
	/// Flags for holding directional information.
	/// </summary>
	[Flags]
	public enum RoomExits
	{
		None = 0,
		North = 1,
		South = 2,
		West = 4,
		East = 8,
		NorthWest = 16,
		NorthEast = 32,
		SouthWest = 64,
		SouthEast = 128,
		Up = 256,
		Down = 512,
		All = North | NorthEast | East | SouthEast | South | SouthWest | West | NorthWest | Up | Down
	}


	/// <summary>
	/// Extensions to simplify RoomExits flags manipulation.
	/// </summary>
	public static class RoomExitsUtilities
	{
		public static bool HasFlag(this RoomExits flags, RoomExits flag)
		{
			return (flags & flag) == flag;
		}


		public static RoomExits RotateClockwise(this RoomExits roomExit)
		{
			switch (roomExit)
			{
				default:
					return RoomExits.None;
				case RoomExits.North:
					return RoomExits.NorthEast;
				case RoomExits.NorthEast:
					return RoomExits.East;
				case RoomExits.East:
					return RoomExits.SouthEast;
				case RoomExits.SouthEast:
					return RoomExits.South;
				case RoomExits.South:
					return RoomExits.SouthWest;
				case RoomExits.SouthWest:
					return RoomExits.West;
				case RoomExits.West:
					return RoomExits.NorthWest;
				case RoomExits.NorthWest:
					return RoomExits.North;
			}
		}


		public static RoomExits RotateCounterClockwise(this RoomExits roomExit)
		{
			switch (roomExit)
			{
				default:
					return RoomExits.None;
				case RoomExits.North:
					return RoomExits.NorthWest;
				case RoomExits.NorthWest:
					return RoomExits.West;
				case RoomExits.West:
					return RoomExits.SouthWest;
				case RoomExits.SouthWest:
					return RoomExits.South;
				case RoomExits.South:
					return RoomExits.SouthEast;
				case RoomExits.SouthEast:
					return RoomExits.East;
				case RoomExits.East:
					return RoomExits.NorthEast;
				case RoomExits.NorthEast:
					return RoomExits.North;
			}
		}


		public static RoomExits SetFlag(this RoomExits flags, RoomExits flag)
		{
			flags |= flag;
			return flags;
		}


		public static RoomExits UnsetFlag(this RoomExits flags, RoomExits flag)
		{
			flags &= ~flag;
			return flags;
		}
	}
}