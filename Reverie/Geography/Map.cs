namespace Reverie.Maps
{
	using System.Collections.Generic;
	using PrimitiveEngine;


	public sealed class Map
	{
		#region Fields
		private readonly Dictionary<IntegerVector3, MapNodeComponent> map;
		#endregion


		#region Indexers
		public MapNodeComponent this[IntegerVector3 coordinates]
		{
			get
			{
				if (this.map.ContainsKey(coordinates))
					return this.map[coordinates];
				return null;
			}
		}


		public MapNodeComponent this[int x, int y, int z]
		{
			get { return this[new IntegerVector3(x, y, z)]; }
		}
		#endregion
	}
}