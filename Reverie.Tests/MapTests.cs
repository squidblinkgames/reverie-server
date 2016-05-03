namespace Reverie.Tests
{
	using NUnit.Framework;
	using PrimitiveEngine;
	using Reverie.Components;
	using Reverie.Debug;
	using Reverie.Maps;
	using Reverie.Utilities;


	[TestFixture]
	[Category("Components")]
	class MapTests
	{
		// TODO: Exits and walls.
		// TODO: Checks to make sure rooms are not shared between maps.

		[TestFixture]
		class Move : TestBase
		{
			[Test]
			public void Down()
			{
				IntegerVector3 position = this.entity.GetPosition();
				int expectedY = position.Y - 1;
				this.entity.Move(MapDirection.Down);
				Assert.AreEqual(expectedY, this.entity.GetPosition().Y);
			}


			[Test]
			public void East()
			{
				IntegerVector3 position = this.entity.GetPosition();
				int expectedX = position.X + 1;
				this.entity.Move(MapDirection.East);
				Assert.AreEqual(expectedX, this.entity.GetPosition().X);
			}


			[Test]
			public void North()
			{
				IntegerVector3 position = this.entity.GetPosition();
				int expectedY = position.Y + 1;
				this.entity.Move(MapDirection.North);
				Assert.AreEqual(expectedY, this.entity.GetPosition().Y);
			}


			[Test]
			public void South()
			{
				IntegerVector3 position = this.entity.GetPosition();
				int expectedY = position.Y - 1;
				this.entity.Move(MapDirection.South);
				Assert.AreEqual(expectedY, this.entity.GetPosition().Y);
			}


			[Test]
			public void Up()
			{
				IntegerVector3 position = this.entity.GetPosition();
				int expectedY = position.Y + 1;
				this.entity.Move(MapDirection.Up);
				Assert.AreEqual(expectedY, this.entity.GetPosition().Y);
			}


			[Test]
			public void West()
			{
				IntegerVector3 position = this.entity.GetPosition();
				int expectedX = position.X - 1;
				this.entity.Move(MapDirection.West);
				Assert.AreEqual(expectedX, this.entity.GetPosition().X);
			}
		}


		[TestFixture]
		class AddNode : TestBase
		{
			[Test]
			public void By_Entity()
			{
				int x = 2, y = 0, z = 0;

				Entity moreEastRoom = this.world.CreateEntity(
					name: "Most East Room",
					description: "A room even further east.");

				this.map.AddNode(
					x: x,
					y: y,
					z: z,
					roomEntity: moreEastRoom);

				Assert.IsNotNull(this.map[x, y, z]);
				Assert.IsNotNull(moreEastRoom.GetComponent<Container>());
			}


			[Test]
			public void By_Name()
			{
				int x = 1, y = 2, z = 0;

				this.map.AddNode(
					x: x,
					y: y,
					z: z,
					roomName: "North Room");

				Entity roomEntity = this.map[x, y, z];

				Assert.IsNotNull(roomEntity);
				Assert.IsNotNull(roomEntity.GetComponent<Container>());
				Assert.IsNotNull(roomEntity.GetComponent<EntityDetails>());
			}
		}


		[TestFixture]
		class RemoveNode : TestBase
		{
			[Test]
			public void By_Coordinate()
			{
				Entity temporaryRoom = this.world.CreateEntity(
					name: "Room to Be Removed",
					description: "Remove me!");

				this.map.AddNode(
					x: -1,
					y: -1,
					z: -1,
					roomEntity: temporaryRoom);

				Entity removedRoom = this.map.RemoveNode(x: -1, y: -1, z: -1);
				Assert.IsNotNull(removedRoom);
				Assert.IsNull(this.map[x: -1, y: -1, z: -1]);
			}
		}


		abstract class TestBase
		{
			#region Fields
			protected EntityWorld world;
			protected Entity entity;
			protected Map map;
			#endregion


			[TestFixtureSetUp]
			public void SetUp()
			{
				this.world = MockWorld.Generate();
				this.entity = this.world.CreateEntity();

				this.map = new Map();
				map.AddRoom(
					name: "North Room",
					description: "You are in the northern room.");
				map.AddNode(
					x: 1,
					y: 1,
					z: 0,
					roomName: "North Room");

				map.AddRoom(
					name: "East Room",
					description: "You are in the eastern room.");
				map.AddNode(
					x: 2,
					y: 0,
					z: 0,
					roomName: "East Room");

				map.AddRoom(
					name: "South Room",
					description: "You are in the southern room.");
				map.AddNode(
					x: 1,
					y: -1,
					z: 0,
					roomName: "South Room");

				map.AddRoom(
					name: "West Room",
					description: "You are in the western room.");
				map.AddNode(
					x: 0,
					y: 0,
					z: 0,
					roomName: "West Room");

				map.AddRoom(
					name: "Center Room",
					description: "You are in the center room.");
				map.AddNode(
					x: 1,
					y: 0,
					z: 0,
					roomName: "Center Room");

				Entity mapEntity = this.world.CreateEntity();
				mapEntity.AddComponent(this.map);

				// TODO: Place entity inside the map.
			}
		}
	}