using System;
using System.Collections.Generic;


namespace Reverie.Tests.CommandParser.Commands
{
	using System.Reflection;
	using global::CommandParser;
	using Newtonsoft.Json;
	using NUnit.Framework;
	using PrimitiveEngine;
	using Reverie.Cache;
	using Reverie.Components;
	using Reverie.Debug;
	using Reverie.Models.Client;


    [TestFixture]
	[Category("MUD Commands")]
	class InventoryCommandTests
	{
		class Process : TestBase
		{
			[Test]
			public void Inventory()
			{
				string result = this.interpreter.Interpret(
					this.entity,
					"inventory").ToPrettyJson();
				Console.WriteLine(result);
				IList<EntityClientModel> inventory = JsonConvert.DeserializeObject<IList<EntityClientModel>>(result);
				Assert.AreEqual(inventory.Count, 2);
				Assert.IsNull(inventory[0].Entities);
			}


			[Test]
			public void Inventory_All()
			{
				string result = this.interpreter.Interpret(
					this.entity,
					"inventory all").ToPrettyJson();
				Console.WriteLine(result);
				EntityClientModel[] inventory = JsonConvert.DeserializeObject<EntityClientModel[]>(result);
				Assert.AreEqual(inventory.Length, 2);
				Assert.IsNotNull(inventory[0].Entities);
			}


			[Test]
			public void Items()
			{
				string result = this.interpreter.Interpret(
					this.entity,
					"items").ToPrettyJson();
				IList<EntityClientModel> inventory = JsonConvert.DeserializeObject<IList<EntityClientModel>>(result);
				Console.WriteLine(result);
				Assert.AreEqual(inventory.Count, 2);
				Assert.IsNull(inventory[0].Entities);
			}


			[Test]
			public void Storage()
			{
				string result = this.interpreter.Interpret(
					this.entity,
					"storage").ToPrettyJson();
				Console.WriteLine(result);
				EntityClientModel[] inventory = JsonConvert.DeserializeObject<EntityClientModel[]>(result);
				Assert.AreEqual(inventory.Length, 2);
				Assert.IsNull(inventory[0].Entities);
			}
		}


		abstract class TestBase
		{
			#region Fields
			protected EntityWorld world;
			protected Entity entity;
			protected Interpreter interpreter;
			#endregion


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
				EntityDetails bagEntityDetails = new EntityDetails(
					Guid.NewGuid(),
					"Bag",
					"Just a bag.",
					null,
					EntityType.Container);
				bag.AddComponent(bagEntityDetails);
				bag.AddComponent(container);
				container.AddEntity(itemStack);

				inventory.AddEntity(bag);
				inventory.AddEntity(itemSingle);
			}
		}
	}
}