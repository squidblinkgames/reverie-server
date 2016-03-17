namespace Reverie.Items.Models
{
	using System.Collections.Generic;


	public class ItemModel
	{
		#region Properties
		public int Id { get; set; }
		public int PrototypeId { get; set; }
		public int? Quantity { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public List<ItemModel> Items { get; set; }
		public List<int> ItemIds { get; set; } 
		#endregion
	}
}
