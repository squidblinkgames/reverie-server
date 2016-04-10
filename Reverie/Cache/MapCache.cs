namespace Reverie.Cache
{
	using System.Collections;
	using System.Collections.Generic;
	using Reverie.Maps;


	public sealed class MapCache : IEnumerable<Map>
	{
		#region Fields
		private readonly Dictionary<string, Map> maps;
		#endregion


		#region Constructors
		public MapCache()
		{
			this.maps = new Dictionary<string, Map>();
		}
		#endregion


		#region Indexers
		public Map this[string mapName]
		{
			get { return this.maps[mapName]; }
		}
		#endregion


		public void AddMap(string mapName, Map map)
		{
			if (this.maps.ContainsKey(mapName))
				return;
			this.maps.Add(mapName, map);
		}


		public IEnumerator<Map> GetEnumerator()
		{
			return this.maps.Values.GetEnumerator();
		}


		#region Helper Methods
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		#endregion
	}
}