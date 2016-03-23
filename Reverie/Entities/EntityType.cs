namespace Reverie.Entities
{
	using System.Collections.Generic;


	public class EntityType
	{
		public static readonly EntityType None = new EntityType(0, "None");
		public static readonly EntityType Creature = new EntityType(1, "Creature");
		public static readonly EntityType Container = new EntityType(2, "Container");
		public static readonly EntityType Consumable = new EntityType(3, "Consumable");
		public static readonly EntityType Weapon = new EntityType(4, "Weapon");
		public static readonly EntityType Apparel = new EntityType(5, "Apparel");

		public static readonly List<EntityType> AllTypes =
			new List<EntityType>
			{
				None,
				Creature,
				Container,
				Consumable,
				Weapon,
				Apparel
			};


		#region Fields
		private readonly long value;
		private readonly string name;
		#endregion


		#region Constructors
		public EntityType(long value, string name)
		{
			this.value = value;
			this.name = name;
		}
		#endregion


		#region Properties
		public string Name
		{
			get { return this.name; }
		}


		public long Value
		{
			get { return this.value; }
		}
		#endregion


		#region Operators
		public static implicit operator long(EntityType entityType)
		{
			return entityType.value;
		}


		public static implicit operator string(EntityType entityType)
		{
			return entityType.name;
		}
		#endregion


		public override string ToString()
		{
			return this.name;
		}
	}
}