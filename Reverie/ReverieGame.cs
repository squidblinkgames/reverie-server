namespace Reverie
{
    using System;
    using System.Reflection;
    using PrimitiveEngine;
    using Reverie.State;
    using Reverie.Templates;


    public class ReverieGame : GameLoop
    {
        #region Fields
        private ReverieWorld reverieWorld;
        private CommandParser.Interpreter interpreter;
        #endregion


        #region Constructors
        public ReverieGame()
        {
            // TODO: Create game world here.
            this.interpreter = new CommandParser.Interpreter(Assembly.GetAssembly(typeof(ReverieGame)));
        }
        #endregion


        #region Properties
        public ReverieWorld ReverieWorld
        {
            get { return this.reverieWorld; }
        }


        public CommandParser.Interpreter Interpreter
        {
            get { return this.interpreter; }
        }
        #endregion


        public Entity InsertPlayer()
        {
            Console.WriteLine("New player");
            Entity player = this.ReverieWorld.CreateEntityFromTemplate(NewPlayerTemplate.Name);
            return player;
        }


        public override void Update(long deltaTime)
        {
            // TODO
        }
    }
}