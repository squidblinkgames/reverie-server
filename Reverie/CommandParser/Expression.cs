namespace CommandParser
{
	/// <summary>
	/// Represents an object that may be invoked as a command.
	/// </summary>
	public abstract class Expression
	{
		#region Properties
		public abstract ExpressionType ExpressionType { get; }
		public abstract bool Interpretted { get; protected set; }
		public abstract string Result { get; protected set; }
		#endregion


		/// <summary>
		/// Processes the expression associated with this object.
		/// </summary>
		public virtual void ProcessExpression(ExpressionTokens expressionTokens) {}
	}
}