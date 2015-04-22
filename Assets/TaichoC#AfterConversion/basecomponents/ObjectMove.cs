namespace com.cosmichorizons.basecomponents
{

	using EmptyObject = com.cosmichorizons.characters.EmptyObject;
	using Player = com.cosmichorizons.enums.Player;
	using Ranks = com.cosmichorizons.enums.Ranks;

	/// <summary>
	/// Class used to keep a track of the moves made in the game
	/// implemented for undo feature
	/// @author Ryan
	/// 
	/// </summary>
	public class ObjectMove
	{
		public enum MOVE_TYPE
		{
			ATTACK,
			MOVE,
			STACK,
			UNSTACK
		}

		private readonly Player player;
		private readonly Coordinate start;
		private readonly Coordinate finish;
		private readonly MOVE_TYPE moveType;
		private MovableObject victimOfAttack;

		public ObjectMove(Player p, Coordinate pos1, Coordinate pos2, ObjectMove.MOVE_TYPE type, MovableObject victim)
		{
			this.player = p;
			this.start = pos1;
			this.finish = pos2;
			this.moveType = type;
			if (this.moveType == ObjectMove.MOVE_TYPE.ATTACK)
			{
				this.victimOfAttack = victim;
			}
			else
			{
				this.victimOfAttack = null;
			}
		}

		/* (non-Javadoc)
		 * @see java.lang.Object#toString()
		 */
		public override string ToString()
		{
			string beg = "ObjectMove [player=" + player + ", start=" + start + ", finish=" + finish + ", moveType=" + moveType;
			if (this.victimOfAttack != null)
			{
				beg += ",\n\t\t character=" + victimOfAttack.ToString();
			}
			beg += "]";

			return beg;
		}

		public virtual MovableObject resurrectDeadCharacter()
		{
			return VictimOfAttack;
		}
		/// <returns> the victimOfAttack </returns>
		public virtual MovableObject VictimOfAttack
		{
			get
			{
				if (victimOfAttack != null)
				{
					return victimOfAttack;
				}
				else
				{
					return new EmptyObject();
				}
			}
			set
			{
				this.victimOfAttack = value;
			}
		}


		/// <returns> the player </returns>
		public virtual Player Player
		{
			get
			{
				return player;
			}
		}

		/// <returns> the start </returns>
		public virtual Coordinate Start
		{
			get
			{
				return start;
			}
		}

		/// <returns> the finish </returns>
		public virtual Coordinate Finish
		{
			get
			{
				return finish;
			}
		}

		/// <returns> the moveType </returns>
		public virtual MOVE_TYPE MoveType
		{
			get
			{
				return moveType;
			}
		}

	//	public ObjectMove(Player p, Coordinate pos1, Coordinate pos2, ObjectMove.MOVE_TYPE type){
	//		this.player = p;
	//		this.start = pos1;
	//		this.finish = pos2;
	//		this.moveType = type;
	//	}

	}

}