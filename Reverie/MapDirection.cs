namespace Reverie
{
	using System.Collections.Generic;


	/// <summary>
	/// Possible exits for a room/map node.
	/// </summary>
	public enum MapDirection
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
		Down = 512
	}


	public static class MapDirectionExtensions
	{
		public static IReadOnlyCollection<string> ToStrings(this IEnumerable<MapDirection> roomExits)
		{
			List<string> roomExitStrings = new List<string>();
			foreach (MapDirection roomExit in roomExits)
			{
				roomExitStrings.Add(roomExit.ToString());
			}

			return roomExitStrings.AsReadOnly();
		}
	}
}