namespace CommandParser.Commands
{
	using System.Collections.Generic;
	using Reverie.Models;


	public class InventoryCommand : CommandExpression
	{
		#region Properties
		public override string[] Commands
		{
			get { return new[] { "inventory", "items", "storage" }; }
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
			return new InventoryCommand();
		}


		public override void ProcessExpression(ExpressionTokens expressionTokens)
		{
			IList<EntityModel> inventory;

			Expression nextToken = expressionTokens.GetRightToken();
			if (nextToken != null
				&& nextToken.ExpressionType == ExpressionType.Parameter
				&& nextToken.Result.Equals("all"))
			{
				inventory = new EntityModel(expressionTokens.InvokingEntity)
					.SaturateContainerDetails(recurse: true)
					.Entities;
			}
			else
			{
				inventory = new EntityModel(expressionTokens.InvokingEntity)
					.SaturateContainerDetails(recurse: false)
					.Entities;
			}

			this.Interpretted = true;
			this.Result = inventory;
		}
	}
}