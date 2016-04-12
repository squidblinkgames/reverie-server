namespace CommandParser
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using PrimitiveEngine;
	using PrimitiveEngine.Systems;


	public class Interpreter : EntitySystem
	{
		#region Fields
		private Dictionary<string, Func<CommandExpression>> commands;
		#endregion


		#region Constructors
		public Interpreter(Assembly assembly)
		{
			this.commands = new Dictionary<string, Func<CommandExpression>>();
			SaturateCommands(assembly);
		}
		#endregion


		public bool ContainsCommand(string term)
		{
			return this.commands.ContainsKey(term);
		}


		public CommandExpression GetCommand(string command)
		{
			if (this.commands.ContainsKey(command))
				return this.commands[command]();
			else
				return null;
		}


		public object Interpret(Entity invokingEntity, string command)
		{
			ExpressionTokens expressionTokens = new ExpressionTokens(this, invokingEntity, command);
			return expressionTokens.Interpret();
		}


		#region Helper Methods
		private void SaturateCommands(Assembly assembly)
		{
			IList<Type> commandExpressionTypes =
				CommandExpression.FindAllInAssembly(assembly);
			foreach (Type type in commandExpressionTypes)
			{
				CommandExpression factoryInstance =
					(CommandExpression)Activator.CreateInstance(type);
				Func<CommandExpression> factoryMethod = factoryInstance.CreateInstance;
				foreach (string command in factoryInstance.Commands)
				{
					this.commands.Add(command, factoryMethod);
				}
			}
		}
		#endregion
	}
}