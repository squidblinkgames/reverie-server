namespace Reverie.Entities.Components
{
	using System.Collections.Generic;
	using PrimitiveEngine;
	using Artemis;


	public class Prototype : IComponent
	{
		public const string Key = "Prototype";


		#region Fields
		private readonly int id;
		private readonly string name;
		private readonly Dictionary<string, DynamicValue> properties;
		private readonly int? parentPrototypeId;
		#endregion


		#region Constructors
		public Prototype(
			int id,
			string name,
			Dictionary<string, DynamicValue> properties,
			int? parentPrototypeId = null)
		{
			this.id = id;
			this.name = name;
			this.properties = properties;
			this.parentPrototypeId = parentPrototypeId;
		}
		#endregion


		#region Properties
		public int Id
		{
			get { return this.id; }
		}


		public string Name
		{
			get { return this.name; }
		}


		public int? ParentPrototypeId
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