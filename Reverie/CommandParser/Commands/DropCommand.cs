namespace CommandParser.Commands
{
	public class DropCommand : CommandExpression
	{
		#region Fields
		private static string[] commands = { "drop" };
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
			return new DropCommand();
		}


		public override void ProcessExpression(CommandTokens commandTokens)
		{
			throw new System.NotImplementedException();
		}
	}
}