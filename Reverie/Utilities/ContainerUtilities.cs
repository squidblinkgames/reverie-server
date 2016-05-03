namespace Reverie.Utilities
{
	using System.Collections.Generic;
	using PrimitiveEngine;
	using Reverie.Components;


	public static class ContainerUtilities
	{
		public static IReadOnlyCollection<Entity> GetChildEntities(this Entity entity)
		{
			Container container = entity.GetComponent<Container>();
			if (container == null)
				return null;

			return container.GetChildEntities();
		}
	}
}