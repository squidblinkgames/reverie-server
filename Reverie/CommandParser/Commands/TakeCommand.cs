namespace CommandParser.Commands
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using PrimitiveEngine;
	using Reverie.Components;
	using Reverie.Extensions;
	using Reverie.Utilities;


	public class TakeCommand : CommandExpression
	{
		#region Fields
		private static string[] commands = { "take" };
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
			return new TakeCommand();
		}


		public override void ProcessExpression(CommandTokens commandTokens)
		{
			Entity player = commandTokens.InvokingEntity;
			List<Expression> entityTokens =
				(from token in commandTokens.Tokens
				 where token.ExpressionType == ExpressionType.Entity
				 select token)
					.ToList();
			if (entityTokens.Count() < 1)
			{
				this.Result = "Must specify an item to take.";
				foreach (Expression token in entityTokens)
					Console.WriteLine("Token: " + token.Result);
				return;
			}

			EntityExpression item = (EntityExpression)entityTokens[0];
			
			Container groundItemContainer = item.ParentContainer.GetComponent<Container>();
			Container playerInventory = commandTokens.InvokingEntity.GetComponent<Container>();
			
			if (groundItemContainer != null
				&& playerInventory != null)
			{
				groundItemContainer.RemoveEntity(item.Entity);
				playerInventory.AddEntity(item.Entity);
				this.Result =
					"Got " + item.Entity.GetEntityName();
			}
			else
				this.Result = "Could not retrieve the item.";
			
		}
	}
}