namespace Reverie.Items.Systems.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using PrimitiveEngine.Artemis;
	using Newtonsoft.Json;
	using NUnit.Framework;
	using PrimitiveEngine.Interpreter;
	using Reverie.State;
	using Reverie.Debug;
	using Reverie.Entities;
	using Reverie.Items.Components;
	using Reverie.Items.Models;


	[TestFixture]
	public class InventorySystemTests
	{
		#region Fields
		private EntityWorld world;
		private Entity entity;
		private Inventory inventory;
		private CommandInterpreter interpreter;
		#endregion


		[Test]
		public void Get_Container_Contents_Recursively_Test()
		{
			List<ContainerModel> inventory = this.inventory.GetContainerContents(
				this.entity,
				Inventory.LoadOptions.Recursive);
			Console.WriteLine(inventory.ToPrettyJson());
			Assert.AreEqual(inventory.Count, 2);
			Assert.IsNotNull(inventory[0].ItemIds);
			Assert.IsNotNull(inventory[0].Entities);
		}


		[Test]
		public void Get_Container_Contents_Test()
		{
			List<ContainerModel> inventory =
				this.inventory.GetContainerContents(this.entity);
			Console.WriteLine(inventory.ToPrettyJson());
			Assert.AreEqual(inventory.Count, 2);
			Assert.IsNull(inventory[0].ItemIds);
			Assert.IsNull(inventory[0].Entities);
		}


		[Test]
		public void Process_Inventory_All_Command_Test()
		{
			string result = this.interpreter.Interpret(
				this.entity,
				"inventory all");
			Console.WriteLine(result);
			ContainerModel[] inventory = JsonConvert.DeserializeObject<ContainerModel[]>(result);
			Assert.AreEqual(inventory.Length, 2);
			Assert.IsNotNull(inventory[0].ItemIds);
			Assert.IsNotNull(inventory[0].Entities);
		}


		[Test]
		public void Process_Inventory_Command_Test()
		{
			string result = this.interpreter.Interpret(
				this.entity,
				"inventory");
			Console.WriteLine(result);
			ContainerModel[] inventory = JsonConvert.DeserializeObject<ContainerModel[]>(result);
			Assert.AreEqual(inventory.Length, 2);
			Assert.IsNull(inventory[0].ItemIds);
			Assert.IsNull(inventory[0].Entities);
		}


		[Test]
		public void Process_Items_Command_Test()
		{
			string result = this.interpreter.Interpret(
				this.entity,
				"items");
			ContainerModel[] inventory = JsonConvert.DeserializeObject<ContainerModel[]>(result);
			Assert.AreEqual(inventory.Length, 2);
			Assert.IsNull(inventory[0].ItemIds);
			Assert.IsNull(inventory[0].Entities);
		}


		[Test]
		public void Process_Storage_Command_Test()
		{
			string result = this.interpreter.Interpret(
				this.entity,
				"storage");
			Console.WriteLine(result);
			ContainerModel[] inventory = JsonConvert.DeserializeObject<ContainerModel[]>(result);
			Assert.AreEqual(inventory.Length, 2);
			Assert.IsNull(inventory[0].ItemIds);
			Assert.IsNull(inventory[0].Entities);
		}


		[TestFixtureSetUp]
		public void SetUp()
		{
			this.world = MockWorld.Generate();
			this.entity = this.world.CreateEntity();

			this.inventory = this.world.GetSystem<Inventory>();
			this.interpreter = new CommandInterpreter(Assembly.GetAssembly(typeof(ReverieGame)));

			Container inventory = new Container(10);
			this.entity.AddComponent(inventory);

			PrototypeCache prototypes =
				world.BlackBoard.GetEntry<PrototypeCache>();

			Entity itemStack = world.CreateEntity();
			itemStack.AddComponent(new Stackable(3, 3));
			itemStack.AddComponent(prototypes[EntityType.Consumable]);

			Entity itemSingle = world.CreateEntity();
			itemSingle.AddComponent(prototypes[EntityType.Consumable]);

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