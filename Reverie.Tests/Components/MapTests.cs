namespace Reverie.Tests.Components
{
	using System.Collections.Generic;
	using NUnit.Framework;
	using PrimitiveEngine;
	using Reverie.Components;
	using Reverie.Debug;
	using Reverie.Utilities;


	[TestFixture]
	[Category("Components")]
	internal class MapTests
	{
		// TODO: Exits and walls.
		// TODO: Checks to make sure rooms are not shared between maps.

		[TestFixture]
		private class Exits : TestBase
		{
			[Test]
			public void Can_Get_Exits_List()
			{
				ResetEntityPosition();
				IReadOnlyList<MapDirection> exits = this.entity.GetExits();

				Assert.IsNotEmpty(exits);
			}


			[Test]
			public void Cannot_Move_Through_Unavailable_Exit()
			{
				ResetEntityPosition();
				IntegerVector3 expectedPosition = this.entity.GetPosition();
				this.entity.Move(MapDirection.NorthEast);
				IntegerVector3 newPosition = this.entity.GetPosition();

				Assert.AreEqual(expectedPosition, newPosition);
			}
		}


		[TestFixture]
		private class Movement : TestBase
		{
			[Test]
			public void Can_Move_Down()
			{
				ResetEntityPosition();
				IntegerVector3 position = this.entity.GetPosition();
				int expectedY = position.Y - 1;
				this.entity.Move(MapDirection.Down);

				Assert.AreEqual(expectedY, this.entity.GetPosition().Y);
			}


			[Test]
			public void Can_Move_East()
			{
				ResetEntityPosition();
				IntegerVector3 position = this.entity.GetPosition();
				int expectedX = position.X + 1;
				this.entity.Move(MapDirection.East);

				Assert.AreEqual(expectedX, this.entity.GetPosition().X);
			}


			[Test]
			public void Can_Move_North()
			{
				ResetEntityPosition();
				IntegerVector3 position = this.entity.GetPosition();
				int expectedY = position.Y + 1;
				this.entity.Move(MapDirection.North);

				Assert.AreEqual(expectedY, this.entity.GetPosition().Y);
			}


			[Test]
			public void Can_Move_South()
			{
				ResetEntityPosition();
				IntegerVector3 position = this.entity.GetPosition();
				int expectedY = position.Y - 1;
				this.entity.Move(MapDirection.South);

				Assert.AreEqual(expectedY, this.entity.GetPosition().Y);
			}


			[Test]
			public void Can_Move_Up()
			{
				ResetEntityPosition();
				IntegerVector3 position = this.entity.GetPosition();
				int expectedY = position.Y + 1;
				this.entity.Move(MapDirection.Up);

				Assert.AreEqual(expectedY, this.entity.GetPosition().Y);
			}


			[Test]
			public void Can_Move_West()
			{
				ResetEntityPosition();
				IntegerVector3 position = this.entity.GetPosition();
				int expectedX = position.X - 1;
				this.entity.Move(MapDirection.West);

				Assert.AreEqual(expectedX, this.entity.GetPosition().X);
			}
		}


		[TestFixture]
		private class AddingNodes : TestBase
		{
			[Test]
			public void Can_Add_By_Entity()
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
			public void Can_Add_By_Name()
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
		private class NodeRemoval : TestBase
		{
			[Test]
			public void Can_Remove_By_Coordinate()
			{
				int x = -1, y = -1, z = -1;
				Entity temporaryRoom = this.world.CreateEntity(
					name: "Room to Be Removed",
					description: "Remove me!");
				this.map.AddNode(
					x: x,
					y: y,
					z: z,
					roomEntity: temporaryRoom);

				Entity removedRoom = this.map.RemoveNode(x, y, z);
				Assert.IsNotNull(removedRoom);
				Assert.IsNull(this.map[x, y, z]);
			}
		}


		private abstract class TestBase
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

				this.map.AddRoom(
					name: "North Room",
					description: "You are in the northern room.");
				this.map.AddNode(
					x: 1,
					y: 1,
					z: 0,
					roomName: "North Room");

				this.map.AddRoom(
					name: "East Room",
					description: "You are in the eastern room.");
				this.map.AddNode(
					x: 2,
					y: 0,
					z: 0,
					roomName: "East Room");

				this.map.AddRoom(
					name: "South Room",
					description: "You are in the southern room.");
				this.map.AddNode(
					x: 1,
					y: -1,
					z: 0,
					roomName: "South Room");

				this.map.AddRoom(
					name: "West Room",
					description: "You are in the western room.");
				this.map.AddNode(
					x: 0,
					y: 0,
					z: 0,
					roomName: "West Room");

				this.map.AddRoom(
					name: "Center Room",
					description: "You are in the center room.");
				this.map.AddNode(
					x: 1,
					y: 0,
					z: 0,
					roomName: "Center Room");

				Entity mapEntity = this.world.CreateEntity();
				mapEntity.AddComponent(this.map);

				// TODO: Place entity inside the map.
			}


			protected void ResetEntityPosition()
			{
				this.entity.SetPosition(0, 0, 0);
			}
		}
	}