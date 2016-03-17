namespace Reverie
{
	using System;
	using System.Collections.Generic;
	using Artemis;
	using PrimitiveEngine;
	using PrimitiveEngine.Interpreter;
	using Reverie.Debug;
	using Reverie.Entities;
	using Reverie.Entities.Components;
	using Reverie.Items.Components;


	public class ReverieGame : GameLoop
	{
		#region Fields
		private EntityWorld gameWorld;
		private CommandInterpreter commandInterpreter;
		#endregion


		#region Constructors
		public ReverieGame()
		{
			this.gameWorld = MockWorld.Generate();
			this.commandInterpreter = new CommandInterpreter();
		}
		#endregion


		#region Properties
		public CommandInterpreter CommandInterpreter
		{
			get { return this.commandInterpreter; }
		}


		public EntityWorld GameWorld
		{
			get { return this.gameWorld; }
		}
		#endregion


		public void AddTestInventory(Entity entity)
		{
			Container inventory = new Container(10);
			entity.AddComponent(inventory);

			Dictionary<int, Prototype> prototypes =
				this.gameWorld.BlackBoard.GetEntry<Dictionary<int, Prototype>>(Prototype.Key);

			Entity itemStack = this.gameWorld.CreateEntity();
			itemStack.AddComponent(new Stackable(3, 3));
			itemStack.AddComponent(prototypes[EntityType.Junk]);

			Entity itemSingle = this.gameWorld.CreateEntity();
			itemSingle.AddComponent(prototypes[EntityType.Junk]);

			Entity bag = this.gameWorld.CreateEntity();
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


		public Entity InsertPlayer()
		{
			Console.WriteLine("New player");
			Entity player = this.GameWorld.CreateEntity();
			AddTestInventory(player);
			return player;
		}


		public override void Update(long deltaTime)
		{
			// TODO
		}
	}
}