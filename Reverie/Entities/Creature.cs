namespace Reverie.Entities
{
	using System.Collections.Generic;
	using PrimitiveEngine.Artemis;


	public class Creature : IComponent
	{
		public const string Head = "Head";
		public const string Neck = "Neck";
		public const string Arm = "Arm";
		public const string Leg = "Leg";
		public const string Hand = "Hand";
		public const string Finger = "Finger";
		public const string Foot = "Foot";
		public const string Chest = "Chest";
		public const string Waist = "Waist";
		public const string Shoulder = "Shoulder";


		#region Fields
		private Dictionary<string, long?> parts;
		#endregion


		#region Constructors
		public Creature() {}


		public Creature(Dictionary<string, long?> parts)
		{
			this.parts = parts;
		}
		#endregion


		#region Properties
		public Dictionary<string, long?> Parts
		{
			get { return this.parts; }
		}
		#endregion
	}
}