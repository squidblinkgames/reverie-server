namespace Reverie.Maps
{
	using System;
	using System.Collections.Generic;
	using PrimitiveEngine;
	using Reverie.Components;


	public sealed class Map
	{
		#region Fields
		private readonly string name;
		private readonly Dictionary<string, EntityDetails> rooms; 
		private readonly Dictionary<IntegerVector3, Entity> nodes;
		#endregion


		#region Constructors
		public Map(string name)
		{
			this.name = name;
			this.nodes = new Dictionary<IntegerVector3, MapNode>();
		}


		public Map(string name, Dictionary<IntegerVector3, MapNode> nodes)
		{
			this.name = name;
			this.nodes = nodes;
		}
		#endregion


		#region Properties
		public string Name
		{
			get { return this.name; }
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