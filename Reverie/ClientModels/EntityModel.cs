namespace Reverie.ClientModels
{
	using System;
	using System.Collections.Generic;
	using Newtonsoft.Json;
	using PrimitiveEngine;
	using PrimitiveEngine.Components;
	using Reverie.Components;


	public class EntityModel
	{
		#region Fields
		private Entity entity;
		#endregion


		#region Constructors
		public EntityModel() {}


		public EntityModel(Entity entity)
		{
			this.entity = entity;
			this.Id = entity.UniqueId;
			SaturateEntityDetails();
		}
		#endregion


		#region Properties
		public IList<string> Components { get; set; }
		public int? CurrentHealth { get; set; }
		public int? CurrentMemory { get; set; }
		public int? CurrentStorage { get; set; }
		public string Description { get; set; }
		public IList<EntityModel> Entities { get; set; }


		[JsonIgnore]
		public Entity Entity
		{
			get { return this.entity; }
			set { this.entity = value; }
		}


		public Guid Id { get; set; }
		public int? MaxHealth { get; set; }
		public int? MaxMemory { get; set; }
		public int? MaxQuantity { get; set; }
		public int? MaxStorage { get; set; }
		public string Name { get; set; }
		public int? Quantity { get; set; }
		#endregion


		public EntityModel SaturateComponentDetails()
		{
			List<string> components = new List<string>();
			Bag<Component> componentsBag = entity.EntityWorld.EntityManager.GetComponents(this.entity);
			foreach (Component component in componentsBag)
			{
				components.Add(component.GetType().Name);
			}
			this.Components = components.AsReadOnly();

			return this;
		}


		public EntityModel SaturateContainerDetails(
			bool recurse = false,
			bool includeComponents = true)
		{
			return this.SaturateContainerDetails(this.entity, recurse, includeComponents);
		}


		public EntityModel SaturateCreatureDetails()
		{
			Creature creature = this.entity.GetComponent<Creature>();
			if (creature == null)
				return this;

			this.CurrentHealth = creature.CurrentHealth;
			this.CurrentMemory = creature.CurrentMemory;
			this.CurrentStorage = creature.CurrentStorage;
			this.MaxHealth = creature.MaxHealth;
			this.MaxMemory = creature.MaxMemory;
			this.MaxStorage = creature.MaxStorage;

			return this;
		}


		public EntityModel SaturateEntityDetails()
		{
			EntityDetails entityDetails = this.entity.GetComponent<EntityDetails>();
			if (entityDetails == null)
				return this;

			this.Name = entityDetails.Name;
			this.Description = entityDetails.Description;

			return this;
		}


		public EntityModel SaturateStackDetails()
		{
			EntityStack stack = this.entity.GetComponent<EntityStack>();
			if (stack == null)
			{
				this.Quantity = 1;
				this.MaxQuantity = 1;
				return this;
			}
			
			this.Quantity = stack.Quantity;
			this.MaxQuantity = stack.MaxQuantity;

			return this;
		}


		#region Helper Methods
		private EntityModel SaturateContainerDetails(
			Entity entity, 
			bool recurse,
			bool includeComponents)
		{
			Container container = entity.GetComponent<Container>();
			if (container == null)
				return this;

			List<EntityModel> childModels = new List<EntityModel>();
			IReadOnlyCollection<Entity> children = container.GetChildEntities();
			foreach (Entity child in children)
			{
				EntityModel childModel = new EntityModel(child)
					.SaturateStackDetails();
				if (includeComponents)
					childModel.SaturateComponentDetails();
				if (recurse)
					childModel.SaturateContainerDetails(
						child, 
						recurse, 
						includeComponents);
				childModels.Add(childModel);
			}
			this.Entities = childModels;

			return this;
		}
		#endregion
	}
}