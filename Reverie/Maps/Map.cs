namespace Reverie.Maps
{
	using System;
	using System.Collections.Generic;
	using PrimitiveEngine;


	public sealed class Map
	{
		#region Fields
		private readonly string name;
		private readonly Dictionary<IntegerVector3, MapNode> map;
		#endregion


		#region Constructors
		public Map(string name)
		{
			this.name = name;
			this.map = new Dictionary<IntegerVector3, MapNode>();
		}


		public Map(string name, Dictionary<IntegerVector3, MapNode> map)
		{
			this.name = name;
			this.map = map;
		}
		#endregion


		#region Properties
		public string Name
		{
			get { return this.name; }
		}
		#endregion


		#region Indexers
		public MapNode this[IntegerVector3 coordinates]
		{
			get
			{
				if (this.map.ContainsKey(coordinates))
					return this.map[coordinates];
				return null;
			}
		}


		public MapNode this[int x, int y, int z]
		{
			get { return this[new IntegerVector3(x, y, z)]; }
		}
		#endregion


		public void AddNode(IntegerVector3 coordinates, MapNode mapNode)
		{
			if (this.map.ContainsKey(coordinates))
				throw new ArgumentException("Map coordinate already exists.");
			this.map.Add(coordinates, mapNode);
		}


		public void AddNode(int x, int y, int z, MapNode mapNode)
		{
			IntegerVector3 coordinate = new IntegerVector3(x, y, z);
			AddNode(coordinate, mapNode);
		}
	}
}