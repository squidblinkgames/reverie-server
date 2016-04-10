namespace Reverie.Entities
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using PrimitiveEngine;
	using PrimitiveEngine.Components;


	public class CreatureComponent : Component
	{
		public const string Head = "Head";
		public const string Neck = "Neck";
		public const string Chest = "Chest";
		public const string Shoulder = "Shoulder";
		public const string Arm = "Arm";
		public const string Hand = "Hand";
		public const string Finger = "Finger";
		public const string Waist = "Waist";
		public const string Leg = "Leg";
		public const string Foot = "Foot";
		
		


		#region Fields
		private Dictionary<string, long?> parts;
		#endregion


		#region Constructors
		public CreatureComponent() {}


		public CreatureComponent(Dictionary<string, long?> parts)
		{
			this.parts = parts;
		}
		#endregion


		#region Properties
		public IReadOnlyDictionary<string, long?> Parts
		{
			get { return new ReadOnlyDictionary<string, long?>(this.parts); }
		}
		#endregion


		public CreatureComponent AddPart(string part)
		{
			if (this.parts == null)
				this.parts = new Dictionary<string, long?>();
			//this.parts.Add(part, null);

			return this;
		}
	}
}