namespace Reverie.Components
{
	using System;
	using System.Collections.Generic;
	using PrimitiveEngine;
	using PrimitiveEngine.Components;
	using Reverie.Cache;


	public class EntityData : Component
	{
		#region Fields
		private readonly Guid id;
		private readonly string name;
		private readonly string description;
		private readonly Dictionary<string, DynamicValue> properties;
		private readonly Guid parentPrototypeId;
		#endregion


		#region Constructors
		public EntityData(
			Guid id,
			string name,
			string description,
			Dictionary<string, DynamicValue> properties,
			Guid parentPrototypeId = default(Guid))
		{
			this.id = id;
			this.name = name;
			this.description = description;
			this.properties = properties;
			this.parentPrototypeId = parentPrototypeId;
		}
		#endregion


		#region Properties
		public string Description
		{
			get { return this.description; }
		}


		public Guid Id
		{
			get { return this.id; }
		}


		public string Name
		{
			get { return this.name; }
		}


		public Guid ParentPrototypeId
		{
			get { return this.parentPrototypeId; }
		}


		public Dictionary<string, DynamicValue> Properties
		{
			get { return this.properties; }
		}
		#endregion


		#region Helper Methods

		//TODO
		private void MergeAllParentProperties(
			EntityWorld world,
			EntityData parentEntityData)
		{
			foreach (KeyValuePair<string, DynamicValue> parentProperty in parentEntityData.properties)
			{
				if (this.properties.ContainsKey(parentProperty.Key))
					continue;
				this.properties.Add(parentProperty.Key, parentProperty.Value);
			}

			if (parentEntityData.parentPrototypeId != null)
			{
				parentEntityData =
					WorldCache.GetCache(world).EntityDatas[parentEntityData.parentPrototypeId];
				MergeAllParentProperties(world, parentEntityData);
			}
		}
		#endregion
	}
}