namespace Reverie.Maps
{
	using System.Collections.Generic;
	using PrimitiveEngine;
	using PrimitiveEngine.Components;


	public sealed class MapNodeComponent : Component
	{
		#region Fields
		private Room room;
		private RoomExits exits;
		private readonly List<long?> entityIds;
		#endregion


		#region Constructors
		public MapNodeComponent(Room room, RoomExits exits)
		{
			this.room = room;
			this.exits = exits;
			this.entityIds = new List<long?>();
		}


		public MapNodeComponent(Room room, RoomExits exits, List<long?> entityIds)
		{
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


		public RoomExits Exits
		{
			get { return this.exits; }
		}


		public Room Room
		{
			get { return this.room; }
		}
		#endregion


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