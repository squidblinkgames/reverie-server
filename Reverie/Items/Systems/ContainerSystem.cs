namespace Reverie.Items.Systems
{
	using System.Collections.Generic;
	using System.Linq;
	using Artemis;
	using Artemis.Attributes;
	using Artemis.Systems;
	using Reverie.Entities.Components;
	using Reverie.Entities.Systems;
	using Reverie.Items.Components;
	using Reverie.Items.Models;


	[ArtemisEntitySystem]
	
	public class ContainerSystem : EntitySystem
	{
		#region Fields
		private PrototypeSystem prototypeSystem;
		#endregion


		#region Enums
		public enum LoadOptions
		{
			Recursive,
			Flatten
		}
		#endregion


		#region Properties
		/// <summary>
		/// Gets the prototype system.
		/// </summary>
		/// <value>The prototype system.</value>
		public PrototypeSystem PrototypeSystem
		{
			get
			{
				if (this.prototypeSystem == null)
					this.prototypeSystem = this.GetSystem<PrototypeSystem>();
				return this.prototypeSystem;
			}
		}
		#endregion


		/// <summary>
		/// Gets the inventory of an Entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>A list of item models representing inventory contents.</returns>
		public List<ItemModel> GetContainerContents(
			Entity entity,
			params LoadOptions[] options)
		{
			List<ItemModel> inventory = new List<ItemModel>();
			bool recursive = options.Contains(LoadOptions.Recursive);

			Container container = entity.GetComponent<Container>();
			foreach (int id in container.ChildEntityIds)
			{
				Entity item = entity.GetEntity(id);
				ItemModel itemData = GetItemModel(item, recursive);
				inventory.Add(itemData);
			}

			return inventory;
		}


		/// <summary>
		/// Gets an item model representation of an entity.
		/// </summary>
		/// <param name="itemEntity">The item entity.</param>
		/// <returns>The item model.</returns>
		public ItemModel GetItemModel(Entity itemEntity, bool recursive)
		{
			Prototype prototype = itemEntity.GetComponent<Prototype>();
			ItemModel itemModel = SaturateBasicItemModel(itemEntity, prototype);

			// If a container, get its contents, too.
			if (!recursive)
				return itemModel;

			Container container = itemEntity.GetComponent<Container>();
			if (container != null)
			{
				itemModel.ItemIds = new List<int>();
				itemModel.Items = new List<ItemModel>();

				foreach (int childId in container.ChildEntityIds)
				{
					Entity childEntity = this.GetEntity(childId);
					ItemModel childItemModel = GetItemModel(childEntity, recursive);
					itemModel.ItemIds.Add(childItemModel.Id);
					itemModel.Items.Add(childItemModel);
				}
			}

			return itemModel;
		}


		#region Helper Methods
		private ItemModel SaturateBasicItemModel(Entity itemEntity, Prototype prototype)
		{
			ItemModel itemModel = new ItemModel();

			// Get basic item details.
			itemModel.Id = itemEntity.Id;
			itemModel.PrototypeId = prototype.Id;
			itemModel.Name = prototype.Name;
			itemModel.Type = this.PrototypeSystem.GetBasePrototype(itemEntity).Name;

			// Get quantity.
			Stackable stack = itemEntity.GetComponent<Stackable>();
			if (stack == null)
				itemModel.Quantity = 1;
			else
				itemModel.Quantity = stack.Quantity;

			return itemModel;
		}
		#endregion
	}
}