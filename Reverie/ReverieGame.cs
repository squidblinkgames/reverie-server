namespace Reverie
{
	using System;
	using System.Reflection;
	using PrimitiveEngine;
	using Reverie.Debug;
	using Reverie.Templates;


	public class ReverieGame : GameLoop
	{
		#region Fields
		private EntityWorld gameWorld;
		private CommandParser.Interpreter interpreter;
		#endregion


		#region Constructors
		public ReverieGame()
		{
			this.gameWorld = MockWorld.Generate();
			this.interpreter = new CommandParser.Interpreter(Assembly.GetAssembly(typeof(ReverieGame)));
		}
		#endregion


		#region Properties
		public CommandParser.Interpreter Interpreter
		{
			get { return this.interpreter; }
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