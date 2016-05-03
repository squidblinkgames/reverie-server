namespace CommandParser
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;
	using PrimitiveEngine;
	using Reverie.Components;
	using Reverie.Extensions;
	using Reverie.Utilities;


	public class CommandTokens
	{
		private static readonly string[] SelfExpressions = new[] { "self", "me" };


		#region Fields
		private Interpreter interpreter;
		private Entity invokingEntity;
		private List<Expression> tokens;
		private int currentIndex;
		private object result;
		#endregion


		#region Constructors
		public CommandTokens(
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


		public object Result
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


		public object Interpret()
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
			if (!this.invokingEntity.HasComponent<Location>())
				return false;

			IReadOnlyCollection<Entity> roomEntities = this.invokingEntity
				.GetMapNode()
				.GetEntities();

			Entity roomEntity = roomEntities.GetEntityByName(expression);
			if (roomEntity != null)
			{
				this.tokens.Add(new EntityExpression(roomEntity));
				return true;
			}

			return false;
		}


		private bool AddSelfEntityToken(string expression)
		{
			if (SelfExpressions.Contains(expression.ToLower()))
			{
				this.tokens.Add(new EntityExpression(this.invokingEntity));
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

				if (AddSelfEntityToken(expression))
					continue;

				if (AddInventoryEntityToken(expression))
					continue;

				if (AddRoomEntityToken(expression))
					continue;

				this.tokens.Add(new ParameterExpression(expression));
			}
		}


		private bool ScrapeInventory(string item)
		{
			Entity inventoryEntity;
			Entity container = this.invokingEntity;
			IReadOnlyCollection<Entity> entityInventory =
				this.invokingEntity.GetChildEntities();

			inventoryEntity = entityInventory.GetEntityByName(item);
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
			Entity containerEntity = this.invokingEntity;

			string[] containerPath = itemPath.Split('/');
			for (int i = 0; i < containerPath.Length; i++)
			{
				string part = containerPath[i];
				inventoryEntity = containerEntity
					.GetChildEntities()
					.GetEntityByName(part);
				if (inventoryEntity == null)
					return false;

				Container container =
					inventoryEntity.GetComponent<Container>();
				if (container != null
					&& container.ChildEntityIds != null)
				{
					if (i == containerPath.Length - 1)
					{
						this.tokens.Add(new EntityExpression(inventoryEntity, containerEntity));
						return true;
					}
					containerEntity = inventoryEntity;
				}

				else
				{
					this.tokens.Add(new EntityExpression(inventoryEntity, containerEntity));
					return true;
				}
			}

			return false;
		}
		#endregion
	}
}