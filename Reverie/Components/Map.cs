namespace Reverie.Components
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using PrimitiveEngine;
	using PrimitiveEngine.Components;


	public sealed class Map : Component
	{
		#region Fields
		private readonly Dictionary<string, EntityDetails> rooms;
		private readonly Dictionary<IntegerVector3, Entity> nodes;
		#endregion


		#region Constructors
		public Map()
		{
			this.rooms = new Dictionary<string, EntityDetails>();
			this.nodes = new Dictionary<IntegerVector3, Entity>();
		}
		#endregion


		#region Properties
		public IReadOnlyDictionary<IntegerVector3, Entity> Nodes
		{
			get { return new ReadOnlyDictionary<IntegerVector3, Entity>(this.nodes); }
		}
		#endregion


		#region Indexers
		public Entity this[IntegerVector3 coordinates]
		{
			get
			{
				if (this.nodes.ContainsKey(coordinates))
					return this.nodes[coordinates];
				return null;
			}
		}


		public Entity this[int x, int y, int z]
		{
			get { return this[new IntegerVector3(x, y, z)]; }
		}
		#endregion


		public void AddNode(IntegerVector3 coordinates, string room)
		{
			if (this.nodes.ContainsKey(coordinates))
				throw new ArgumentException("Map coordinate already exists.");
			this.nodes.Add(coordinates, mapNode);
		}


		public void AddNode(int x, int y, int z, MapNode mapNode)
		{
			IntegerVector3 coordinate = new IntegerVector3(x, y, z);
			AddNode(coordinate, mapNode);
		}
	}
}