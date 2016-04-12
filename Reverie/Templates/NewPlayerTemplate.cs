namespace Reverie.Templates
{
	using System;
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
			AddEntityDataComponent(entity);

			return entity;
		}


		#region Helper Methods
		private void AddEntityDataComponent(Entity entity)
		{
			EntityDataComponent entityDataComponent = new EntityDataComponent(
				Guid.NewGuid(),
				"Another Player",
				"You see another player over there.",
				null);
			entity.AddComponent(entityDataComponent);
		}
		
		
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
			EntityDataCache entityDatas = WorldCache.GetCache(gameWorld).EntityDatas;

			ContainerComponent inventory = new ContainerComponent(10);
			
			entity.AddComponent(inventory);

			Entity itemStack = gameWorld.CreateEntity();
			itemStack.AddComponent(new StackComponent(3, 3));
			itemStack.AddComponent(entityDatas[EntityType.Consumable]);

			Entity itemSingle = gameWorld.CreateEntity();
			itemSingle.AddComponent(entityDatas[EntityType.Consumable]);

			Entity bag = gameWorld.CreateEntity();
			ContainerComponent bagContainerComponent = new ContainerComponent(3);
			EntityDataComponent bagEntityData = new EntityDataComponent(
				Guid.NewGuid(),
				"Bag",
				"Just some bag.",
				null,
				EntityType.Container);
			entityDatas.Add(bagEntityData);
			bag.AddComponent(bagEntityData);
			bag.AddComponent(bagContainerComponent);
			bagContainerComponent.AddEntity(itemStack);

			Entity backpack = gameWorld.CreateEntity();
			ContainerComponent backpackContainerComponent = new ContainerComponent(3);
			EntityDataComponent backpackEntityData = new EntityDataComponent(
				Guid.NewGuid(),
				"Backpack",
				"A weathered old backpack.",
				null,
				EntityType.Container);
			entityDatas.Add(backpackEntityData);
			backpack.AddComponent(backpackEntityData);
			backpack.AddComponent(backpackContainerComponent);

			inventory.AddEntity(bag);
			inventory.AddEntity(backpack);
			inventory.AddEntity(itemSingle);
		}
		#endregion
	}
}