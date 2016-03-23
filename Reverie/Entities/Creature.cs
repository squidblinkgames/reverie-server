namespace Reverie.Entities
{
	using System.Collections.Generic;


	public class Creature
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

		private Dictionary<string, long?> parts;
	}
}
