namespace CommandParser.Commands
{
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
		public override string Result { get; protected set; }
		#endregion


		public override CommandExpression CreateInstance()
		{
			return new TakeCommand();
		}


		public override void ProcessExpression(ExpressionTokens expressionTokens)
		{
			throw new System.NotImplementedException();
		}
	}
}