namespace Reverie.Tests.Items
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using Newtonsoft.Json;
	using NUnit.Framework;
	using PrimitiveEngine;
	using CommandParser;
	using Reverie.Debug;
	using Reverie.Entities;
	using Reverie.Items;
	using Reverie.Items.Components;
	using Reverie.Items.Models;
	using Reverie.State;


	[TestFixture]
	public class InventoryTests
	{
		#region Fields
		private EntityWorld world;
		private Entity entity;
		private Interpreter interpreter;
		#endregion


		[Test]
		public void Get_Container_Contents_Recursively_Test()
		{
			List<ContainerModel> inventory = Inventory.GetContainerContents(
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
				Inventory.GetContainerContents(this.entity);
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
			Console.WriteLine(result);
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

			this.interpreter = new Interpreter(Assembly.GetAssembly(typeof(ReverieGame)));

			ContainerComponent inventory = new ContainerComponent(10);
			this.entity.AddComponent(inventory);

			PrototypeCache prototypes = WorldCache.GetCache(this.world).Prototypes;

			Entity itemStack = this.world.CreateEntity();
			itemStack.AddComponent(new StackComponent(3, 3));
			itemStack.AddComponent(prototypes[EntityType.Consumable]);

			Entity itemSingle = this.world.CreateEntity();
			itemSingle.AddComponent(prototypes[EntityType.Consumable]);

			Entity bag = this.world.CreateEntity();
			ContainerComponent containerComponentComponent = new ContainerComponent(3);
			PrototypeComponent bagPrototype = new PrototypeComponent(
				prototypes.Count,
				"Bag",
				null,
				EntityType.Container);
			bag.AddComponent(bagPrototype);
			bag.AddComponent(containerComponentComponent);
			containerComponentComponent.AddEntity(itemStack);

			inventory.AddEntity(bag);
			inventory.AddEntity(itemSingle);
		}
	}
}