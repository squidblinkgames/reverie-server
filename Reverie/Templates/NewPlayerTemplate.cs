namespace Reverie.Templates
{
	using System;
	using PrimitiveEngine;
	using Reverie.Components;
	using Reverie.Cache;
	using Reverie.Debug;


	[EntityTemplate(Name)]
	public class NewPlayerTemplate : IEntityTemplate
	{
		public const string Name = "NewPlayerTemplate";
		

		public Entity BuildEntity(Entity entity, EntityWorld entityWorld, params object[] args)
		{
			entity.AddComponent(new Player());

			AddNewPlayerInventory(entity);
			AddCreatureComponent(entity);
			AddLocationComponent(entity);
			AddEntityDataComponent(entity);

			return entity;
		}


		#region Helper Methods
		private void AddCreatureComponent(Entity entity)
		{
			Creature creature = new Creature();
			creature.AddPart(Creature.Head);
			creature.AddPart(Creature.Neck);
			creature.AddPart(Creature.Chest);
			creature.AddPart(Creature.Shoulder);
			creature.AddPart(Creature.Shoulder);
			creature.AddPart(Creature.Arm);
			creature.AddPart(Creature.Arm);
			creature.AddPart(Creature.Hand);
			creature.AddPart(Creature.Hand);
			creature.AddPart(Creature.Finger);
			creature.AddPart(Creature.Finger);
			creature.AddPart(Creature.Waist);
			creature.AddPart(Creature.Leg);
			creature.AddPart(Creature.Leg);
			creature.AddPart(Creature.Foot);
			creature.AddPart(Creature.Foot);

			creature.MaxHealth = 100;
			creature.MaxMemory = 100;
			creature.MaxStorage = 100;
			creature.CurrentHealth = 100;
			creature.CurrentMemory = 100;
			creature.CurrentStorage = 100;

			entity.AddComponent(creature);
		}


		private void AddEntityDataComponent(Entity entity)
		{
			EntityDetails entityDetails = new EntityDetails(
				Guid.NewGuid(),
				"Another Player",
				"You see another player over there.",
				null);
			entity.AddComponent(entityDetails);
		}


		private void AddLocationComponent(Entity entity)
		{
			Location location = new Location(
				MockWorld.StartMapName,
				0,
				0,
				0);
			entity.AddComponent(location);
		}


		private void AddNewPlayerInventory(Entity entity)
		{
			EntityWorld gameWorld = entity.EntityWorld;
			EntityDataCache entityDatas = WorldCache.GetCache(gameWorld).EntityDatas;

			Container inventory = new Container(10);

			entity.AddComponent(inventory);

			Entity itemStack = gameWorld.CreateEntity();
			itemStack.AddComponent(new EntityStack(3, 3));
			itemStack.AddComponent(entityDatas[EntityType.Consumable]);

			Entity itemSingle = gameWorld.CreateEntity();
			itemSingle.AddComponent(entityDatas[EntityType.Consumable]);

			Entity bag = gameWorld.CreateEntity();
			Container bagContainer = new Container(3);
			EntityDetails bagEntityDetails = new EntityDetails(
				Guid.NewGuid(),
				"Bag",
				"Just some bag.",
				null,
				EntityType.Container);
			entityDatas.Add(bagEntityDetails);
			bag.AddComponent(bagEntityDetails);
			bag.AddComponent(bagContainer);
			bagContainer.AddEntity(itemStack);

			Entity backpack = gameWorld.CreateEntity();
			Container backpackContainer = new Container(3);
			EntityDetails backpackEntityDetails = new EntityDetails(
				Guid.NewGuid(),
				"Backpack",
				"A weathered old backpack.",
				null,
				EntityType.Container);
			entityDatas.Add(backpackEntityDetails);
			backpack.AddComponent(backpackEntityDetails);
			backpack.AddComponent(backpackContainer);

			inventory.AddEntity(bag);
			inventory.AddEntity(backpack);
			inventory.AddEntity(itemSingle);
		}
		#endregion
	}
}