namespace Reverie.Tests.Items
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using Newtonsoft.Json;
	using NUnit.Framework;
	using PrimitiveEngine;
	using CommandParser;
	using Reverie.Components;
	using Reverie.Debug;
	using Reverie.Models;
	using Reverie.Cache;
	using Reverie.Utilities;


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
			IList<EntityModel> inventory = new EntityModel(this.entity)
				.SaturateContainerDetails(recurse: true)
				.Entities;
			Console.WriteLine(inventory.ToPrettyJson());
			Assert.AreEqual(inventory.Count, 2);
			Assert.IsNotNull(inventory[0].Entities);
		}


		[Test]
		public void Get_Container_Contents_Test()
		{
			IList<EntityModel> inventory = new EntityModel(this.entity)
				.SaturateContainerDetails(recurse: false)
				.Entities;
			Console.WriteLine(inventory.ToPrettyJson());
			Assert.AreEqual(inventory.Count, 2);
			Assert.IsNull(inventory[0].Entities);
		}


		[Test]
		public void Process_Inventory_All_Command_Test()
		{
			string result = this.interpreter.Interpret(
				this.entity,
				"inventory all").ToPrettyJson();
			Console.WriteLine(result);
			EntityModel[] inventory = JsonConvert.DeserializeObject<EntityModel[]>(result);
			Assert.AreEqual(inventory.Length, 2);
			Assert.IsNotNull(inventory[0].Entities);
		}


		[Test]
		public void Process_Inventory_Command_Test()
		{
			string result = this.interpreter.Interpret(
				this.entity,
				"inventory").ToPrettyJson();
			Console.WriteLine(result);
			IList<EntityModel> inventory = JsonConvert.DeserializeObject<IList<EntityModel>>(result);
			Assert.AreEqual(inventory.Count, 2);
			Assert.IsNull(inventory[0].Entities);
		}


		[Test]
		public void Process_Items_Command_Test()
		{
			string result = this.interpreter.Interpret(
				this.entity,
				"items").ToPrettyJson();
			IList<EntityModel> inventory = JsonConvert.DeserializeObject<IList<EntityModel>>(result);
			Console.WriteLine(result);
			Assert.AreEqual(inventory.Count, 2);
			Assert.IsNull(inventory[0].Entities);
		}


		[Test]
		public void Process_Storage_Command_Test()
		{
			string result = this.interpreter.Interpret(
				this.entity,
				"storage").ToPrettyJson();
			Console.WriteLine(result);
			EntityModel[] inventory = JsonConvert.DeserializeObject<EntityModel[]>(result);
			Assert.AreEqual(inventory.Length, 2);
			Assert.IsNull(inventory[0].Entities);
		}


		[TestFixtureSetUp]
		public void SetUp()
		{
			this.world = MockWorld.Generate();
			this.entity = this.world.CreateEntity();

			this.interpreter = new Interpreter(Assembly.GetAssembly(typeof(ReverieGame)));

			Container inventory = new Container(10);
			this.entity.AddComponent(inventory);

			EntityDataCache entityDatas = WorldCache.GetCache(this.world).EntityDatas;

			Entity itemStack = this.world.CreateEntity();
			itemStack.AddComponent(new EntityStack(3, 3));
			itemStack.AddComponent(entityDatas[EntityType.Consumable]);

			Entity itemSingle = this.world.CreateEntity();
			itemSingle.AddComponent(entityDatas[EntityType.Consumable]);

			Entity bag = this.world.CreateEntity();
			Container container = new Container(3);
			EntityData bagEntityData = new EntityData(
				Guid.NewGuid(),
				"Bag",
				"Just a bag.",
				null,
				EntityType.Container);
			bag.AddComponent(bagEntityData);
			bag.AddComponent(container);
			container.AddEntity(itemStack);

			inventory.AddEntity(bag);
			inventory.AddEntity(itemSingle);
		}
	}
}