namespace Reverie.Tests
{
	using System.Collections.Generic;
	using System.Reflection;
	using Artemis;
	using PrimitiveEngine.Interpreter;
	using Reverie.Entities;
	using Reverie.Entities.Components;


	public static class MockWorld
	{
		public static Commands CreateVerbs(EntityWorld world)
		{
			Commands commands = new Commands(world);
			return commands;
		}


		public static Dictionary<int, Prototype> CreatePrototypes()
		{
			Dictionary<int, Prototype> prototypes = new Dictionary<int, Prototype>();
			foreach (EntityType type in EntityType.AllTypes)
			{
				Prototype prototype = new Prototype(type.Value, type.Name, null);
				prototypes.Add(prototype.Id, prototype);
			}

			return prototypes;
		}


		public static EntityWorld Generate()
		{
			EntityWorld testWorld = new EntityWorld();
			testWorld.InitializeAll(Assembly.GetAssembly(typeof(ReverieGame)));
			testWorld.BlackBoard.SetEntry(Prototype.Key, CreatePrototypes());
			SeedItems(testWorld);

			return testWorld;
		}


		public static IReadOnlyCollection<Entity> SeedItems(EntityWorld world)
		{
			// TODO
			return null;
		}
	}
}