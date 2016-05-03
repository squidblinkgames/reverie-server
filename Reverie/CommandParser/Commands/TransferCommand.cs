namespace CommandParser.Commands
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Reverie.Components;
	using Reverie.Extensions;
	using Reverie.Utilities;


	public class TransferCommand : CommandExpression
	{
		#region Fields
		private static string[] commands = { "transfer", "move", "mv" };
		#endregion


		#region Properties
		public override string[] Commands
		{
			get { return commands; }
		}


		public override ExpressionType ExpressionType
		{
			get { return ExpressionType.Command; }
		}


		public override bool Interpretted { get; protected set; }
		public override object Result { get; protected set; }
		#endregion


		public override CommandExpression CreateInstance()
		{
			return new TransferCommand();
		}


		public override void ProcessExpression(CommandTokens commandTokens)
		{
			List<Expression> entityTokens =
				(from token in commandTokens.Tokens
				where token.ExpressionType == ExpressionType.Entity
				select token)
					.ToList();
			if (entityTokens.Count() < 2)
			{
				this.Result = "Must specify at least two items.";
				foreach(Expression token in entityTokens)
					Console.WriteLine("Token: " + token.Result);
				return;
			}
				

			EntityExpression origin = (EntityExpression)entityTokens[0];
			EntityExpression destination = (EntityExpression)entityTokens[1];
			Container originContainer =
				origin.ParentContainer.GetComponent<Container>();
			Container destinationContainer =
				destination.Entity.GetComponent<Container>();

			if (originContainer != null
				&& destinationContainer != null)
			{
				originContainer.RemoveEntity(origin.Entity);
				destinationContainer.AddEntity(origin.Entity);
				this.Result =
					"Moved " + origin.Entity.GetEntityName() +
					" to " + destination.Entity.GetEntityName() + ".";
			}
			else
				this.Result = "Cannot move item to something that isn't a container.";
		}
	}
}