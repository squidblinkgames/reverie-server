namespace Reverie.Entities
{
	using System.Collections.Generic;
	using PrimitiveEngine.Artemis;
	using PrimitiveEngine;


	public class Prototype : IComponent
	{
		public const string Key = "Prototype";


		#region Fields
		private readonly long id;
		private readonly string name;
		private readonly Dictionary<string, DynamicValue> properties;
		private readonly long? parentPrototypeId;
		#endregion


		#region Constructors
		public Prototype(
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
	}
}