namespace CommandParser
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;
	using PrimitiveEngine;
	using Reverie.Items;
	using Reverie.Items.Components;
	using Reverie.Utilities;


	public class ExpressionTokens
	{
		#region Fields
		private Interpreter interpreter;
		private Entity invokingEntity;
		private List<Expression> tokens;
		private int currentIndex;
		private string result;
		#endregion


		#region Constructors
		public ExpressionTokens(
			Interpreter interpreter,
			Entity invokingEntity,
			string input)
		{
			this.interpreter = interpreter;
			this.invokingEntity = invokingEntity;
			this.tokens = new List<Expression>();

			if (string.IsNullOrEmpty(input))
				return;

			List<string> expressions = Regex.Matches(input, @"[\""].+?[\""]|[^ ]+")
				.Cast<Match>()
				.Select(m => m.Value)
				.ToList();
			string command = expressions[0].ToLower();
			if (interpreter.ContainsCommand(command))
				this.tokens.Add(interpreter.GetCommand(command));
			else
				return;
			ParseTokens(expressions);
		}
		#endregion


		#region Properties
		public int CurrentIndex
		{
			get { return this.currentIndex; }
		}


		public Interpreter Interpreter
		{
			get { return this.interpreter; }
		}


		public Entity InvokingEntity
		{
			get { return this.invokingEntity; }
		}


		public string Result
		{
			get { return this.result; }
			set { this.result = value; }
		}


		public IReadOnlyCollection<Expression> Tokens
		{
			get { return this.tokens.AsReadOnly(); }
		}
		#endregion


		#region Indexers
		public Expression this[int index]
		{
			get { return this.tokens[index]; }
		}
		#endregion


		public Expression GetLeftToken()
		{
			if (this.currentIndex > 0)
				return this.tokens[this.currentIndex--];
			else
				return null;
		}


		public Expression GetRightToken()
		{
			if (this.currentIndex + 1 < this.tokens.Count)
				return this.tokens[this.currentIndex + 1];
			else
				return null;
		}


		public string Interpret()
		{
			this.result = string.Empty;
			this.currentIndex = 0;
			foreach (Expression token in this.tokens)
			{
				if (token.ExpressionType == ExpressionType.Command)
				{
					token.ProcessExpression(this);
					this.result = token.Result;
				}
				this.currentIndex++;
			}

			return this.result;
		}


		#region Helper Methods
		private bool AddInventoryEntityToken(string expression)
		{
			expression = expression.ToLower();
			if (expression.Contains('/'))
				return ScrapeInventoryPath(expression);
			else
				return ScrapeInventory(expression);
		}


		private bool AddRoomEntityToken(string expression)
		{
			IReadOnlyCollection<Entity> roomEntities = this.invokingEntity
				.GetMapNode()
				.GetEntities();
			Entity roomEntity = roomEntities.FindByName(expression);
			if (roomEntity != null)
			{
				this.tokens.Add(new EntityExpression(roomEntity));
				return true;
			}

			return false;
		}


		private void ParseTokens(List<string> expressions)
		{
			if (expressions.Count < 2)
				return;

			for (int i = 1; i < expressions.Count; i++)
			{
				string expression = expressions[i];

				if (AddInventoryEntityToken(expression))
					continue;

				// TODO: Add back
				//if (AddRoomEntityToken(expression))
				//	continue;

				this.tokens.Add(new ParameterExpression(expression));
			}
		}


		private bool ScrapeInventory(string item)
		{
			Entity inventoryEntity;
			Entity container = this.invokingEntity;
			List<Entity> entityInventory = this.invokingEntity
				.GetContainerEntities();

			inventoryEntity = entityInventory.FindByName(item);
			if (inventoryEntity != null)
			{
				this.tokens.Add(new EntityExpression(inventoryEntity, container));
				return true;
			}

			return false;
		}


		private bool ScrapeInventoryPath(string itemPath)
		{
			Entity inventoryEntity;
			Entity container = this.invokingEntity;

			string[] containerPath = itemPath.Split('/');
			for (int i = 0; i < containerPath.Length; i++)
			{
				string part = containerPath[i];
				inventoryEntity = container
					.GetContainerEntities()
					.FindByName(part);
				if (inventoryEntity == null)
					return false;

				ContainerComponent containerComponent =
					inventoryEntity.GetComponent<ContainerComponent>();
				if (containerComponent != null
					&& containerComponent.ChildEntityIds != null)
				{
					if (i == containerPath.Length - 1)
					{
						this.tokens.Add(new EntityExpression(inventoryEntity, container));
						return true;
					}
					container = inventoryEntity;
				}

				else
				{
					this.tokens.Add(new EntityExpression(inventoryEntity, container));
					return true;
				}
			}

			return false;
		}
		#endregion
	}
}