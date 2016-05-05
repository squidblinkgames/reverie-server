namespace Reverie.Utilities
{
	using PrimitiveEngine;
	using Reverie.Components;


	public static class LocationUtilities
	{
		public static IntegerVector3 GetPosition(this Entity entity)
		{
			Location location = entity.GetComponent<Location>();
			if (location == null)
				return null;

			return location.Position;
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