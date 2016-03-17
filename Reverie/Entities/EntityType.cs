﻿namespace Reverie.Entities
{
	using System.Collections.Generic;


	public class EntityType
	{
		public static readonly EntityType None = new EntityType(0, "None");
		public static readonly EntityType Creature = new EntityType(1, "Creature");
		public static readonly EntityType Junk = new EntityType(2, "Junk");
		public static readonly EntityType Container = new EntityType(3, "Container");
		public static readonly List<EntityType> AllTypes =
			new List<EntityType>
			{
				None,
				Creature,
				Junk,
				Container
			};


		#region Fields
		private readonly int value;
		private readonly string name;
		#endregion


		#region Constructors
		public EntityType(int value, string name)
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


		public int Value
		{
			get { return this.value; }
		}
		#endregion


		#region Operators
		public static implicit operator int(EntityType entityType)
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