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
	}
}