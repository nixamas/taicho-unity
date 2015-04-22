using System;
using System.Collections.Generic;

namespace com.cosmichorizons.basecomponents
{

	//import java.awt.Color;

	//using Color = com.badlogic.gdx.graphics.Color;
	using ComponentImages = com.cosmichorizons.enums.ComponentImages;
//	using LevelOneLegalMoves = com.cosmichorizons.enums.LevelOneLegalMoves;
//	using LevelThreeLegalMoves = com.cosmichorizons.enums.LevelThreeLegalMoves;
//	using LevelTwoLegalMoves = com.cosmichorizons.enums.LevelTwoLegalMoves;
	using Location = com.cosmichorizons.enums.Location;
	using Player = com.cosmichorizons.enums.Player;
	using Ranks = com.cosmichorizons.enums.Ranks;
	using SurroundingBCMoves = com.cosmichorizons.enums.SurroundingBCMoves;
	using BoardComponentNotFoundException = com.cosmichorizons.exceptions.BoardComponentNotFoundException;
	// TODO NEED TO FIX THIS!!!!!!!!!
//	using TaichoGameData = com.cosmichorizons.gameparts.TaichoGameData;
	using MoveManager = com.cosmichorizons.interfaces.MoveManager;


	/// <summary>
	/// All Characters will extend this object. 
	/// Extended by OneUnit.java, TwoUnit.java, ThreeUnit.java, Taicho.java, EmptyObject.java
	/// The getPossibleMoves method is used for all characters
	/// @author Ryan
	/// 
	/// </summary>
	public abstract class MovableObject
	{

		protected internal int combatValue;
		protected internal Player player;
		protected internal Ranks rank;
	//	protected final ComponentImages imageLocation;
		private bool surroundedByEnemies = false;

		/// <summary>
		/// Constructor for MovableObject sets the player, rank, and componentImages members
		/// the combat values is set based on what ever the rank is </summary>
		/// <param name="p"> </param>
		/// <param name="r"> </param>
		/// <param name="ci"> </param>
		public MovableObject(Player p, Ranks r)
		{
			player = p;
			rank = r;
			switch (rank)
			{
				case Ranks.NONE:
					this.combatValue = -1;
					break;
				case Ranks.LEVEL_ONE:
					this.combatValue = 1;
					break;
				case Ranks.LEVEL_TWO:
					this.combatValue = 2;
					break;
				case Ranks.LEVEL_THREE:
					this.combatValue = 3;
					break;
				case Ranks.TAICHO:
					this.combatValue = 1;
					break;
				default:
					this.combatValue = -1;
					break;
			}
		}

		public virtual int CombatValue
		{
			get
			{
				return this.combatValue;
			}
		}
	//	public Color getColor() {
	//		return player.getColor();
	//	}
	//
	//	public void setColor(Color color) {
	//		this.player.setColor(color);
	//	}
		public virtual Player Player
		{
			set
			{
				player = value;
			}
			get
			{
				return player;
			}
		}

		public virtual Ranks Rank
		{
			get
			{
				return rank;
			}
			set
			{
				this.rank = value;
				switch (value)
				{
					case Ranks.NONE:
						this.combatValue = -1;
						break;
					case Ranks.LEVEL_ONE:
						this.combatValue = 1;
						break;
					case Ranks.LEVEL_TWO:
						this.combatValue = 2;
						break;
					case Ranks.LEVEL_THREE:
						this.combatValue = 3;
						break;
					case Ranks.TAICHO:
						this.combatValue = 1;
						break;
					default:
						this.combatValue = -1;
						break;
				}
				this.rank = value;
			}
		}


	//	public ComponentImages getImageLocation() {
	//		return imageLocation;
	//	}

		public virtual bool SurroundedByEnemies
		{
			get
			{
				return surroundedByEnemies;
			}
			set
			{
				this.surroundedByEnemies = value;
			}
		}

		public virtual List<BoardComponent> getPossibleUnstackLocations(TaichoGameData board, BoardComponent bc) {
			List<BoardComponent> legalMoves = new List<BoardComponent>();
			List<MoveClass> mm = new List<MoveClass>();
			LevelOneLegalMovesClass levelOneLegalMoves = new LevelOneLegalMovesClass();
			MoveClass[] l1moves = levelOneLegalMoves.getMoves ();
			int count = l1moves.Length;
			for (int i = 0; i < count; i++)
			{
				mm.Add(l1moves[i]);
			}
			for (int i = 0; i < mm.Count; i++)
			{
				int changeVal = mm[i].getValue();//.getMove(i);
				try
				{
					BoardComponent potentialPosition = board.getBoardComponentAtId(bc.Id + changeVal);
					if (!potentialPosition.Occupied && potentialPosition.Location != Location.OUT_OF_BOUNDS)
					{
						potentialPosition.Highlight = true;
						legalMoves.Add(potentialPosition);
					}
				}
				catch (BoardComponentNotFoundException bcnfe)
				{
					Console.Error.WriteLine(bcnfe.Message);
				}
			}
			return legalMoves;
		}

		/// <summary>
		/// getPossibleMoves is called for all character objects that extend MovableObject.
		/// 
		/// based on the rank of 'this' object it will create an ArrayList<ArrayList<MoveManager>>
		/// 		that will contain all possible moves for that ranked object.
		/// 
		/// This double array is used to calculate whether objects are blocked by other objects. 
		/// The first set of arrays is the different paths that an object can take. Each array 
		/// of each path is made up of moves in order along the path radiating out from the object. 
		/// This method then looks through the double array and if a path is found to be blocked, a 
		/// boolean flag is set and the rest of the path is ignored. </summary>
		/// <param name="board"> </param>
		/// <param name="bc">
		/// @return </param>
		public virtual List<BoardComponent> getPossibleMoves(TaichoGameData board, BoardComponent bc)
		{
			List<BoardComponent> legalMoves = new List<BoardComponent>();
			List<List<MoveClass>> mm = new List<List<MoveClass>>();
			int bufferZone = 0;
			bool isTaicho = false;
			switch (rank)
			{
				case Ranks.NONE:
					break;
				case Ranks.LEVEL_ONE:
					LevelOneLegalMovesClass levelOneLegalMoves = new LevelOneLegalMovesClass();
					bufferZone = levelOneLegalMoves.getBufferValue();
					mm = levelOneLegalMoves.getBlockablePathsOfMoves();
					break;
				case Ranks.LEVEL_TWO:
					LevelTwoLegalMovesClass levelTwoLegalMoves = new LevelTwoLegalMovesClass();
					bufferZone = levelTwoLegalMoves.getBufferValue();
					mm = levelTwoLegalMoves.getBlockablePathsOfMoves();
					break;
				case Ranks.LEVEL_THREE:
					LevelThreeLegalMovesClass levelThreeLegalMoves = new LevelThreeLegalMovesClass();
					bufferZone = levelThreeLegalMoves.getBufferValue();
					mm = levelThreeLegalMoves.getBlockablePathsOfMoves();
					break;
				case Ranks.TAICHO:
					isTaicho = true;
					break;
				default:
					break;
			}

			if (!isTaicho)
			{
				Console.WriteLine("you clicked a samurai");
				legalMoves = getSamuraiMoves(mm, board, bc, bufferZone);
			}
			else if (isTaicho)
			{
				Console.WriteLine("you clicked a taicho");
				legalMoves = getTaichoMoves(board, bc);
			}

			return legalMoves;
		}

//		private bool canIBeKilled(TaichoGameData board, int id)
//		{
//			bool killed = false;
//			int powerTally = 0;
//			List<MoveManager> moves = SurroundingBCMoves.MoveManagerMoves;
//			List<BoardComponent> surroundingBoardComponents = getSurroundingBoardComponents(board, id, moves);
//			foreach (BoardComponent sbc in surroundingBoardComponents)
//			{
//				if (sbc.Occupied)
//				{
//					MovableObject character = sbc.Character;
//					if (this.Player != character.Player)
//					{
//						powerTally += character.CombatValue;
//					}
//				}
//			}
//			if (powerTally >= this.CombatValue)
//			{
//				killed = true;
//			}
//			return killed;
//		}

//		private List<BoardComponent> getSurroundingBoardComponents(TaichoGameData board, int id, List<MoveManager> mm)
//		{
//			List<BoardComponent> surroundingBoardComponents = new List<BoardComponent>();
//			foreach (MoveManager move in mm)
//			{
//				int changeVal = move.NumVal;
//				surroundingBoardComponents.Add(board.getBoardComponentAtId(id + changeVal));
//			}
//			return surroundingBoardComponents;
//		}

		/// <summary>
		/// called if the chosen object is a Taicho ranked object
		/// returns all unoccupied BC's within the players castle area </summary>
		/// <param name="board"> </param>
		/// <param name="bc">
		/// @return </param>
		private List<BoardComponent> getTaichoMoves(TaichoGameData board, BoardComponent bc)
		{
			List<BoardComponent> legalMoves = new List<BoardComponent>();
			List<BoardComponent> castle = board.getCastleBoardComponents(bc.Character.Player);
			foreach (BoardComponent potentialBc in castle)
			{
				if (!potentialBc.Equals(bc) && !potentialBc.Occupied)
				{
					potentialBc.Highlight = true;
					legalMoves.Add(potentialBc);
				}
				else if (!potentialBc.Equals(bc) && potentialBc.Occupied && potentialBc.CharacterPlayer != this.Player && potentialBc.Character.Rank == Ranks.LEVEL_ONE)
				{
						//an opposing players character has entered the castle
					potentialBc.Attackable = true;
					legalMoves.Add(potentialBc);
				}
			}
			return legalMoves;
		}

		/// <summary>
		/// called if the chosen object is a lvl1, lvl2, or lvl3 object </summary>
		/// <param name="mm"> </param>
		/// <param name="board"> </param>
		/// <param name="bc"> </param>
		/// <param name="bufferZone">
		/// @return </param>
		private List<BoardComponent> getSamuraiMoves(List<List<MoveClass>> mm, TaichoGameData board, BoardComponent bc, int bufferZone)
		{
			List<BoardComponent> legalMoves = new List<BoardComponent>();
			bool blockedPath = false;
			foreach (List<MoveClass> path in mm)
			{ //for each path in the list
				blockedPath = false;
				foreach (MoveClass move in path)
				{ //for each move in the path
					int changeVal = move.getValue(); //get change value (+/-X)
					if (!blockedPath)
					{ //if current path is not yet blocked
						try
						{
							BoardComponent potentialPosition = board.getBoardComponentAtId(bc.Id + changeVal);
							if (board.isWithinBufferZone(bufferZone, bc, potentialPosition))
							{ //if potPos is within bufferZone
								if (!potentialPosition.Occupied && potentialPosition.Location != Location.OUT_OF_BOUNDS)
								{
										//unoccupied BC
									potentialPosition.Highlight = true;
									legalMoves.Add(potentialPosition);
									if (potentialPosition.Barrier)
									{
										blockedPath = true;
									}
								}
								else if (potentialPosition.Occupied)
								{
									//occupied BC
									if (this.rank != Ranks.LEVEL_THREE && this.rank != Ranks.TAICHO)
									{
										//must be rank lvl1 or lvl2 to stack
										if (potentialPosition.Character.Player == bc.Character.Player && (potentialPosition.Character.Rank != Ranks.TAICHO && potentialPosition.Character.Rank != Ranks.LEVEL_THREE))
										{
											//BC is a stackable position
											potentialPosition.Stackable = true;
											legalMoves.Add(potentialPosition);
										}
									}
									if (this.player != potentialPosition.Character.Player)
									{
										MovableObject potentialOpponent = potentialPosition.Character;
										if (this.combatValue >= potentialOpponent.CombatValue)
										{
											//BC is a attackable position
											Console.WriteLine("Found a potential enemy of " + this.ToString() + " at -- " + potentialPosition.Coordinate.ToString());
											potentialPosition.Attackable = true;
											legalMoves.Add(potentialPosition);
										}
										//TODO
//										else if (potentialOpponent.canIBeKilled(board, potentialPosition.Id))
//										{
//											//potential oppenent can be killed by using multiple samurai
//											Console.WriteLine("A potential enemy can be killed by adding multiple samurais");
//											potentialPosition.Attackable = true;
//											potentialOpponent.SurroundedByEnemies = true;
//											legalMoves.Add(potentialPosition);
//										}
									}
									blockedPath = true;
								}
							}
						}
						catch (BoardComponentNotFoundException bcnfe)
						{
							Console.Error.WriteLine(bcnfe.Message);
						}
					} //if( !blockedPath)
				} //for(MoveManager move : path)
			} //for(ArrayList<MoveManager> path : mm)
			return legalMoves;
		}

		public override string ToString()
		{
			return "MovableObject [combatValue=" + combatValue + ", player=" + player + ", rank=" + rank + "]";
		}

//		public virtual string toSaveString()
//		{
//			return "{MovableObject:[player=" + player.Name + ", rank=" + rank.Name + "]}";
//		}

	}

}