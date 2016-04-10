namespace Reverie.Templates
{
	using System.Collections.Generic;
	using PrimitiveEngine;
	using Reverie.Components;
	using Reverie.Cache;
	using Reverie.Debug;


	[EntityTemplate(Name)]
	public class NewPlayerTemplate : IEntityTemplate
	{
		public const string Name = "NewPlayerTemplate";


		#region Fields
		private Dictionary<int, EntityDataComponent> prototypes;
		#endregion


		public Entity BuildEntity(Entity entity, EntityWorld entityWorld, params object[] args)
		{
			entity.AddComponent(new PlayerComponent());

			AddNewPlayerInventory(entity);
			AddCreatureComponent(entity);
			AddLocationComponent(entity);

			return entity;
		}


		#region Helper Methods
		private void AddLocationComponent(Entity entity)
		{
			LocationComponent locationComponent = new LocationComponent(
				MockWorld.StartMapName,
				0, 0, 0);
			entity.AddComponent(locationComponent);
		}


		private void AddCreatureComponent(Entity entity)
		{
			CreatureComponent creatureComponent = new CreatureComponent();
			creatureComponent.AddPart(CreatureComponent.Head);
			creatureComponent.AddPart(CreatureComponent.Neck);
			creatureComponent.AddPart(CreatureComponent.Chest);
			creatureComponent.AddPart(CreatureComponent.Shoulder);
			creatureComponent.AddPart(CreatureComponent.Shoulder);
			creatureComponent.AddPart(CreatureComponent.Arm);
			creatureComponent.AddPart(CreatureComponent.Arm);
			creatureComponent.AddPart(CreatureComponent.Hand);
			creatureComponent.AddPart(CreatureComponent.Hand);
			creatureComponent.AddPart(CreatureComponent.Finger);
			creatureComponent.AddPart(CreatureComponent.Finger);
			creatureComponent.AddPart(CreatureComponent.Waist);
			creatureComponent.AddPart(CreatureComponent.Leg);
			creatureComponent.AddPart(CreatureComponent.Leg);
			creatureComponent.AddPart(CreatureComponent.Foot);
			creatureComponent.AddPart(CreatureComponent.Foot);

			creatureComponent.MaxHealth = 100;
			creatureComponent.MaxMemory = 100;
			creatureComponent.MaxStorage = 100;
			creatureComponent.CurrentHealth = 100;
			creatureComponent.CurrentMemory = 100;
			creatureComponent.CurrentStorage = 100;

			entity.AddComponent(creatureComponent);
		}
		
		private void AddNewPlayerInventory(Entity entity)
		{
			EntityWorld gameWorld = entity.EntityWorld;
			PrototypeCache prototypes = WorldCache.GetCache(gameWorld).Prototypes;

			ContainerComponent inventory = new ContainerComponent(10);
			
			entity.AddComponent(inventory);

			Entity itemStack = gameWorld.CreateEntity();
			itemStack.AddComponent(new StackComponent(3, 3));
			itemStack.AddComponent(prototypes[EntityType.Consumable]);

			Entity itemSingle = gameWorld.CreateEntity();
			itemSingle.AddComponent(prototypes[EntityType.Consumable]);

			Entity bag = gameWorld.CreateEntity();
			ContainerComponent bagContainerComponent = new ContainerComponent(3);
			EntityDataComponent bagEntityData = new EntityDataComponent(
				prototypes.Count,
				"Bag",
				"Just some bag.",
				null,
				EntityType.Container);
			prototypes.Add(bagEntityData);
			bag.AddComponent(bagEntityData);
			bag.AddComponent(bagContainerComponent);
			bagContainerComponent.AddEntity(itemStack);

			Entity backpack = gameWorld.CreateEntity();
			ContainerComponent backpackContainerComponent = new ContainerComponent(3);
			EntityDataComponent backpackEntityData = new EntityDataComponent(
				prototypes.Count,
				"Backpack",
				"A weathered old backpack.",
				null,
				EntityType.Container);
			prototypes.Add(backpackEntityData);
			backpack.AddComponent(backpackEntityData);
			backpack.AddComponent(backpackContainerComponent);

			inventory.AddEntity(bag);
			inventory.AddEntity(backpack);
			inventory.AddEntity(itemSingle);
		}
		#endregion
	}
}