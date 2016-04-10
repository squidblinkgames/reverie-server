namespace CommandParser
{
	using PrimitiveEngine;
	using Reverie.Utilities;


	public class EntityExpression : Expression
	{
		#region Fields
		private Entity entity;
		private Entity parentContainer;
		#endregion


		#region Constructors
		public EntityExpression(Entity entity, Entity parentContainer = null)
		{
			this.entity = entity;
			this.parentContainer = parentContainer;
			this.Interpretted = true;
			this.Result = entity.GetName();
		}
		#endregion


		#region Properties
		public Entity Entity
		{
			get { return this.entity; }
		}


		public override ExpressionType ExpressionType
		{
			get { return ExpressionType.Entity; }
		}


		public override bool Interpretted { get; protected set; }


		public Entity ParentContainer
		{
			get { return this.parentContainer; }
		}


		public override string Result { get; protected set; }
		#endregion
	}
}