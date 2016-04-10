namespace Reverie.Entities
{
	using System.Collections.Generic;
	using PrimitiveEngine;
	using PrimitiveEngine.Components;
	using Reverie.State;


	public class PrototypeComponent : Component
	{
		#region Fields
		private readonly long id;
		private readonly string name;
		private readonly Dictionary<string, DynamicValue> properties;
		private readonly long? parentPrototypeId;
		#endregion


		#region Constructors
		public PrototypeComponent(
			long id,
			string name,
			Dictionary<string, DynamicValue> properties,
			long? parentPrototypeId = null)
		{
			this.id = id;
			this.name = name;
			this.properties = properties;
			this.parentPrototypeId = parentPrototypeId;
		}
		#endregion


		#region Properties
		public long Id
		{
			get { return this.id; }
		}


		public string Name
		{
			get { return this.name; }
		}


		public long? ParentPrototypeId
		{
			get { return this.parentPrototypeId; }
		}


		public Dictionary<string, DynamicValue> Properties
		{
			get { return this.properties; }
		}
		#endregion


		//TODO
		private void MergeAllParentProperties(
			EntityWorld world,
			PrototypeComponent parentPrototype)
		{
			foreach (KeyValuePair<string, DynamicValue> parentProperty in parentPrototype.properties)
			{
				if (this.properties.ContainsKey(parentProperty.Key))
					continue;
				this.properties.Add(parentProperty.Key, parentProperty.Value);
			}

			if (parentPrototype.parentPrototypeId != null)
			{
				parentPrototype =
					WorldCache.GetCache(world).Prototypes[parentPrototype.parentPrototypeId];
				MergeAllParentProperties(world, parentPrototype);
			}
		}
	}
}