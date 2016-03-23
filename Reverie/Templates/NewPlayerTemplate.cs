namespace Reverie.Templates
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.CompilerServices;
	using PrimitiveEngine.Artemis;
	using Reverie.Entities;
	using Reverie.Items.Components;


	[ArtemisEntityTemplate(Name)]
	public class NewPlayerTemplate : IEntityTemplate
	{
		public const string Name = "NewPlayerTemplate";

		private Dictionary<int, Prototype> prototypes;


		


		public Entity BuildEntity(Entity entity, EntityWorld entityWorld, params object[] args)
		{
			AddNewPlayerInventory(entity);
			return entity;
		}


		#region Helper Methods
		private void AddNewPlayerInventory(Entity entity)
		{
			EntityWorld gameWorld = entity.EntityWorld;
			Container inventory = new Container(10);
			entity.AddComponent(inventory);

			Dictionary<long, Prototype> prototypes =
				gameWorld.BlackBoard.GetEntry<Dictionary<long, Prototype>>(Prototype.Key);

			Entity itemStack = gameWorld.CreateEntity();
			itemStack.AddComponent(new Stackable(3, 3));
			itemStack.AddComponent(prototypes[EntityType.Consumable]);

			Entity itemSingle = gameWorld.CreateEntity();
			itemSingle.AddComponent(prototypes[EntityType.Consumable]);

			Entity bag = gameWorld.CreateEntity();
			Container containerComponent = new Container(3);
			Prototype bagPrototype = new Prototype(
				prototypes.Count,
				"Bag",
				null,
				EntityType.Container);
			bag.AddComponent(bagPrototype);
			bag.AddComponent(containerComponent);
			containerComponent.AddEntity(itemStack);

			inventory.AddEntity(bag);
			inventory.AddEntity(itemSingle);
		}
		#endregion
	}
}