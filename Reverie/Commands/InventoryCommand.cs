namespace Reverie.Commands
{
	using System.Collections.Generic;
	using PrimitiveEngine.Interpreter;
	using Reverie.Items;
	using Reverie.Items.Models;


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


		public override void ProcessExpression(GameCommand command)
		{
			
			List<ContainerModel> inventory;

			Expression nextToken = command.GetRightToken();
			if (nextToken != null
				&& nextToken.ExpressionType == ExpressionType.Parameter
				&& nextToken.Result == "all")
			{
				inventory = Inventory.GetContainerContents(
					command.InvokingEntity,
					Inventory.LoadOptions.Recursive);
			}
			else
				inventory = Inventory.GetContainerContents(command.InvokingEntity);

			this.Interpretted = true;
			this.Result = inventory.ToPrettyJson();
		}
	}
}