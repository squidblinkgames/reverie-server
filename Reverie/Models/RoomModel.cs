namespace Reverie.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using PrimitiveEngine;
	using Reverie.Components;
	using Reverie.Maps;


	public class RoomModel
	{
		#region Fields
		private Guid? player;
		private IEnumerable<EntityModel> entities;
		private IEnumerable<Entity> characters;
		private IEnumerable<Entity> items;
		private string message;
		#endregion


		#region Constructors
		public RoomModel(MapNode mapNode, Entity player)
		{
			this.player = player.UniqueId;
			SaturateEntities(mapNode, player);
			GenerateDescription(mapNode);
		}
		#endregion


		#region Properties
		public IEnumerable<EntityModel> Entities
		{
			get { return this.entities; }
		}


		public List<RoomExit> Exits { get; set; }


		public string Message
		{
			get { return this.message; }
			set { this.message = value; }
		}


		public Guid? Player
		{
			get { return this.player; }
			set { this.player = value; }
		}
		#endregion


		#region Helper Methods
		private void GenerateDescription(MapNode mapNode)
		{
			StringBuilder description = new StringBuilder();
			Room room = mapNode.Room;

			description.AppendLine("# " + room.Name)
				.AppendLine()
				.AppendLine(room.Description)
				.AppendLine();

			foreach (Entity item in items)
			{
				EntityDataComponent itemData = item.GetComponent<EntityDataComponent>();
				description.AppendLine(
					"- There is a [" + itemData.Name + "]" +
					"(/items/" + item.UniqueId + ") here.");
			}

			foreach (Entity character in characters)
			{
				EntityDataComponent characterData = character.GetComponent<EntityDataComponent>();
				description.AppendLine(
					"- [" + characterData.Name + "]" +
					"(/characters/" + character.UniqueId + ") is here.");
			}

			description.AppendLine("Exits:");
			foreach (string exit in mapNode.Exits.ToStrings())
			{
				description.AppendLine("- [" + exit + "](/exits/" + exit.ToLower());
			}

			this.message = description.ToString();
		}


		private void SaturateEntities(MapNode mapNode, Entity player = null)
		{
			if (mapNode.EntityIds == null)
				return;

			List<EntityModel> entityModels = new List<EntityModel>();
			IEnumerable<Entity> roomEntities =
				from entity in mapNode.GetEntities()
				where entity != player
				select entity;

			this.characters =
				from entity in roomEntities
				where entity.HasComponent<CreatureComponent>()
				select entity;
			foreach (Entity character in this.characters)
			{
				EntityModel entityModel = new EntityModel(character);
				entityModels.Add(entityModel);
			}

			this.items =
				from entity in roomEntities
				where !entity.HasComponent<CreatureComponent>()
				select entity;
			foreach (Entity item in this.items)
			{
				EntityModel entityModel = new EntityModel(item);
				entityModels.Add(entityModel);
			}

			if (player != null)
				entityModels.Add(new EntityModel(player));

			this.entities = entityModels;
		}
		#endregion
	}
}