namespace Reverie
{
	using System;
	using System.Reflection;
	using PrimitiveEngine.Artemis;
	using PrimitiveEngine;
	using PrimitiveEngine.Interpreter;
	using Reverie.Debug;
	using Reverie.Templates;


	public class ReverieGame : GameLoop
	{
		#region Fields
		private EntityWorld gameWorld;
		private CommandInterpreter commandInterpreter;
		#endregion


		#region Constructors
		public ReverieGame()
		{
			this.gameWorld = MockWorld.Generate();
			this.commandInterpreter = new CommandInterpreter(Assembly.GetAssembly(typeof(ReverieGame)));
		}
		#endregion


		#region Properties
		public CommandInterpreter CommandInterpreter
		{
			get { return this.commandInterpreter; }
		}


		public EntityWorld GameWorld
		{
			get { return this.gameWorld; }
		}
		#endregion


		public Entity InsertPlayer()
		{
			Console.WriteLine("New player");
			Entity player = this.GameWorld.CreateEntityFromTemplate(NewPlayerTemplate.Name);
			return player;
		}


		public override void Update(long deltaTime)
		{
			// TODO
		}
	}
}