namespace Reverie.Maps
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using PrimitiveEngine;


	public sealed class MapNode
	{
		#region Fields
		private readonly EntityWorld entityWorld;
		private Room room;
		private IList<RoomExit> exits;
		private readonly List<long?> entityIds;
		#endregion


		#region Constructors
		public MapNode(EntityWorld entityWorld, Room room, IList<RoomExit> exits)
		{
			this.entityWorld = entityWorld;
			this.room = room;
			this.exits = exits;
			this.entityIds = new List<long?>();
		}


		public MapNode(EntityWorld entityWorld, Room room, IList<RoomExit> exits, List<long?> entityIds)
		{
			this.entityWorld = entityWorld;
			this.room = room;
			this.exits = exits;
			this.entityIds = entityIds;
		}
		#endregion


		#region Properties
		public IReadOnlyCollection<long?> EntityIds
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


		public bool AddEntity(long entityId)
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
			foreach (long uniqueId in this.entityIds)
			{
				Entity entity = this.EntityWorld.GetEntityByUniqueId(uniqueId);
				entities.Add(entity);
			}

			return entities.AsReadOnly();
		}
	}
}