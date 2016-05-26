namespace Reverie.Tests.Components
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using PrimitiveEngine;
    using Reverie.Cache;
    using Reverie.Components;
    using Reverie.Models.Client;


    [TestFixture]
    [Category("Components")]
    class ContainerTests
    {
        class Get_Nested_Contents : TestBase
        {
            [Test]
            public void NonRecursively()
            {
                IList<EntityClientModel> inventory = new EntityClientModel(this.entity)
                    .SaturateContainerDetails(recurse: false)
                    .Entities;
                Console.WriteLine(inventory.ToPrettyJson());
                Assert.AreEqual(inventory.Count, 2);
                Assert.IsNull(inventory[0].Entities);
            }


            [Test]
            public void Recursively()
            {
                IList<EntityClientModel> inventory = new EntityClientModel(this.entity)
                    .SaturateContainerDetails(recurse: true)
                    .Entities;
                Console.WriteLine(inventory.ToPrettyJson());
                Assert.AreEqual(inventory.Count, 2);
                Assert.IsNotNull(inventory[0].Entities);
            }
        }


        abstract class TestBase
        {
            #region Fields
            protected EntityWorld world;
            protected Entity entity;
            #endregion


            [TestFixtureSetUp]
            public void SetUp()
            {
                this.world = MockWorld.Generate();
                this.entity = this.world.CreateEntity();

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