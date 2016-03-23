namespace Reverie.Items.Systems
{
	using System.Collections.Generic;
	using System.Linq;
	using PrimitiveEngine.Artemis.Systems;
	using PrimitiveEngine.Artemis;
	using Reverie.Cache;
	using Reverie.Entities;
	using Reverie.Items.Components;
	using Reverie.Items.Models;


	[ArtemisEntitySystem]
	public class ContainerSystem : EntitySystem
	{
		#region Fields
		private PrototypeCache prototypeSystem;
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
		public PrototypeCache PrototypeSystem
		{
			get
			{
				if (this.prototypeSystem == null)
				{
					this.prototypeSystem =
						this.EntityWorld.BlackBoard.GetEntry<PrototypeCache>();
				}

				return this.prototypeSystem;
			}
		}
		#endregion


		/// <summary>
		/// Gets the inventory of an Entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>A list of item models representing inventory contents.</returns>
		public List<ContainerModel> GetContainerContents(
			Entity entity,
			params LoadOptions[] options)
		{
			List<ContainerModel> inventory = new List<ContainerModel>();
			bool recursive = options.Contains(LoadOptions.Recursive);

			Container container = entity.GetComponent<Container>();
			foreach (long id in container.ChildEntityIds)
			{
				Entity item = entity.GetEntityByUniqueId(id);
				ContainerModel containerData = GetItemModel(item, recursive);
				inventory.Add(containerData);
			}

			return inventory;
		}


		/// <summary>
		/// Gets an item model representation of an entity.
		/// </summary>
		/// <param name="itemEntity">The item entity.</param>
		/// <returns>The item model.</returns>
		public ContainerModel GetItemModel(Entity itemEntity, bool recursive)
		{
			Prototype prototype = itemEntity.GetComponent<Prototype>();
			ContainerModel containerModel = SaturateBasicItemModel(itemEntity, prototype);

			// If a container, get its contents, too.
			if (!recursive)
				return containerModel;

			Container container = itemEntity.GetComponent<Container>();
			if (container != null)
			{
				containerModel.ItemIds = new List<long>();
				containerModel.Entities = new List<ContainerModel>();

				foreach (long childId in container.ChildEntityIds)
				{
					Entity childEntity = this.GetEntityByUniqueId(childId);
					ContainerModel childContainerModel = GetItemModel(childEntity, recursive);
					containerModel.ItemIds.Add(childContainerModel.Id);
					containerModel.Entities.Add(childContainerModel);
				}
			}

			return containerModel;
		}


		#region Helper Methods
		private ContainerModel SaturateBasicItemModel(Entity itemEntity, Prototype prototype)
		{
			ContainerModel containerModel = new ContainerModel();

			// Get basic item details.
			containerModel.Id = itemEntity.UniqueId;
			containerModel.PrototypeId = prototype.Id;
			containerModel.Name = prototype.Name;
			containerModel.Type = this.PrototypeSystem.GetBasePrototype(itemEntity).Name;

			// Get quantity.
			Stackable stack = itemEntity.GetComponent<Stackable>();
			if (stack == null)
				containerModel.Quantity = 1;
			else
				containerModel.Quantity = stack.Quantity;

			return containerModel;
		}
		#endregion
	}
}