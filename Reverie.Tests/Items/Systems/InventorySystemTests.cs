namespace Reverie.Items.Systems.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using Artemis;
	using Newtonsoft.Json;
	using NUnit.Framework;
	using PrimitiveEngine.Interpreter;
	using Reverie.Entities;
	using Reverie.Entities.Components;
	using Reverie.Items.Components;
	using Reverie.Items.Models;
	using Reverie.Tests;


	[TestFixture]
	public class InventorySystemTests
	{
		#region Fields
		private EntityWorld world;
		private Entity entity;
		private ContainerSystem containerSystem;
		private CommandInterpreter interpreter;
		#endregion


		[Test]
		public void Get_Container_Contents_Recursively_Test()
		{
			List<ItemModel> inventory = this.containerSystem.GetContainerContents(
				this.entity,
				ContainerSystem.LoadOptions.Recursive);
			Console.WriteLine(inventory.ToPrettyJson());
			Assert.AreEqual(inventory.Count, 2);
			Assert.IsNotNull(inventory[0].ItemIds);
			Assert.IsNotNull(inventory[0].Items);
		}


		[Test]
		public void Get_Container_Contents_Test()
		{
			List<ItemModel> inventory =
				this.containerSystem.GetContainerContents(this.entity);
			Console.WriteLine(inventory.ToPrettyJson());
			Assert.AreEqual(inventory.Count, 2);
			Assert.IsNull(inventory[0].ItemIds);
			Assert.IsNull(inventory[0].Items);
		}


		[Test]
		public void Process_Inventory_All_Command_Test()
		{
			string result = this.interpreter.Interpret(
				this.entity,
				"inventory all");
			Console.WriteLine(result);
			ItemModel[] inventory = JsonConvert.DeserializeObject<ItemModel[]>(result);
			Assert.AreEqual(inventory.Length, 2);
			Assert.IsNotNull(inventory[0].ItemIds);
			Assert.IsNotNull(inventory[0].Items);
		}


		[Test]
		public void Process_Inventory_Command_Test()
		{
			string result = this.interpreter.Interpret(
				this.entity,
				"inventory");
			Console.WriteLine(result);
			ItemModel[] inventory = JsonConvert.DeserializeObject<ItemModel[]>(result);
			Assert.AreEqual(inventory.Length, 2);
			Assert.IsNull(inventory[0].ItemIds);
			Assert.IsNull(inventory[0].Items);
		}


		[Test]
		public void Process_Items_Command_Test()
		{
			string result = this.interpreter.Interpret(
				this.entity,
				"items");
			ItemModel[] inventory = JsonConvert.DeserializeObject<ItemModel[]>(result);
			Assert.AreEqual(inventory.Length, 2);
			Assert.IsNull(inventory[0].ItemIds);
			Assert.IsNull(inventory[0].Items);
		}


		[Test]
		public void Process_Storage_Command_Test()
		{
			string result = this.interpreter.Interpret(
				this.entity,
				"storage");
			Console.WriteLine(result);
			ItemModel[] inventory = JsonConvert.DeserializeObject<ItemModel[]>(result);
			Assert.AreEqual(inventory.Length, 2);
			Assert.IsNull(inventory[0].ItemIds);
			Assert.IsNull(inventory[0].Items);
		}


		[TestFixtureSetUp]
		public void SetUp()
		{
			this.world = MockWorld.Generate();
			this.entity = this.world.CreateEntity();

			this.containerSystem = this.world.GetSystem<ContainerSystem>();
			this.interpreter = new CommandInterpreter();

			Container inventory = new Container(10);
			this.entity.AddComponent(inventory);

			Dictionary<int, Prototype> prototypes =
				world.BlackBoard.GetEntry<Dictionary<int, Prototype>>(Prototype.Key);

			Entity itemStack = world.CreateEntity();
			itemStack.AddComponent(new Stackable(3, 3));
			itemStack.AddComponent(prototypes[EntityType.Junk]);

			Entity itemSingle = world.CreateEntity();
			itemSingle.AddComponent(prototypes[EntityType.Junk]);

			Entity bag = world.CreateEntity();
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
	}
}