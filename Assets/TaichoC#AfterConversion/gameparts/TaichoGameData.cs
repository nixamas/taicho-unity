using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using com.cosmichorizons.enums;
using com.cosmichorizons.characters;
using com.cosmichorizons.exceptions;
using com.cosmichorizons.basecomponents;

namespace com.cosmichorizons.basecomponents
{
	public class TaichoGameData {
		public BoardComponent[,] board;
		public Player currentPlayer, gameWinner;
		public bool gameInPlay = true;
		//TODO need to figure out if this list object will work
		public List<ObjectMove> moves = new List<ObjectMove>();
		//private CollectionBase<ObjectMove> moves;

		/**
	 * Constructor. Create the board and set it up for a new game.
	 * Initialize the two players and create a 9*15 playing board
	 */
		public TaichoGameData() {


		}//TaichoGameData

		public void initialize () {
			Debug.Log ("Initializing TaichoGameData");
			gameWinner = Player.NONE;
			//			this.moves = new LinkedList<ObjectMove>();
			board = new BoardComponent[9, 15];
//			setUpGame(); // set up of game is now done in TaichGameGrid so that we only have to loop board once
			Debug.Log ("TaichoGameBoard has been Initialized");
		}

		public void nextTurn(Player nextPlayer){
			if( nextPlayer != Player.NONE){
				if( nextPlayer == Player.PLAYER_ONE ){
					Debug.Log("Starting player ones turn");
					this.currentPlayer = Player.PLAYER_ONE;
				}else if( nextPlayer == Player.PLAYER_TWO ){
					Debug.Log("Starting player twos turn");
					this.currentPlayer = Player.PLAYER_TWO;
				}
			}
		}
		/**
	 * Return the BC of the square in the specified row and column.
	 * If row or column are out of bounds a 'BoardComponentNotFoundException' is thrown
	 */
		public BoardComponent pieceAt(int row, int col) {
			if(row > 8 || col > 14){
				throw new BoardComponentNotFoundException();
			}else{
				return board[row, col];
			}
		}

		public BoardComponent componentFromCoord(Coordinate coor){
			//		BoardComponent bc = null;
			if(coor.PosX > 14 || coor.PosY > 8){
				throw new BoardComponentNotFoundException();
			}else{
				return board[coor.PosY, coor.PosX];
			}
			//		return bc;
		}

		/**
	 * Returns the Coordinate object of the BC found to have the 
	 * same id as the @param
	 * @param id
	 * @return
	 */
		public Coordinate getCoordinateOfId(int id){
			for (int col = 0; col < 15; col++) {
				for (int row = 0; row < 9; row++) {
					if(board[row, col].Coordinate.Id == id){
						return board[row, col].Coordinate;
					}
				}
			}
			return new Coordinate();//new BoardComponent(null, null);
		}

		/**
	 * Returns the BC object of the BC found to have the 
	 * same id as the @param
	 * @param id
	 * @return
	 */
		public BoardComponent getBoardComponentAtId(int id) {
			for (int col = 0; col < 15; col++) {
				for (int row = 0; row < 9; row++) {
					if(board[row, col].Coordinate.Id == id){
						//					//TODO Do i need this debug log Debug.Log("found BoardComponent of id - " + id + " at " + board[row][col].Coordinate);
						return board[row, col];
					}
				}
			}
			throw new BoardComponentNotFoundException();
		}

		/**
	 * Returns the single BC on the board that has its selected member set to true.
	 * If one is not found then throw a BoardComponentNotFoundException is thrown
	 * same id as the @param
	 * @param id
	 * @return
	 */
		public BoardComponent getSelectedBoardComponent(){
			for (int col = 0; col < 15; col++) {
				for (int row = 0; row < 9; row++) {
					if( board[row, col].Selected){
						return board[row, col];
					}
				}
			}
			throw new BoardComponentNotFoundException();
		}

		public void setSelectedBoardComponent(BoardComponent bc){
			//make sure nothing else is selected
			for (int col = 0; col < 15; col++) {
				for (int row = 0; row < 9; row++) {
					board[row, col].Selected = false;
				}
			}
			bc.Selected = true;
		}

		/**
	 * This method is used to makes sure that moves do not wrap around the board. 
	 * It makes sure that selectedBc and potentialBc are within the buffer zone for the appropriate chracter
	 * Returns true if potentialBc.row is within +/-bufferZone of selectedBc.row && 
	 * 		potentialBc.col is within +/-bufferZone of selectedBc.col 
	 * 
	 * The buffer zone is the maximum number of rows/cols that an object can move for its designated Rank
	 * lvl1 bufferZone - 1
	 * lvl2 bufferZone - 2
	 * lvl3 bufferZone - 3
	 * @param bufferZone
	 * @param selectedBc
	 * @param potentialBc
	 * @return
	 */
		public bool isWithinBufferZone(int bufferZone, BoardComponent selectedBc, BoardComponent potentialBc){
			Coordinate selectedCoor = selectedBc.Coordinate;
			Coordinate potentialCoor = potentialBc.Coordinate;
			if( (( potentialCoor.PosY <= (selectedCoor.PosY + bufferZone) ) && ( potentialCoor.PosY >= (selectedCoor.PosY - bufferZone))) &&
			   (( potentialCoor.PosX <= (selectedCoor.PosX + bufferZone) ) && ( potentialCoor.PosX >= (selectedCoor.PosX - bufferZone))) ){
				return true;
			}else{
				return false;
			}
		}

		/****************************************************************************************************************************************************************/
		
		/**
	 * returns all BC's contained in the castle of player 'p'
	 * @param p
	 * @return
	 */
		public List<BoardComponent> getCastleBoardComponents(Player p){
			List<BoardComponent> castleBc = new List<BoardComponent>();
			for (int col = 0; col < 15; col++) {
				for (int row = 0; row < 9; row++) {
					if( board[row, col].Location == Location.PLAYER_ONE_CASTLE && p == Player.PLAYER_ONE ){
						castleBc.Add(board[row, col]);
					}else if( board[row, col].Location == Location.PLAYER_TWO_CASTLE && p == Player.PLAYER_TWO ){
						castleBc.Add(board[row, col]);
					}
				}
			}
			return castleBc;
		}
		
		public Player getCurrentPlayer(){
			return this.currentPlayer;
		}
		public void setCurrentPlayer(Player currPlyr){
			this.currentPlayer = currPlyr;
		}
		public String getCurrentPlayerString(){
			String plyr = "NONE";
			if(this.currentPlayer == Player.PLAYER_ONE){
				plyr = "Player One";
			}else if(this.currentPlayer == Player.PLAYER_TWO){
				plyr = "Player Two";
			}
			return plyr;
		}
//		public Player getPlayer1() {
//			return player1;
//		}
//		
//		public void setPlayer1(Player player1) {
//			this.player1 = player1;
//		}
//		
//		public Player getPlayer2() {
//			return player2;
//		}
//		
//		public void setPlayer2(Player player2) {
//			this.player2 = player2;
//		}	
//		
//		public bool isGameInPlay(){
//			return this.gameInPlay;
//		}
		
		/**
     * Move units from one BC to another. params are coordinates of new location. 
     * @param coor
     */
		public void makeMove(Coordinate coor){
			//TODO Do i need this debug log Debug.Log("makeMove");
			BoardComponent bc = pieceAt(coor.PosY, coor.PosX);
			BoardComponent selectedBc = getSelectedBoardComponent();
			if(!bc.Occupied && bc.Location != Location.OUT_OF_BOUNDS){
				//TODO Do i need this debug log Debug.Log("square IS NOT occupied");
				MovableObject temp = selectedBc.removeCharacter();
				bc.Character = temp ;
			}
			selectedBc.Selected = false;
			addTurn(currentPlayer, selectedBc.Coordinate, bc.Coordinate, ObjectMove.MOVE_TYPE.MOVE, new EmptyObject() );
		}
		
		private void undoMove(ObjectMove lastTurn){
			//TODO Do i need this debug log Debug.Log("undoMove");
			BoardComponent startBc = componentFromCoord( lastTurn.Start );
			BoardComponent endBc = componentFromCoord( lastTurn.Finish );
			startBc.Character = endBc.removeCharacter();
		}
		
		/**
     * This method is called to stack one character on top of another character, of the same player. 
     * After some validation it will remove the character from its original place and replace it 
     * with a character of one higher rank
     * @param row
     * @param col
     * @return
     */
		public bool stackUnits(Coordinate coor){
			//TODO Do i need this debug log Debug.Log("stack units");
			bool success = true;
			BoardComponent bc;
			BoardComponent selectedBc; 
			try{
				bc = pieceAt(coor.PosY, coor.PosX);
				selectedBc = getSelectedBoardComponent();
				Player p = selectedBc.Character.Player;
				if( bc.Occupied && selectedBc.Occupied ){ //make sure both are occupied
					if( bc.Character.Player == p ){ //and belong to the same player
						if( !(bc.Character.Rank == Ranks.LEVEL_THREE || selectedBc.Character.Rank == Ranks.LEVEL_THREE) ){ //&& //not level three 
							if( !(bc.Character.Rank == Ranks.LEVEL_TWO && selectedBc.Character.Rank == Ranks.LEVEL_TWO) ){// ){ //or both are not level two
								if( bc.Character.Rank != Ranks.TAICHO && selectedBc.Character.Rank != Ranks.TAICHO ){			//neither are a taicho
									MovableObject selectedChar = selectedBc.removeCharacter();
									MovableObject joiningChar = bc.removeCharacter();
									MovableObject newChar = new EmptyObject();
									if( selectedChar.Rank == Ranks.LEVEL_ONE && joiningChar.Rank == Ranks.LEVEL_ONE ){
										newChar = new TwoUnit(p, selectedChar, joiningChar);
									}else if( (selectedChar.Rank == Ranks.LEVEL_ONE && joiningChar.Rank == Ranks.LEVEL_TWO) ||
									         (selectedChar.Rank == Ranks.LEVEL_TWO && joiningChar.Rank == Ranks.LEVEL_ONE) ){
										newChar = new ThreeUnit(p, selectedChar, joiningChar);
									}
									bc.Character = newChar;
									selectedBc.Selected = false;
									addTurn(currentPlayer, selectedBc.Coordinate, bc.Coordinate, ObjectMove.MOVE_TYPE.STACK, new EmptyObject() );
									success = true;
								}else{ success = false; }
							}else{ success = false; }
						}else{ success = false; }
					}else{ success = false; }
				}else{
					success = false;
				}
			}catch(BoardComponentNotFoundException bcnfe){
				Debug.LogError("Exception thrown while attempting to stack units  ::: " + bcnfe);
			}
			return success;
		}
		
		private void undoStack(ObjectMove lastTurn){
			//TODO Do i need this debug log Debug.Log("undoStack");
			BoardComponent startBc = componentFromCoord( lastTurn.Start );
			BoardComponent endBc = componentFromCoord( lastTurn.Finish );
			if( endBc.Occupied ){
				MovableObject unit = endBc.Character;
				switch( unit.Rank ){
				case Ranks.LEVEL_TWO:
					TwoUnit selectedTwoUnit = (TwoUnit) endBc.Character;
					MovableObject mo21 = selectedTwoUnit.removeUnitFromStack();
					MovableObject mo20 = selectedTwoUnit.removeUnitFromStack();
					startBc.Character = mo21;
					endBc.Character = mo20;
					break;
				case Ranks.LEVEL_THREE:
					ThreeUnit selectedThreeUnit = (ThreeUnit) endBc.Character;
					MovableObject mo32 = selectedThreeUnit.removeUnitFromStack();
					MovableObject mo31 = selectedThreeUnit.removeUnitFromStack();
					MovableObject mo30 = selectedThreeUnit.removeUnitFromStack();
					startBc.Character = mo32;
					endBc.Character = new TwoUnit( unit.Player, mo31, mo30 );
					break;
				case Ranks.LEVEL_ONE:
				case Ranks.TAICHO:
				case Ranks.NONE:
				default:	//no need to do anything
					//Debug.LogWarning("character is not able to be unstacked");
					break;
				}
			}
		}
		
		/**
	 * Method handles combat. verifies that the characters are able to battle
	 * @param row
	 * @param col
	 * @return
	 */
		public bool attackObject(Coordinate coor){
			//    public bool attackObject(BoardComponent victimBc){
			//TODO Do i need this debug log Debug.Log("Look at me ma, I'm attacking!!!");
			BoardComponent victimBc = pieceAt(coor.PosY, coor.PosX);
			BoardComponent attackingBc = getSelectedBoardComponent();
			if(victimBc.Occupied && attackingBc.Occupied){			//verify both squares are occupied
				MovableObject victimCharacter = victimBc.Character;
				MovableObject oppressingCharacter = attackingBc.Character;
				if(victimCharacter != oppressingCharacter && //must be opposite players and not a 'NONE' Player
				   (victimCharacter.Player != Player.NONE && oppressingCharacter.Player != Player.NONE)){	
					//TODO Do i need this debug log Debug.Log("Uh oh, looks like someone may be getting attacked..");
					if(victimBc.Attackable){
						//TODO Do i need this debug log Debug.Log("Yup, not looking good for you :: " + victimBc.Character.toString());
						if(oppressingCharacter.CombatValue >= victimCharacter.CombatValue && victimCharacter.Rank != Ranks.TAICHO){
							//TODO Do i need this debug log Debug.Log("..Bummer, you've been attacked -- " + victimBc.Character.toString());
							//	    				victimBc.removeCharacter(); //dead
							addTurn(currentPlayer, attackingBc.Coordinate, victimBc.Coordinate, ObjectMove.MOVE_TYPE.ATTACK, victimBc.removeCharacter() );
							victimBc.Character = attackingBc.removeCharacter();
							attackingBc.Selected = false;
							return true;
						}else if(oppressingCharacter.CombatValue >= victimCharacter.CombatValue && victimCharacter.Rank == Ranks.TAICHO){
							// A taicho character has been killed, game is over
							//TODO Do i need this debug log Debug.Log("Game is over, taicho is dead");
							this.gameWinner = oppressingCharacter.Player;
							this.gameInPlay = false;
						}else{
							//attacking character can beat victim using teammates
							//TODO Do i need this debug log Debug.Log("Multiple samurais are about to kill you..");
							//	    				victimBc.removeCharacter();
							addTurn(currentPlayer, attackingBc.Coordinate, victimBc.Coordinate, ObjectMove.MOVE_TYPE.ATTACK, victimBc.removeCharacter() );
							victimBc.Character = attackingBc.removeCharacter();
							attackingBc.Selected = false;
							return true;
						}
					}
				}else{
					//players were not right
					Debug.LogError("You cant attack your own team!");
					return false;
				}
			}else{
				//one or both BC's are not occupied
				Debug.LogError("You cant attack empty squares");
				return false;
			}
			return false;
		}
		
		
		private void undoAttack(ObjectMove lastTurn){
			//TODO Do i need this debug log Debug.Log("undoAttack");
			BoardComponent startBc = componentFromCoord( lastTurn.Start );
			BoardComponent endBc = componentFromCoord( lastTurn.Finish );
			MovableObject zombie = lastTurn.resurrectDeadCharacter();
			startBc.Character = endBc.removeCharacter ();
			endBc.Character = zombie;
		}
		
		/**
     * Does basically the opposite of makeMove.
     * @param row
     * @param col
     * @return
     */
		public bool unstackUnits(Coordinate coor){
			BoardComponent bc = pieceAt(coor.PosY, coor.PosX);
			BoardComponent selectedBc = getSelectedBoardComponent();
			Player p = selectedBc.Character.Player;
			if(!bc.Occupied && bc.Location != Location.OUT_OF_BOUNDS){
				//TODO Do i need this debug log Debug.Log("square IS NOT occupied");
				Ranks r = selectedBc.Character.Rank;
				switch(r){
				case Ranks.LEVEL_TWO:
					TwoUnit selectedTwoUnit = (TwoUnit) selectedBc.Character;
					MovableObject mo21 = selectedTwoUnit.removeUnitFromStack();
					MovableObject mo20 = selectedTwoUnit.removeUnitFromStack();
					bc.Character = mo21;
					selectedBc.Character = mo20;
					addTurn(currentPlayer, selectedBc.Coordinate, bc.Coordinate, ObjectMove.MOVE_TYPE.UNSTACK, new EmptyObject() );
					break;
				case Ranks.LEVEL_THREE:
					ThreeUnit selectedThreeUnit = (ThreeUnit) selectedBc.Character;
					MovableObject mo32 = selectedThreeUnit.removeUnitFromStack();
					MovableObject mo31 = selectedThreeUnit.removeUnitFromStack();
					MovableObject mo30 = selectedThreeUnit.removeUnitFromStack();
					bc.Character = mo32;
					selectedBc.Character = new TwoUnit( p, mo31, mo30 );
					addTurn(currentPlayer, selectedBc.Coordinate, bc.Coordinate, ObjectMove.MOVE_TYPE.UNSTACK, new EmptyObject() );
					break;
				case Ranks.LEVEL_ONE:
				case Ranks.TAICHO:
				case Ranks.NONE:
				default:	//no need to do anything
					Debug.LogError("character is not able to be unstacked");
					break;
				}
			}
			selectedBc.Selected = false;
			return true;
		}
		
		private void undoUnstack(ObjectMove lastTurn){
			//TODO Do i need this debug log Debug.Log("undoUnstack");
			BoardComponent startBc = componentFromCoord( lastTurn.Start );
			BoardComponent endBc = componentFromCoord( lastTurn.Finish );
			switch(startBc.Character.Rank){
			case Ranks.LEVEL_ONE:
				startBc.Character = new TwoUnit(lastTurn.Player, startBc.removeCharacter(), endBc.removeCharacter());
				break;
			case Ranks.LEVEL_TWO:
				startBc.Character = new ThreeUnit(lastTurn.Player, startBc.removeCharacter(), endBc.removeCharacter());
				break;
			default:
				break;
			}
			
		}
		
//		public IList<T> getMoves(){
//			return this.moves;
//		}
		
		private void addTurn(Player p, Coordinate startC, Coordinate endC, ObjectMove.MOVE_TYPE type, MovableObject r){
			ObjectMove move = new ObjectMove(p, startC, endC, type, r);
			//TODO Do i need this debug log Debug.Log("Adding Turn :::: " + move.toString() );
			moves.Add(move);
		}
		
		public void undoTurn(){
			performUndo();
		}
		
		private void performUndo(){
			ObjectMove lastTurn = moves[moves.Count];
			//TODO Do i need this debug log Debug.Log("LAST MOVE :: " + lastTurn.toString() );    	
			switch(lastTurn.MoveType){
				case ObjectMove.MOVE_TYPE.ATTACK:
					undoAttack(lastTurn);
					break;
				case ObjectMove.MOVE_TYPE.MOVE:
					undoMove(lastTurn);
					break;
				case ObjectMove.MOVE_TYPE.STACK:
					undoStack(lastTurn);
					break;
				case ObjectMove.MOVE_TYPE.UNSTACK:
					undoUnstack(lastTurn);
					break;
			default:
				//no op
				break;
			}
		}   
		
		public static Location getLocationFromBoardComponentId(int id){
			Location loc = Location.OUT_OF_BOUNDS;
			if( isBetween(id, 3, 5, 12, 14, 21, 23) ){
				loc = Location.PLAYER_ONE_CASTLE;
			}else if( isBetween(id, 27, 107) ){
				loc = Location.GAME_BOARD;
			}else if( isBetween(id, 111, 113, 120, 122, 129, 131) ){
				loc = Location.PLAYER_TWO_CASTLE;
			}
			return loc;
		}
		/**
     * returns true if x is between any of the 3 ranges
     * @param x
     * @param min1
     * @param max1
     * @param min2
     * @param max2
     * @param min3
     * @param max3
     * @return
     */
		private static bool isBetween(int x, int min1, int max1, int min2, int max2, int min3, int max3){
			if( isBetween(x, min1, max1) || isBetween(x, min2, max2) || isBetween(x, min3, max3) ){
				return true;
			}else{
				return false;
			}
		}
		private static bool isBetween(int x, int min, int max){
			if( x >= min && x <= max){
				return true;
			}else{
				return false;
			}
		}
		
		public Player getGameWinner() {
			return gameWinner;
		}


		/****************************************************************************************************************************************************************/











	}//class TaichoGameData
}


