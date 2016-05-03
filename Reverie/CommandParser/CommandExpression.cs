namespace CommandParser
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;


	public abstract class CommandExpression : Expression
	{
		#region Properties
		public abstract string[] Commands { get; }
		#endregion


		public abstract CommandExpression CreateInstance();

		public abstract override void ProcessExpression(CommandTokens commandTokens);


		public static IList<Type> FindAllInAssembly(Assembly assembly)
		{
			List<Type> types = new List<Type>();
			Type commandExpressionType = typeof(CommandExpression);
			foreach (Type type in assembly.GetTypes())
			{
				if (type.IsSubclassOf(commandExpressionType))
					types.Add(type);
			}

			return types;
		}
	}
}