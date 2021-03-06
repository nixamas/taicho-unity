﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.cosmichorizons.basecomponents;
using com.cosmichorizons.characters;
using com.cosmichorizons.enums;
using com.cosmichorizons.exceptions;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class TaichoGameGrid : MonoBehaviour {
	private int rows = 9;
	private int columns = 15;
	private float distanceBetweenTiles = 2.1F;
	private static int tilesCount = 135;
	public bool autostartGame = true;
	public Tile tilePrefab;	//defined in GUI
	public HighlightTileSprite highlightTileSpritePrefab;
	public CharacterSprite characterSpritePrefab;
	public AudioManager audioManager;
	public Tile[] tiles = new Tile[tilesCount];
	public bool disableTileSelection = false;

	private bool unstackObjects, showIcons; // Is a game currently in progress? //probably need to move these elsewhere
	public Tile selectedTile; //public so the tutorial can manually update game
	public Tile firstTileSelected, secondTileSelected;
	public TaichoGameData taicho = new TaichoGameData ();
	public List<BoardComponent> validMoves = new List<BoardComponent> ();


	// Photon Network Unity Turnbased objects
	protected internal Dictionary<int, GameObject> TileGameObjects;
//	protected internal byte[] TileValues = new byte[tilesCount];



	//List<BoardComponent> validMoves; // An array containing the legal moves for the
	// current player.

	// Use this for initialization
	void Start () {
		bool autoPopulateTiles = true;
		if (NetworkManager.instance.rejoiningTaichoGame) {
			Debug.Log ("TaichoGameGrid.Start() rejoining taicho game");
			autoPopulateTiles = false;
		}
		if (autostartGame) {
			initialize (autoPopulateTiles);
		}
	}

	public void initialize() { 
		initialize (true);
	}

	public void initialize(bool autoPopulateTiles) { 
		taicho.initialize ();
		createTiles (autoPopulateTiles);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void createTiles(bool autoPopulateTiles) {
		this.TileGameObjects = new Dictionary<int, GameObject>(tilesCount);
		float xOffset = 0.0F;
		float zOffset = 0.0F;
		tiles = new Tile[tilesCount];
		int index = 0;
		for (var col = 0; col < columns; col++) {
			xOffset += distanceBetweenTiles * col;
			//draw columns
			for (var row = 0; row < rows; row++) {
				// draw rows
				zOffset += distanceBetweenTiles * row;
				Tile tile = (Tile)Instantiate(tilePrefab, new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z + zOffset), transform.rotation);

				//Load a random stone image for tile texture TODO do something else
//				tile.GetComponent<Renderer> ().material.mainTexture = (Texture)UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Resources/Images/stone" + Random.Range(1,6) + ".png", typeof(Texture));
				tile.GetComponent<Renderer> ().material.color = TaichoColors.TAICHO_BOARD_TILE_TINT;

				HighlightTileSprite highlight = (HighlightTileSprite)Instantiate(highlightTileSpritePrefab, new Vector3(transform.position.x + xOffset, transform.position.y+2, transform.position.z + zOffset), highlightTileSpritePrefab.transform.rotation);
				tile.initializeHighlighter(highlight);

				CharacterSprite sprite = (CharacterSprite)Instantiate(characterSpritePrefab, new Vector3(transform.position.x + xOffset, transform.position.y+2, transform.position.z + zOffset), highlightTileSpritePrefab.transform.rotation);
				tile.initializeSprite(sprite);


				if ( (col < 3 && (row < 3 || row > 5) || (col > 11 && (row < 3 || row > 5)))){
					//Debug.Log("skipping displaying tile at coordinate column[" + col + "] - row[" + row + "]");
					taicho.board[row, col] = new BoardComponent(new TaichoUnit(Player.NONE), Location.OUT_OF_BOUNDS, new Coordinate(col, row, index));
					tile.hide ();
				} else if (row == 4 && col == 1) {
					// position is Player One Taicho
					taicho.board[row, col] = new BoardComponent(new TaichoUnit(Player.PLAYER_ONE), Location.PLAYER_ONE_CASTLE, new Coordinate(col, row, index));
					tile.GetComponent<Renderer> ().material.color = TaichoColors.TAICHO_BOARD_CASTLE_TINT;
//					tile.GetComponent<Renderer> ().material.mainTexture = (Texture)UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Resources/Images/castle1stone" + Random.Range(1,4) + ".png", typeof(Texture));
				} else if (row == 4 && col == 13) {
					// position is Player Two Taicho
					tile.GetComponent<Renderer> ().material.color = TaichoColors.TAICHO_BOARD_CASTLE_TINT;
					taicho.board[row, col] = new BoardComponent(new TaichoUnit(Player.PLAYER_TWO), Location.PLAYER_TWO_CASTLE, new Coordinate(col, row, index));
//					tile.GetComponent<Renderer> ().material.mainTexture = (Texture)UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Resources/Images/castle2stone" + Random.Range(1,4) + ".png", typeof(Texture));
				} else if (((col == 4) && (row % 2 == 0))
				           || ((col == 3 || col == 5) && (row % 2 == 1))) {
					// position is Player One Samurai
					taicho.board[row, col] = new BoardComponent(new OneUnit(Player.PLAYER_ONE), Location.GAME_BOARD, new Coordinate(col, row, index));
				} else if (((col == 10) && (row % 2 == 0))
				           || ((col == 9 || col == 11) && (row % 2 == 1))) {
					// position is Player Two Samurai
					taicho.board[row, col] = new BoardComponent(new OneUnit(Player.PLAYER_TWO), Location.GAME_BOARD, new Coordinate(col, row, index));
				} else if((col <= 2) && (row >= 3 && row <=5)){
					// position is Player One Castle
					taicho.board[row, col] = new BoardComponent(Location.PLAYER_ONE_CASTLE, new Coordinate(col, row, index));
					tile.GetComponent<Renderer> ().material.color = TaichoColors.TAICHO_BOARD_CASTLE_TINT;
//					tile.GetComponent<Renderer> ().material.mainTexture = (Texture)UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Resources/Images/castle1stone" + Random.Range(1,4) + ".png", typeof(Texture));
				} else if((col >= 12) && (row >= 3 && row <=5)){
					// position is Player Two Castle
					taicho.board[row, col] = new BoardComponent(Location.PLAYER_TWO_CASTLE, new Coordinate(col, row, index));
					tile.GetComponent<Renderer> ().material.color = TaichoColors.TAICHO_BOARD_CASTLE_TINT;
//					tile.GetComponent<Renderer> ().material.mainTexture = (Texture)UnityEditor.AssetDatabase.LoadAssetAtPath ("Assets/Resources/Images/castle2stone" + Random.Range(1,4) + ".png", typeof(Texture));
				} else {
					taicho.board[row, col] = new BoardComponent(Location.GAME_BOARD, new Coordinate(col, row, index));
				}
				if( ( col == 3 || col == 11 ) && ( row >= 3  && row <= 5) ){
					//Board component is a barrier
					taicho.getBoardComponentAtId(index).Barrier = true;
				}

				//easy to have a reference to bc from gui object
				tile.boardComponent = taicho.getBoardComponentAtId(index);
				if (!autoPopulateTiles) {
					tile.boardComponent.removeCharacter ();
				}
				this.TileGameObjects.Add(index, tile.gameObject);
				tile.updateSprite ();
				tiles[index] = tile;
				index++;
				zOffset = 0;

			}
			xOffset = 0;
		}

		validMoves = new List<BoardComponent> ();
		Debug.Log ("Game Board object have been created");
	}

	public int GetCubeTileIndex(GameObject cube)
	{
		foreach (KeyValuePair<int, GameObject> pair in TileGameObjects)
		{
			if (pair.Value.Equals(cube))
			{
				return pair.Key;
			}
		}
		
		Debug.LogError("Could not find Cube in Dict.");
		return -1;
	}
	
	protected internal Hashtable GetBoardAsCustomProperties()
	{
		Hashtable customProps = new Hashtable();
		for (int i = 0; i < tilesCount; i++)
		{
			BoardComponent bc = TileGameObjects[i].GetComponent<Tile> ().boardComponent;
			if (bc.Occupied) {
				customProps[i.ToString()] = bc;
			}
		}
		return customProps;
	}

	public Hashtable GetTurnCustomProperties () {
		Hashtable customProps = new Hashtable ();
		if (firstTileSelected == null || secondTileSelected == null) {
			return customProps;
		}
		customProps["firstTileSelected"] = firstTileSelected.GetComponent<Tile> ().boardComponent;
		customProps["secondTileSelected"] = secondTileSelected.GetComponent<Tile> ().boardComponent;
		return customProps;
	}

	public void performTurnAction(BoardComponent sourceBc, BoardComponent destBc) {
		Debug.Log("Performing Turn Action SourceBc["+sourceBc+"]   DestBC["+destBc+"]");
	}

	private void updateTileSprites () {
		for (int i = 0; i < tilesCount; i++) {
			tiles[i].GetComponent<Tile> ().updateSprite ();
		}
	}

	protected internal bool SetBoardByCustomProperties(Hashtable customProps)
	{
		Debug.Log ("Set board by saved custom properties " + customProps);
		int readTiles = 0;
		for (int i = 0; i < tilesCount; i++) {
			if (customProps.ContainsKey(i.ToString())) {
				BoardComponent bc = (BoardComponent) customProps[i.ToString()];
//				Debug.Log("BoardComponent recieved -- " +bc);
				tiles[bc.Id].GetComponent<Tile> ().boardComponent = bc;
				tiles[bc.Id].GetComponent<Tile> ().updateSprite ();
				readTiles++;
			} 
//			else {
//				tiles[i].GetComponent<Tile> ().boardComponent.removeCharacter ();
//			}
		}
		if (readTiles == 0) {
			Debug.Log ("No tiles loaded from custom properties");
		}
//		updateTileSprites ();

		if (customProps["firstTileSelected"] != null) {
//			Debug.Log("custom prop contains select Tile --  [" + customProps["firstTileSelected"] + "]");
			BoardComponent bc = (BoardComponent) customProps["firstTileSelected"];
			Debug.Log("firstTileSelected BoardComponent recieved -- " +bc);
			tiles[bc.Id].GetComponent<Tile> ().boardComponent = bc;
			tiles[bc.Id].GetComponent<Tile> ().updateSprite ();
		} else {
			Debug.Log ("No 'firstTileSelected' object found");
		}

		if (customProps["secondTileSelected"] != null) {
//			Debug.Log("custom prop contains select Tile --  [" + customProps["secondTileSelected"] + "]");
			BoardComponent bc = (BoardComponent) customProps["secondTileSelected"];
			Debug.Log("secondTileSelected BoardComponent recieved -- " +bc);
			tiles[bc.Id].GetComponent<Tile> ().boardComponent = bc;
			tiles[bc.Id].GetComponent<Tile> ().updateSprite ();
		} else {
			Debug.Log ("No 'secondTileSelected' object found");
		}
		return true; //readTiles == tilesCount;
	}

	public bool shouldUnstackButtonBeEnabled () {
		if (selectedTile != null 
		    && selectedTile.boardComponent != null 
		    && selectedTile.boardComponent.Occupied 
			&& (selectedTile.boardComponent.Character.Rank == Ranks.LEVEL_TWO || selectedTile.boardComponent.Character.Rank == Ranks.LEVEL_THREE)) {
			return true;
		}
		return false;
	}


	public void onUnstackButtonClicked() {
		if (selectedTile != null) {
			erasePossibleMoves(true); //erase shown moves but preserve the currently selected tile
			validMoves = selectedTile.boardComponent.Character.getPossibleUnstackLocations (taicho, selectedTile.boardComponent);
			highlightPossibleUnstackMovesForSelectedComponent ();
			unstackObjects = true;
		}
	}

	public void setTurnTiles(Tile firstTile, Tile secondTile) {
		Debug.Log("Setting Turn Tiles 1ST["+firstTile.boardComponent+"]  ---  2ND["+secondTile.boardComponent+"]");
		firstTileSelected = firstTile;
		secondTileSelected = secondTile;
	}

	//They tile clicked will call this method
	public void onTileClicked(Tile tile) {
		if (this.disableTileSelection) {
			return;
		}
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
					audioManager.playTileSelectedSound ();
				}else if( !(validMoves.Count == 0) && validSelection(bc)){
					// if there is a selected BC and user chose a valid BC
					//TODO doIneedThisStatement Debug.Log("make move to new VALID square");
					try{
						BoardComponent selectedBc = selectedTile.boardComponent;
						
						if( bc.Occupied && bc.Character.Player == selectedBc.Character.Player ){
							//both square are occupied by the same player
							setTurnTiles(selectedTile, tile);
							stackUnits(tile);
						}else if( isOpposingPlayers(tile.boardComponent, selectedTile.boardComponent) ){
							setTurnTiles(selectedTile, tile);
							//both squares are occupied by opposite players
							attackObject(tile);
						}else if( !bc.Occupied ){
							//TODO need additional information to signify unstack over network
							setTurnTiles(selectedTile, tile);
							if(unstackObjects){
								unstackUnits(tile);
								unstackObjects = false;
							}else{
								makeMove(tile);
							}
						}
					}catch(BoardComponentNotFoundException bcnfe){
						Debug.LogError(bcnfe.Message);
					}
				}else if (selectedTile != null) {

					//TODO doIneedThisStatement Debug.Log("checking if user clicked selected BC again. If so, Abort");
					try{
						BoardComponent selectedBc = selectedTile.boardComponent;
						if(bc.Coordinate == selectedBc.Coordinate ){
							audioManager.playTileSelectedSound ();
							//user clicked same BC twice, abort the BC selection
							//if there is a selected BC then and the user clicked it a second time, 
							//Then clear the valid moves array
							selectedBc.Selected = false;
							erasePossibleMoves();
						}

					}catch(BoardComponentNotFoundException bcnfe){
						Debug.LogError(bcnfe.Message);
					}
				}
			} else if (selectedTile != null && tile.boardComponent == selectedTile.boardComponent) {	
				//TODO fix this so that when a use clicks out of bounds the selected tile is unselected,
				//condition above was added because interference with UI buttons
				BoardComponent selectedBc = selectedTile.boardComponent;
				selectedBc.Selected = false;
				erasePossibleMoves();
			}
		}catch(BoardComponentNotFoundException bcnfe){
			Debug.LogError(bcnfe.Message);
		}catch(UnityException e){
			Debug.LogError(e);
		}

		tile.updateSprite ();
		if (selectedTile != null) {
			selectedTile.updateSprite();
		}
	}

	private void makeMove (Tile destinationTile) {
		this.audioManager.playTileSlideSound ();
		Player player = selectedTile.boardComponent.CharacterPlayer;
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

							selectedTile.updateSprite();
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
		selectedTile.updateSprite ();
		destinationTile.updateSprite ();
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
						//addTurn(currentPlayer, attackingBc.getCoordinate(), victimBc.getCoordinate(), ObjectMove.MOVE_TYPE.ATTACK, victimBc.removeCharacter() );
						victimBc.Character = attackingBc.removeCharacter(); 
						selectedTile.updateSprite();
						victimsTile.updateSprite();
					}else if(oppressingCharacter.CombatValue >= victimCharacter.CombatValue && victimCharacter.Rank == Ranks.TAICHO){
						// A taicho character has been killed, game is over
						Debug.Log("Game is over, taicho is dead");
						victimBc.Character = attackingBc.removeCharacter();
						erasePossibleMoves();
						this.taicho.gameWinner = oppressingCharacter.Player;
						this.taicho.gameInPlay = false;

						//TODO dont immediatly restart game, show menu
						Application.LoadLevel ("GameBoardScene");
					}else{
						//attacking character can beat victim using teammates
						Debug.Log("Multiple samurais are about to kill you...");
						//addTurn(currentPlayer, attackingBc.getCoordinate(), victimBc.getCoordinate(), ObjectMove.MOVE_TYPE.ATTACK, victimBc.removeCharacter() );
						victimBc.Character = attackingBc.removeCharacter();
						selectedTile.updateSprite();
						victimsTile.updateSprite();
					}
					this.audioManager.playCharacterDestroyedSound();
					attackingBc.Selected = false;
					erasePossibleMoves();
					return true;
				}
			}else{
				//players were not right
				//Debug.Log("You cant attack your own team!");
				return false;
			}
		}else{
			//one or both BC's are not occupied
			//Debug.Log("You cant attack empty squares");
			return false;
		}
		return false;
	}



	/**
     * if the param bc coordinate member is equal to a coordinate in the valid moves array return true, else false
     * @param bc
     * @return
     */
	private bool validSelection(BoardComponent bc) {
		foreach (BoardComponent vbc in validMoves) {
			if (vbc.Coordinate.Id == bc.Coordinate.Id) {
				return true;
			}
		}
		return false;
	}

	public void erasePossibleMoves () {
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
			this.selectedTile.updateSprite();
			this.selectedTile = null;
		}
		this.validMoves.Clear();
	}

	public Tile getTileForBoardComponent(BoardComponent bc) {
		for (int i = 0; i < tilesCount; i++) {
			Tile tile = tiles[i];
			if(tile.boardComponent.Id == bc.Id) {
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

	public void highlightPossibleUnstackMovesForSelectedComponent () {
		for (int i = 0; i < this.validMoves.Count; i++) {
			Tile tile = getTileForBoardComponent( this.validMoves[i] );
			tile.highlightMove();
		}
	}

	public void highlightPossibleMovesForSelectedComponent () {
		for (int i = 0; i < this.validMoves.Count; i++) {
			Tile tile = getTileForBoardComponent( this.validMoves[i] );
			if(!tile.boardComponent.Occupied && tile.boardComponent.Location != Location.OUT_OF_BOUNDS) {
				tile.highlightMove ();
			} else if (tile.boardComponent.Character.Player == selectedTile.boardComponent.Character.Player) { 
				tile.highlightStack ();
			} else if (isOpposingPlayers(tile.boardComponent, selectedTile.boardComponent)) {
				tile.highlightAttack ();
			}
		}
	}

	public void destroyTiles () {
		Debug.Log ("Destroying Game grid tiles");
		erasePossibleMoves ();
		for (int i = 0; i < tilesCount; i++) {
			GameObject.Destroy(tiles[i]);
			tiles[i] = null;
		}
		tiles = new Tile[tilesCount];
	}

	public void disableCharacterSprites () {
		foreach (Tile tile in tiles) {
			tile.disableSprite ();
		}
	}

}