namespace Reverie.Utilities
{
	using PrimitiveEngine;
	using Reverie.Components;


	public static class EntityStackUtilities
	{
		public static int Quantity(this Entity entity)
		{
			EntityStack stack = entity.GetComponent<EntityStack>();
			if (stack == null)
				return 1;

			return stack.Quantity;
		}


		public static int MaxQuantity(this Entity entity)
		{
			EntityStack stack = entity.GetComponent<EntityStack>();
			if (stack == null)
				return 1;

			return stack.MaxQuantity;
		}
	}
}
