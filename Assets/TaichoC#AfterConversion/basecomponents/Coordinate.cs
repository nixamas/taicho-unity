namespace com.cosmichorizons.basecomponents
{

	/// <summary>
	/// Every Board Component on the board will contain a coordinate object. 
	/// Look in the board.xls file for the board layout.
	/// 
	/// @author Ryan
	/// 
	/// </summary>
	public class Coordinate
	{
		private int id;
		private int posX, posY;
		public Coordinate(int posX, int posY, int id) : base()
		{
			this.id = id;
			this.posX = posX;
			this.posY = posY;
		}
		public Coordinate(int posX, int posY)
		{
			this.posX = posX;
			this.posY = posY;
			this.id = -1;
		}
		public Coordinate()
		{
			this.posX = -1;
			this.posY = -1;
			this.id = -1;
		}
		public virtual int Id
		{
			get
			{
				return id;
			}
			set
			{
				this.id = value;
			}
		}
		public virtual int PosX
		{
			get
			{
				return posX;
			}
			set
			{
				this.posX = value;
			}
		}
		public virtual int PosY
		{
			get
			{
				return posY;
			}
			set
			{
				this.posY = value;
			}
		}

		public override int GetHashCode()
		{
			const int prime = 31;
			int result = 1;
			result = prime * result + posX;
			result = prime * result + posY;
			return result;
		}
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			if (obj == null)
			{
				return false;
			}
			if (this.GetType() != obj.GetType())
			{
				return false;
			}
			Coordinate other = (Coordinate) obj;
			if (posX != other.posX)
			{
				return false;
			}
			if (posY != other.posY)
			{
				return false;
			}
			return true;
		}
		public override string ToString()
		{
			return "Coordinate [id=" + id + ", posX=" + posX + ", posY=" + posY + "]";
		}
		public virtual string toSaveString()
		{
			return "{Coordinate:[id=" + id + ", posX=" + posX + ", posY=" + posY + "]}";
		}

	}

}