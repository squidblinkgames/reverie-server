namespace CommandParser
{
	public class ParameterExpression : Expression
	{
		#region Fields
		private string value;
		#endregion


		#region Constructors
		public ParameterExpression(string value)
		{
			this.value = value;
		}
		#endregion


		#region Properties
		public override ExpressionType ExpressionType
		{
			get { return ExpressionType.Parameter; }
		}


		public override bool Interpretted
		{
			get { return true; }
			protected set { }
		}


		public override object Result
		{
			get { return this.value; }
			protected set { }
		}
		#endregion


		public override void ProcessExpression(ExpressionTokens expressionTokens) {}
	}
}