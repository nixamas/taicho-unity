using System;
using UnityEngine;
using UnityEngine.UI;
using com.cosmichorizons.basecomponents;
using com.cosmichorizons.enums;
using com.cosmichorizons.characters;


public class Panel05: AbstractPanel
{

	public override String getHeaderText () {
		return "First Lieutenant Samurai";
	}

	public override String getTutorialText () {
		return "First Lieutenant Samurai characters are able to move in a 3 square radius diagonally to the character.";
	}

	public override void processGameGrid (TaichoGameGrid grid) {
		grid.erasePossibleMoves ();
		grid.disableTileSelection = true;
		grid.initialize ();
		grid.disableCharacterSprites ();
		
		populateTileWithCharacter (grid, new BoardComponent (new ThreeUnit (Player.PLAYER_ONE), Location.GAME_BOARD, new Coordinate (7, 4, 67)));
	}

}

