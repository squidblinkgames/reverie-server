namespace Reverie.Items
{
	using System.Collections.Generic;
	using System.Linq;
	using PrimitiveEngine.Artemis;
	using Reverie.Entities;
	using Reverie.Items.Components;
	using Reverie.Items.Models;
	using Reverie.State;


	public static class Inventory
	{
		#region Enums
		public enum LoadOptions
		{
			Recursive,
			Flatten
		}
		#endregion


		/// <summary>
		/// Gets the inventory of an Entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>A list of item models representing inventory contents.</returns>
		public static List<ContainerModel> GetContainerContents(
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
		public static ContainerModel GetItemModel(Entity itemEntity, bool recursive)
		{
			EntityWorld world = itemEntity.EntityWorld;
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
					Entity childEntity = world.GetEntityByUniqueId(childId);
					ContainerModel childContainerModel = GetItemModel(childEntity, recursive);
					containerModel.ItemIds.Add(childContainerModel.Id);
					containerModel.Entities.Add(childContainerModel);
				}
			}

			return containerModel;
		}


		#region Helper Methods
		private static ContainerModel SaturateBasicItemModel(Entity itemEntity, Prototype prototype)
		{
			EntityWorld world = itemEntity.EntityWorld;
			PrototypeCache prototypes = WorldCache.GetCacheForWorld(world).Prototypes;
			ContainerModel containerModel = new ContainerModel();

			// Get basic item details.
			containerModel.Id = itemEntity.UniqueId;
			containerModel.PrototypeId = prototype.Id;
			containerModel.Name = prototype.Name;
			containerModel.Type = prototypes.GetBasePrototype(itemEntity).Name;

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