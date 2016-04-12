namespace Reverie.Components
{
	using System;
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
		private ValueRange<int> health;
		private ValueRange<int> memory;
		private ValueRange<int> storage;
		private IList<Tuple<string, long?>> parts;
		#endregion


		#region Constructors
		public CreatureComponent()
		{
			this.health = new ValueRange<int>();
			this.memory = new ValueRange<int>();
			this.storage = new ValueRange<int>();
			this.parts = new List<Tuple<string, long?>>();
		}


		public CreatureComponent(IEnumerable<Tuple<string, long?>> parts)
		{
			this.parts = new List<Tuple<string, long?>>(parts);
		}
		#endregion


		#region Properties
		public int CurrentHealth
		{
			get { return this.health.Value; }
			set { this.health.Value = value; }
		}


		public int CurrentMemory
		{
			get { return this.memory.Value; }
			set { this.memory.Value = value; }
		}


		public int CurrentStorage
		{
			get { return this.storage.Value; }
			set { this.storage.Value = value; }
		}


		public int MaxHealth
		{
			get { return this.health.Maximum; }
			set { this.health.Maximum = value; }
		}


		public int MaxMemory
		{
			get { return this.memory.Maximum; }
			set { this.memory.Maximum = value; }
		}


		public int MaxStorage
		{
			get { return this.storage.Maximum; }
			set { this.storage.Maximum = value; }
		}


		public IReadOnlyCollection<Tuple<string, long?>> Parts
		{
			get { return new ReadOnlyCollection<Tuple<string, long?>>(this.parts); }
		}
		#endregion


		public CreatureComponent AddPart(
			string part,
			long? equippedItemId = null)
		{
			if (this.parts == null)
				this.parts = new List<Tuple<string, long?>>();
			this.parts.Add(new Tuple<string, long?>(part, equippedItemId));

			return this;
		}


		public bool Equip(string part, long itemId)
		{
			Tuple<string, long?> freeSlot = new Tuple<string, long?>(part, null);
			if (!this.parts.Contains(freeSlot))
				return false;

			this.parts.Remove(freeSlot);
			this.parts.Add(new Tuple<string, long?>(part, itemId));

			return true;
		}


		public bool Unequip(string part, long equippedItemId)
		{
			Tuple<string, long?> equippedSlot = new Tuple<string, long?>(part, equippedItemId);
			if (!this.parts.Contains(equippedSlot))
				return false;

			this.parts.Remove(equippedSlot);

			return true;
		}
	}
}