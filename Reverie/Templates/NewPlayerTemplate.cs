namespace Reverie.Templates
{
	using System.Collections.Generic;
	using PrimitiveEngine;
	using Reverie.Entities;
	using Reverie.Items.Components;
	using Reverie.State;


	[EntityTemplate(Name)]
	public class NewPlayerTemplate : IEntityTemplate
	{
		public const string Name = "NewPlayerTemplate";


		#region Fields
		private Dictionary<int, PrototypeComponent> prototypes;
		#endregion


		public Entity BuildEntity(Entity entity, EntityWorld entityWorld, params object[] args)
		{
			entity.AddComponent(new PlayerComponent());

			AddNewPlayerInventory(entity);
			AddCreatureComponent(entity);

			return entity;
		}


		#region Helper Methods
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
			
			entity.AddComponent(creatureComponent);
		}
		
		private void AddNewPlayerInventory(Entity entity)
		{
			EntityWorld gameWorld = entity.EntityWorld;
			ContainerComponent inventory = new ContainerComponent(10);
			entity.AddComponent(inventory);

			PrototypeCache prototypes = WorldCache.GetCache(gameWorld).Prototypes;

			Entity itemStack = gameWorld.CreateEntity();
			itemStack.AddComponent(new StackComponent(3, 3));
			itemStack.AddComponent(prototypes[EntityType.Consumable]);

			Entity itemSingle = gameWorld.CreateEntity();
			itemSingle.AddComponent(prototypes[EntityType.Consumable]);

			Entity bag = gameWorld.CreateEntity();
			ContainerComponent bagContainerComponent = new ContainerComponent(3);
			PrototypeComponent bagPrototype = new PrototypeComponent(
				prototypes.Count,
				"Bag",
				null,
				EntityType.Container);
			prototypes.Add(bagPrototype);
			bag.AddComponent(bagPrototype);
			bag.AddComponent(bagContainerComponent);
			bagContainerComponent.AddEntity(itemStack);

			Entity backpack = gameWorld.CreateEntity();
			ContainerComponent backpackContainerComponent = new ContainerComponent(3);
			PrototypeComponent backpackPrototype = new PrototypeComponent(
				prototypes.Count,
				"Backpack",
				null,
				EntityType.Container);
			prototypes.Add(backpackPrototype);
			backpack.AddComponent(backpackPrototype);
			backpack.AddComponent(backpackContainerComponent);

			inventory.AddEntity(bag);
			inventory.AddEntity(backpack);
			inventory.AddEntity(itemSingle);
		}
		#endregion
	}
}