namespace Reverie.Models
{
	using System.Collections.Generic;
	using PrimitiveEngine;
	using Reverie.Components;
	using Reverie.Utilities;


	public class EntityModel
	{
		#region Constructors
		public EntityModel() {}


		public EntityModel(Entity entity)
		{
			this.Id = entity.UniqueId;
			SaturateEntityDetails(entity);
			SaturateCreatureDetails(entity);
			SaturateContainerDetails(entity);
		}
		#endregion


		#region Properties
		public int? CurrentHealth { get; set; }
		public int? CurrentMemory { get; set; }
		public int? CurrentStorage { get; set; }
		public long DataId { get; set; }
		public string Description { get; set; }
		public List<EntityModel> Entities { get; set; }
		public List<long> EntityIds { get; set; }
		public long Id { get; set; }
		public int? MaxHealth { get; set; }
		public int? MaxMemory { get; set; }
		public int? MaxStorage { get; set; }
		public string Name { get; set; }
		public int? Quantity { get; set; }
		public string Type { get; set; }
		#endregion


		#region Helper Methods
		private void SaturateContainerDetails(Entity entity)
		{
			ContainerComponent container = entity.GetComponent<ContainerComponent>();
			if (container == null)
				return;

			this.Entities = Inventory.GetContainerContents(entity, Inventory.LoadOptions.Recursive);
			this.EntityIds = new List<long>(container.ChildEntityIds);
		}


		private void SaturateCreatureDetails(Entity entity)
		{
			CreatureComponent creature = entity.GetComponent<CreatureComponent>();
			if (creature == null)
				return;

			this.CurrentHealth = creature.CurrentHealth;
			this.CurrentMemory = creature.CurrentMemory;
			this.CurrentStorage = creature.CurrentStorage;
			this.MaxHealth = creature.MaxHealth;
			this.MaxMemory = creature.MaxMemory;
			this.MaxStorage = creature.MaxStorage;
		}


		private void SaturateEntityDetails(Entity entity)
		{
			EntityDataComponent entityData = entity.GetComponent<EntityDataComponent>();
			if (entityData == null)
				return;

			this.DataId = entityData.Id;
			this.Name = entityData.Name;
			this.Description = entityData.Description;
			this.Type = "TODO: FILL ME IN";
		}
		#endregion
	}
}