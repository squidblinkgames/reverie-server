namespace Reverie.Items.Models
{
	using System.Collections.Generic;


	public class ContainerModel
	{
		#region Properties
		public long Id { get; set; }
		public long PrototypeId { get; set; }
		public int? Quantity { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public List<ContainerModel> Entities { get; set; }
		public List<long> ItemIds { get; set; } 
		#endregion
	}
}
