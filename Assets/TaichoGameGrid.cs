using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.cosmichorizons.basecomponents;
using com.cosmichorizons.characters;
using com.cosmichorizons.enums;
using com.cosmichorizons.exceptions;

public class TaichoGameGrid : MonoBehaviour {
	private int rows = 9;
	private int columns = 15;
	private float distanceBetweenTiles = 2.3F;
	private static int tilesCount = 135;
	public Tile tilePrefab;	//defined in GUI
	public HighlightTileSprite highlightTileSpritePrefab;
	private Tile[] tiles = new Tile[tilesCount];

	private bool unstackObjects, showIcons; // Is a game currently in progress? //probably need to move these elsewhere
	private Tile selectedTile;
	public TaichoGameData taicho = new TaichoGameData ();
	public List<BoardComponent> validMoves = new List<BoardComponent>();
	//List<BoardComponent> validMoves; // An array containing the legal moves for the
	// current player.

	// Use this for initialization
	void Start () {
		taicho.initialize ();
		createTiles();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void createTiles() {
		float xOffset = 0.0F;
		float zOffset = 0.0F;
		int index = 0;
		for (var col = 0; col < columns; col++) {
			xOffset += distanceBetweenTiles * col;
			//draw columns
			for (var row = 0; row < rows; row++) {
				// draw rows
				zOffset += distanceBetweenTiles * row;
				Tile tile = (Tile)Instantiate(tilePrefab, new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z + zOffset), transform.rotation);
				HighlightTileSprite highlight = (HighlightTileSprite)Instantiate(highlightTileSpritePrefab, new Vector3(transform.position.x + xOffset, transform.position.y+2, transform.position.z + zOffset), highlightTileSpritePrefab.transform.rotation);
				tile.initializeHighlighter(highlight);

				if ( (col < 3 && (row < 3 || row > 5) || (col > 11 && (row < 3 || row > 5)))){
					//Debug.Log("skipping displaying tile at coordinate column[" + col + "] - row[" + row + "]");
					taicho.board[row, col] = new BoardComponent(new TaichoUnit(Player.NONE), Location.OUT_OF_BOUNDS, new Coordinate(col, row, index));
					//TODO replace with actual unstack button logic
					if (index == 1) {
						tile.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
					} else {
						tile.gameObject.GetComponent<Renderer>().material.color = Color.clear;
					}
				} else if (row == 4 && col == 1) {
					// position is Player One Taicho
					taicho.board[row, col] = new BoardComponent(new TaichoUnit(Player.PLAYER_ONE), Location.PLAYER_ONE_CASTLE, new Coordinate(col, row, index));
					tile.gameObject.GetComponent<Renderer>().material.color = Color.red;
				} else if (row == 4 && col == 13) {
					// position is Player Two Taicho
					taicho.board[row, col] = new BoardComponent(new TaichoUnit(Player.PLAYER_TWO), Location.PLAYER_TWO_CASTLE, new Coordinate(col, row, index));
					tile.gameObject.GetComponent<Renderer>().material.color = Color.blue;
				} else if (((col == 4) && (row % 2 == 0))
				           || ((col == 3 || col == 5) && (row % 2 == 1))) {
					// position is Player One Samurai
					taicho.board[row, col] = new BoardComponent(new OneUnit(Player.PLAYER_ONE), Location.GAME_BOARD, new Coordinate(col, row, index));
					//tile.gameObject.GetComponent<Renderer>().material.color = Color.magenta;
					tile.gameObject.GetComponent<Renderer>().material.color = taicho.board[row, col].CharacterPlayer.getPlayerColor();
				} else if (((col == 10) && (row % 2 == 0))
				           || ((col == 9 || col == 11) && (row % 2 == 1))) {
					// position is Player Two Samurai
					taicho.board[row, col] = new BoardComponent(new OneUnit(Player.PLAYER_TWO), Location.GAME_BOARD, new Coordinate(col, row, index));
					//tile.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
					tile.gameObject.GetComponent<Renderer>().material.color = taicho.board[row, col].CharacterPlayer.getPlayerColor();
				} else {
					taicho.board[row, col] = new BoardComponent(Location.GAME_BOARD, new Coordinate(col, row, index));
				}
				//easy to have a reference to bc from gui object
				tile.boardComponent = taicho.getBoardComponentAtId(index);
				tiles[index] = tile;
				index++;
				zOffset = 0;

			}
			xOffset = 0;
		}

		Debug.Log ("Game Board object have been created");


		
		
//		for(int tilesCreated = 0; tilesCreated < 6; tilesCreated += 1) {
//	        xOffset += distanceBetweenTiles;
//	        
//	        if(tilesCreated % 3 == 0) {
//	            zOffset += distanceBetweenTiles;
//	            xOffset = 0;
//	        }
//	        Instantiate(tilePrefab, new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z + zOffset), transform.rotation);
//	    }
	}









	public void onUnstackButtonClicked() {
		if (selectedTile != null) {
			erasePossibleMoves(true); //erase shown moves but preserve the currently selected tile
			validMoves = selectedTile.boardComponent.Character.getPossibleUnstackLocations (taicho, selectedTile.boardComponent);
			highlightPossibleUnstackMovesForSelectedComponent ();
			unstackObjects = true;
		}
	}



	//They tile clicked will call this method
	public void onTileClicked(Tile tile) {
		try{
			BoardComponent bc = tile.boardComponent;			
//			Debug.Log("Tile Selected :: " + tile);
			if(bc.Location != Location.OUT_OF_BOUNDS){	//if player clicks on the game board
				if(validMoves.Count == 0 && bc.Occupied) {		//if there is no valid moves (no BC selected)
					//TODO doIneedThisStatement Debug.Log("valid moves is empty, first click");
					//selectBoardComponent(row, col);
					taicho.currentPlayer = bc.Character.Player;
					selectedTile = tile;
					tile.highlightSelected ();
					validMoves = bc.Character.getPossibleMoves(taicho, bc);
					highlightPossibleMovesForSelectedComponent();
				}else if( !(validMoves.Count == 0) && validSelection(bc)){
					// if there is a selected BC and user chose a valid BC
					//TODO doIneedThisStatement Debug.Log("make move to new VALID square");
					try{
						BoardComponent selectedBc = selectedTile.boardComponent;
						
						if( bc.Occupied && bc.Character.Player == selectedBc.Character.Player ){
							//both square are occupied by the same player
							Debug.Log("StackUnits");
							stackUnits(tile);
						}else if( bc.Occupied && bc.Character.Player != selectedBc.Character.Player ){
							//both squares are occupied by opposite players
							//TODO doIneedThisStatement Debug.Log("AttackUnits");
							attackObject(tile);
						}else if( !bc.Occupied ){
							if(unstackObjects){
								Debug.Log("UnstackUnits");
								unstackUnits(tile);
								unstackObjects = false;
							}else{
								Debug.Log("MoveUnits");
								makeMove(tile);
							}
						}
					}catch(BoardComponentNotFoundException bcnfe){
						Debug.LogError(bcnfe.Message);
					}
//TODO					eraseValidMoves();
					erasePossibleMoves();
				}else if (selectedTile != null) {

					//TODO doIneedThisStatement Debug.Log("checking if user clicked selected BC again. If so, Abort");
					try{
						BoardComponent selectedBc = selectedTile.boardComponent;
						if(bc.Coordinate == selectedBc.Coordinate ){
							//user clicked same BC twice, abort the BC selection
							//if there is a selected BC then and the user clicked it a second time, 
							//Then clear the valid moves array
							tile.GetComponent<Renderer> ().material.color = selectedTile.boardComponent.CharacterPlayer.getPlayerColor();
							selectedBc.Selected = false;
							erasePossibleMoves();
//TODO							eraseValidMoves();
						}

					}catch(BoardComponentNotFoundException bcnfe){
						Debug.LogError(bcnfe.Message);
					}
				}
			}else{
				try{
					BoardComponent selectedBc = selectedTile.boardComponent;
					selectedBc.Selected = false;
//TODO					eraseValidMoves();
					erasePossibleMoves();
				}catch(BoardComponentNotFoundException bcnfe){
					Debug.LogError(bcnfe.Message);
				}
			}
		}catch(BoardComponentNotFoundException bcnfe){
			Debug.LogError(bcnfe.Message);
		}catch(UnityException e){
			Debug.LogError(e);
		}
		
		//TODO setButtonState();
	}

	private void makeMove (Tile destinationTile) {
		Player player = selectedTile.boardComponent.CharacterPlayer;
		destinationTile.GetComponent<Renderer> ().material.color = player.getPlayerColor ();
		selectedTile.GetComponent<Renderer> ().material.color = Color.white;
		destinationTile.boardComponent.Character = selectedTile.boardComponent.removeCharacter ();
		destinationTile.boardComponent.CharacterPlayer = player;
		selectedTile.boardComponent.Selected = false;
		erasePossibleMoves ();
	}

	/**
     * This method is called to stack one character on top of another character, of the same player. 
     * After some validation it will remove the character from its original place and replace it 
     * with a character of one higher rank
     * @param row
     * @param col
     * @return
     */
	private bool stackUnits(Tile tile){
		BoardComponent selectedBc = selectedTile.boardComponent;
		BoardComponent baseBc = tile.boardComponent;
		Player p = selectedBc.Character.Player;
		if( baseBc.Occupied && selectedBc.Occupied ){ //make sure both are occupied
			if( baseBc.Character.Player == p ){ //and belong to the same player
				if( !(baseBc.Character.Rank == Ranks.LEVEL_THREE || selectedBc.Character.Rank == Ranks.LEVEL_THREE) ){ //&& //not level three 
					if( !(baseBc.Character.Rank == Ranks.LEVEL_TWO && selectedBc.Character.Rank == Ranks.LEVEL_TWO) ){// ){ //or both are not level two
						if( baseBc.Character.Rank != Ranks.TAICHO && selectedBc.Character.Rank != Ranks.TAICHO ){			//neither are a taicho
							MovableObject selectedChar = selectedBc.removeCharacter();
							MovableObject joiningChar = baseBc.removeCharacter();
							MovableObject newChar = new EmptyObject();
							if( selectedChar.Rank == Ranks.LEVEL_ONE && joiningChar.Rank == Ranks.LEVEL_ONE ){
								newChar = new TwoUnit(p, selectedChar, joiningChar);
							}else if( (selectedChar.Rank == Ranks.LEVEL_ONE && joiningChar.Rank == Ranks.LEVEL_TWO) ||
							         (selectedChar.Rank == Ranks.LEVEL_TWO && joiningChar.Rank == Ranks.LEVEL_ONE) ){
								newChar = new ThreeUnit(p, selectedChar, joiningChar);
							}
							baseBc.Character = newChar;
							selectedBc.Selected = false;

							//TODO Replace object with another character for each level
							selectedTile.GetComponent<Renderer> ().material.color = Color.white;
							erasePossibleMoves ();

							return true;
						}
					}
				}
			}
		}
		return false;    	
	}

	/**
     * Does basically the opposite of makeMove.
     * @param row
     * @param col
     * @return
     */
	private bool unstackUnits(Tile destinationTile){
		BoardComponent bc = destinationTile.boardComponent;
		BoardComponent selectedBc = selectedTile.boardComponent;
		Player p = selectedBc.Character.Player;
		if(!bc.Occupied && bc.Location != Location.OUT_OF_BOUNDS){
			Ranks r = selectedBc.Character.Rank;
			switch(r){
			case Ranks.LEVEL_TWO:
				TwoUnit selectedTwoUnit = (TwoUnit) selectedBc.Character;
				MovableObject mo21 = selectedTwoUnit.removeUnitFromStack();
				MovableObject mo20 = selectedTwoUnit.removeUnitFromStack();
				bc.Character = mo21;
				selectedBc.Character = mo20;
				break;
			case Ranks.LEVEL_THREE:
				ThreeUnit selectedThreeUnit = (ThreeUnit) selectedBc.Character;
				MovableObject mo32 = selectedThreeUnit.removeUnitFromStack();
				MovableObject mo31 = selectedThreeUnit.removeUnitFromStack();
				MovableObject mo30 = selectedThreeUnit.removeUnitFromStack();
				bc.Character =  mo32;
				selectedBc.Character = new TwoUnit( p, mo31, mo30 );
				break;
			case Ranks.LEVEL_ONE:
			case Ranks.TAICHO:
			case Ranks.NONE:
			default:	//no need to do anything
				break;
			}
		}
		destinationTile.GetComponent<Renderer> ().material.color = p.getPlayerColor ();
		selectedBc.Selected = false;
		erasePossibleMoves ();
		return true;
	}


	/**
	 * Method handles combat. verifies that the characters are able to battle
	 * @param row
	 * @param col
	 * @return
	 */
	public bool attackObject(Tile victimsTile){
		//    public boolean attackObject(BoardComponent victimBc){
		Debug.Log("Look at me ma, I'm attacking!!!");
		BoardComponent victimBc = victimsTile.boardComponent;
		BoardComponent attackingBc = selectedTile.boardComponent;
		if(victimBc.Occupied && attackingBc.Occupied){			//verify both squares are occupied
			MovableObject victimCharacter = victimBc.Character;
			MovableObject oppressingCharacter = attackingBc.Character;
			if(victimCharacter != oppressingCharacter && //must be opposite players and not a 'NONE' Player
			   (victimCharacter.Player != Player.NONE && oppressingCharacter.Player != Player.NONE)){	
				Debug.Log("Uh oh, looks like someone may be getting attacked...");
				if(victimBc.Attackable){
					Debug.Log("Yup, not looking good for you :: " + victimBc.Character);
					if(oppressingCharacter.CombatValue >= victimCharacter.CombatValue && victimCharacter.Rank != Ranks.TAICHO){
						Debug.Log("...Bummer, you've been attacked -- " + victimBc.Character);
						//	    				victimBc.removeCharacter(); //dead
						//addTurn(currentPlayer, attackingBc.getCoordinate(), victimBc.getCoordinate(), ObjectMove.MOVE_TYPE.ATTACK, victimBc.removeCharacter() );
						victimBc.Character = attackingBc.removeCharacter();
					}else if(oppressingCharacter.CombatValue >= victimCharacter.CombatValue && victimCharacter.Rank == Ranks.TAICHO){
						// A taicho character has been killed, game is over
						Debug.Log("Game is over, taicho is dead");
						this.taicho.gameWinner = oppressingCharacter.Player;
						this.taicho.gameInPlay = false;
					}else{
						//attacking character can beat victim using teammates
						Debug.Log("Multiple samurais are about to kill you...");
						//	    				victimBc.removeCharacter();
						//addTurn(currentPlayer, attackingBc.getCoordinate(), victimBc.getCoordinate(), ObjectMove.MOVE_TYPE.ATTACK, victimBc.removeCharacter() );
						victimBc.Character = attackingBc.removeCharacter();
					}
					attackingBc.Selected = false;
					selectedTile.GetComponent<Renderer> ().material.color = Color.white;
					victimsTile.GetComponent<Renderer> ().material.color = oppressingCharacter.Player.getPlayerColor();
					erasePossibleMoves();
					return true;
				}
			}else{
				//players were not right
				Debug.Log("You cant attack your own team!");
				return false;
			}
		}else{
			//one or both BC's are not occupied
			Debug.Log("You cant attack empty squares");
			return false;
		}
		return false;
	}


//	/**
//     * This is called by mousePressed() when a player clicks on the
//     * square in the specified row and col. If this BC is a valid selection 
//     * get the valid moves that the selected character could travel
//     */
//	private void selectBoardComponent(int row, int col) {
//		//TODO doIneedThisStatement Debug.Log("doClickSquare");
//		BoardComponent bc = board.pieceAt(row, col);
//		if(bc.isOccupied()){
//			if(validMoves.isEmpty()){
//				bc.Selected = true;
//				//TODO doIneedThisStatement Debug.Log("you clicked BoardComponent with ID of -- " + bc.getId());
//				validMoves = bc.Character.getPossibleMoves(board, bc);
//			}
//		}
//		
//		board.getCoordinateOfId(bc.getId());
//		board.getBoardComponentAtId(bc.getId());
//		repaint();
//	}  //


	/**
     * if the param bc coordinate member is equal to a coordinate in the valid moves array return true, else false
     * @param bc
     * @return
     */
	private bool validSelection(BoardComponent bc) {
		foreach (BoardComponent vbc in validMoves) {
			if (vbc.Coordinate == bc.Coordinate) {
				return true;
			}
		}
		return false;
	}

	private void erasePossibleMoves () {
		//destroy selected tile object
		erasePossibleMoves(false);
	}

	private void erasePossibleMoves (bool preserveSelectedTile) {
		for (int i = 0; i < this.validMoves.Count; i++) {
			Tile tile = getTileForBoardComponent( this.validMoves[i] );
			tile.disableHighlighter();
		}
		if (this.selectedTile != null && !preserveSelectedTile) {
			this.selectedTile.disableHighlighter ();
			this.selectedTile = null;
		}
		this.validMoves.Clear();
	}

	private Tile getTileForBoardComponent(BoardComponent bc) {
		for (int i = 0; i < tilesCount; i++) {
			Tile tile = tiles[i];
			if(tile.boardComponent == bc) {
				return tile;
			}
		}
		return null;
	}

	private bool isOpposingPlayers (BoardComponent bc1, BoardComponent bc2) {
		if ( (bc1.CharacterPlayer == Player.PLAYER_ONE && bc2.CharacterPlayer == Player.PLAYER_TWO)
		    || (bc1.CharacterPlayer == Player.PLAYER_TWO && bc2.CharacterPlayer == Player.PLAYER_ONE) ) {
			return true;
		}
		return false;
	}

	private void highlightPossibleUnstackMovesForSelectedComponent () {
		for (int i = 0; i < this.validMoves.Count; i++) {
			Tile tile = getTileForBoardComponent( this.validMoves[i] );
			tile.highlightMove();
		}
	}

	private void highlightPossibleMovesForSelectedComponent () {
		for (int i = 0; i < this.validMoves.Count; i++) {
			Tile tile = getTileForBoardComponent( this.validMoves[i] );
			if(!tile.boardComponent.Occupied && tile.boardComponent.Location != Location.OUT_OF_BOUNDS) {
				tile.highlightMove ();
//				tile.GetComponent<Renderer> ().material.
			} else if (tile.boardComponent.CharacterPlayer == selectedTile.boardComponent.CharacterPlayer) { 
				tile.highlightStack ();
			} else if (isOpposingPlayers(tile.boardComponent, selectedTile.boardComponent)) {
				tile.highlightAttack ();
			}
		}
	}
}