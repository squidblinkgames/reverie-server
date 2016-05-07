namespace Reverie.Tests.Components
{
	using NUnit.Framework;
	using PrimitiveEngine;
	using Reverie.Components;
	using Reverie.Debug;
	using Reverie.Utilities;


	[TestFixture]
	[Category("Components")]
	internal class LocationTests
	{
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

				// TODO: Place entity inside the map.
			}


			protected void ResetEntityPosition()
			{
				this.entity.SetPosition(0, 0, 0);
			}
		}
	}
}