namespace Reverie.Components
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using PrimitiveEngine;
	using PrimitiveEngine.Components;
	using Reverie.Maps;


	public sealed class MapNode : Component
	{
		#region Fields
		private readonly EntityWorld entityWorld;
		private Room room;
		private IList<RoomExit> exits;
		private readonly List<Guid> entityIds;
		#endregion


		#region Constructors
		public MapNode(EntityWorld entityWorld, Room room, IList<RoomExit> exits)
		{
			this.entityWorld = entityWorld;
			this.room = room;
			this.exits = exits;
			this.entityIds = new List<Guid>();
		}


		public MapNode(EntityWorld entityWorld, Room room, IList<RoomExit> exits, List<Guid> entityIds)
		{
			this.entityWorld = entityWorld;
			this.room = room;
			this.exits = exits;
			this.entityIds = entityIds;
		}
		#endregion


		#region Properties
		public IReadOnlyCollection<Guid> EntityIds
		{
			get { return this.entityIds.AsReadOnly(); }
		}


		public EntityWorld EntityWorld
		{
			get { return this.entityWorld; }
		}


		public IReadOnlyCollection<RoomExit> Exits
		{
			get { return new ReadOnlyCollection<RoomExit>(this.exits); }
		}


		public Room Room
		{
			get { return this.room; }
		}
		#endregion


		public bool AddEntity(Entity entity)
		{
			this.entityIds.Add(entity.UniqueId);
			return true;
		}


		public bool AddEntity(Guid entityId)
		{
			this.entityIds.Add(entityId);
			return true;
		}


		/// <summary>
		/// Gets the entities.
		/// </summary>
		/// <returns></returns>
		public IReadOnlyCollection<Entity> GetEntities()
		{
			if (this.EntityWorld == null)
				return null;

			List<Entity> entities = new List<Entity>();
			foreach (Guid uniqueId in this.entityIds)
			{
				Entity entity = this.EntityWorld.GetEntityByUniqueId(uniqueId);
				entities.Add(entity);
			}

			return entities.AsReadOnly();
		}
	}
}