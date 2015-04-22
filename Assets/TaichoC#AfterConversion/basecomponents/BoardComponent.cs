using System.Text;

namespace com.cosmichorizons.basecomponents
{
	//import java.awt.Color;

	//using Color = com.badlogic.gdx.graphics.Color;
	using EmptyObject = com.cosmichorizons.characters.EmptyObject;
	using Location = com.cosmichorizons.enums.Location;
	using Player = com.cosmichorizons.enums.Player;
	//using TaichoColors = com.cosmichorizons.enums.TaichoColors;


	/// <summary>
	/// Each square on the board is made of its own BoardComponent. 
	/// BoardComponent will hold the character if it is occupied. 
	/// Each BC will have a coordinate that corresponds to a position of the board and 
	/// 		an index value which can be seen in the board.xls file.
	/// @author Ryan
	/// 
	/// </summary>
	public class BoardComponent
	{

		private readonly Coordinate coordinate;
		private readonly Location location;
	//	private Color color = Color.BLACK;
		private bool occupied = false;
		private bool stackable = false;
		private bool selected = false;
		private bool attackable = false;
		private bool barrier, timeServed;
		private MovableObject character;
		private bool highlight;

		public BoardComponent(MovableObject character, Location loc, Coordinate coord)
		{
			this.occupied = true;
			this.character = character;
			this.location = loc;
			this.coordinate = coord;
		}
		public BoardComponent(Location loc, Coordinate coord)
		{
			this.occupied = false;
			this.location = loc;
			this.coordinate = coord;
			this.character = new EmptyObject();
		}
		public virtual bool Occupied
		{
			get
			{
				if (this.character.Player != Player.NONE)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		public virtual Player CharacterPlayer
		{
			get
			{
				return this.Character.Player;
			}

			set
			{
				this.Character.Player = value;
			}

		}
		public virtual MovableObject Character
		{
			get
			{
				if (character != null)
				{
					return character;
				}
				else
				{
					return new EmptyObject();
				}
			}
			set
			{
				this.character = value;
			}
		}
		public virtual MovableObject removeCharacter()
		{
			MovableObject tempChar = this.character;
			this.character = new EmptyObject();
			return tempChar;
		}
		public virtual Location Location
		{
			get
			{
				return location;
			}
		}
		public virtual int Id
		{
			get
			{
				return coordinate.Id;
			}
			set
			{
				this.coordinate.Id = value;
			}
		}
		public virtual Coordinate Coordinate
		{
			get
			{
				return coordinate;
			}
		}
	//	public void setCoordinate(Coordinate coordinate) {
	//		this.coordinate = coordinate;
	//	}
	//	public Color getColor() {
	//		if(selected){
	//			return TaichoColors.GAME_BOARD_SELECTED.getColor();
	//		}else if(highlight){
	//			return TaichoColors.GAME_BOARD_HIGHLIGHT.getColor();
	//		}else if(stackable){
	//			return TaichoColors.GAME_BOARD_STACKABLE.getColor();
	//		}else if(attackable){
	//			return TaichoColors.GAME_BOARD_ATTACKABLE.getColor();
	//		}else{
	//			return color;
	//		}
	//	}
	//	public void setColor(Color color) {
	//		this.color = color;
	//	}
		public virtual bool Highlight
		{
			get
			{
				return highlight;
			}
			set
			{
				this.highlight = value;
			}
		}
		public virtual bool Stackable
		{
			get
			{
				return stackable;
			}
			set
			{
				this.stackable = value;
			}
		}
		public virtual bool Selected
		{
			get
			{
				return selected;
			}
			set
			{
				this.selected = value;
			}
		}
		public virtual bool Attackable
		{
			get
			{
				return attackable;
			}
			set
			{
				this.attackable = value;
			}
		}
		public virtual bool Barrier
		{
			get
			{
				return barrier;
			}
			set
			{
				this.barrier = value;
			}
		}
		public virtual bool TimeServed
		{
			get
			{
				return timeServed;
			}
			set
			{
				this.timeServed = value;
			}
		}

		public virtual string toDebugString(string spltr)
		{
	//		return "BoardComponent [coordinate=" + coordinate + ", location="
	//				+ location + ", color=" + color + ", occupied=" + occupied
	//				+ "\n\t" + "highlight=" + highlight + ", character=" + character + "]";
			StringBuilder buff = new StringBuilder();
			buff.Append("BoardComponent :: " + coordinate + spltr);
			buff.Append("Location :: " + location + spltr);
			buff.Append("high-occ :: " + highlight + "-" + occupied + spltr);
			buff.Append("Char :: " + character + spltr);
			buff.Append("isBarrier :: " + barrier);

			return buff.ToString();
		}

		public override string ToString()
		{
			return "BoardComponent [coordinate=" + coordinate + ", location=" + location + ", occupied=" + occupied + "\n\t" + "highlight=" + highlight + ", character=" + character + "]";
		}
		public override int GetHashCode()
		{
			const int prime = 31;
			int result = 1;
			result = prime * result + ((coordinate == null) ? 0 : coordinate.GetHashCode());
			result = prime * result + (location.GetHashCode());
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
			BoardComponent other = (BoardComponent) obj;
			if (coordinate == null)
			{
				if (other.coordinate != null)
				{
					return false;
				}
			}
			else if (!coordinate.Equals(other.coordinate))
			{
				return false;
			}
			if (location != other.location)
			{
				return false;
			}
			return true;
		}
	}

}