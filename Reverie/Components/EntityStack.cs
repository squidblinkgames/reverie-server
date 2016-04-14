namespace Reverie.Components
{
	using PrimitiveEngine.Components;


	public class EntityStack : Component
	{
		#region Fields
		private int quantity;
		private int maxQuantity;
		#endregion


		#region Constructors
		public EntityStack(int quantity, int maxQuantity)
		{
			this.quantity = quantity;
			this.maxQuantity = maxQuantity;
		}
		#endregion


		#region Properties
		public int MaxQuantity
		{
			get { return this.maxQuantity; }
		}


		public int Quantity
		{
			get { return this.quantity; }
		}
		#endregion



		/// <summary>
		/// Increases stack by the specified amount.
		/// </summary>
		/// <param name="amount">The amount to add to the stack.</param>
		/// <returns>The amount that was successfully added to the stack.</returns>
		public int Increase(int amount)
		{
			if (amount + this.quantity > this.maxQuantity)
				amount = this.maxQuantity - this.quantity;
			this.quantity += amount;
			
			return amount;
		}


		/// <summary>
		/// Decrease stack by the specified amount.
		/// </summary>
		/// <param name="amount">The amount to remove from the stack.</param>
		/// <returns>The amount that was successfully removed from the stack.</returns>
		public int Decrease(int amount)
		{
			if (this.quantity - amount < 0)
				amount = this.quantity;
			this.quantity -= amount;

			return amount;
		}
	}
}