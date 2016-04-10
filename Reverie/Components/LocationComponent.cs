namespace Reverie.Components
{
	using Newtonsoft.Json;
	using PrimitiveEngine;
	using PrimitiveEngine.Components;


	[JsonObject(MemberSerialization.OptIn)]
	public class LocationComponent : Component
	{
		#region Fields
		private string map;
		private int x;
		private int y;
		private int z;
		#endregion


		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="LocationComponent"/> class.
		/// </summary>
		/// <param name="map">The game world id.</param>
		/// <param name="localeId">The locale id.</param>
		/// <param name="x">The X position.</param>
		/// <param name="y">The Y position.</param>
		/// <param name="z">The Z position.</param>
		public LocationComponent(
			string map,
			int x,
			int y,
			int z)
		{
			this.map = map;
			this.x = x;
			this.y = y;
			this.z = z;
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="LocationComponent"/> class.
		/// </summary>
		/// <param name="map">The game world id.</param>
		/// <param name="localeId">The locale id.</param>
		/// <param name="position">The X, Y, Z position.</param>
		public LocationComponent(
			string map,
			IntegerVector3 position)
		{
			this.map = map;
			this.x = position.X;
			this.y = position.Y;
			this.z = position.Z;
		}
		#endregion


		#region Properties
		/// <summary>
		/// Gets or sets the game world id of the location.
		/// </summary>
		/// <value>The game world id.</value>
		[JsonProperty]
		public string Map
		{
			get { return this.map; }
			set { this.map = value; }
		}

		
		/// <summary>
		/// Gets or sets the X, Y, and Z position of the location.
		/// </summary>
		/// <value>The position.</value>
		public IntegerVector3 Position
		{
			get { return new IntegerVector3(this.x, this.y, this.z); }
			set
			{
				this.x = value.X;
				this.y = value.Y;
				this.z = value.Z;
			}
		}


		/// <summary>
		/// The location's X coordinate.
		/// </summary>
		/// <value>X coordinate.</value>
		[JsonProperty]
		public int X
		{
			get { return this.x; }
			set { this.x = value; }
		}


		/// <summary>
		/// The location's Y coordinate.
		/// </summary>
		/// <value>Y coordinate.</value>
		[JsonProperty]
		public int Y
		{
			get { return this.y; }
			set { this.y = value; }
		}


		/// <summary>
		/// The location's Z coordinate.
		/// </summary>
		/// <value>Z coordinate.</value>
		[JsonProperty]
		public int Z
		{
			get { return this.z; }
			set { this.z = value; }
		}
		#endregion


		public int[] GetPositionArray()
		{
			return new[] { this.x, this.y, this.z };
		}
	}
}