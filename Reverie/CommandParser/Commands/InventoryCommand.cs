namespace CommandParser.Commands
{
	using System.Collections.Generic;
	using Reverie.Models;
	using Reverie.Utilities;


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
		public override string Result { get; protected set; }
		#endregion


		public override CommandExpression CreateInstance()
		{
			return new InventoryCommand();
		}


		public override void ProcessExpression(ExpressionTokens expressionTokens)
		{
			
			List<EntityModel> inventory;

			Expression nextToken = expressionTokens.GetRightToken();
			if (nextToken != null
				&& nextToken.ExpressionType == ExpressionType.Parameter
				&& nextToken.Result == "all")
			{
				inventory = Inventory.GetContainerContents(
					expressionTokens.InvokingEntity,
					Inventory.LoadOptions.Recursive);
			}
			else
				inventory = Inventory.GetContainerContents(expressionTokens.InvokingEntity);

			this.Interpretted = true;
			this.Result = inventory.ToPrettyJson();
		}
	}
}