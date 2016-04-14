namespace Reverie.Components
{
	using System;
	using PrimitiveEngine.Components;


	public class Player : Component
	{
		public event Action<string> Updated;


		public void Update(string message)
		{
			Action<string> updated = this.Updated;
			if (updated != null)
				updated(message);

		}
	}
}
