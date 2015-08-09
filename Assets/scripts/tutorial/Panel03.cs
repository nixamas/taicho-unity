using System;
using UnityEngine;
using UnityEngine.UI;
using com.cosmichorizons.basecomponents;
using com.cosmichorizons.enums;
using com.cosmichorizons.characters;


public class Panel03: AbstractPanel
{

	public override String getHeaderText () {
		return "Apprentice Samurai";
	}

	public override String getTutorialText () {
		return "Apprentice samurai are able to enter any unoccupied square in a one block radius.";
	}

	public override void processGameGrid (TaichoGameGrid grid) {
		grid.erasePossibleMoves ();
		grid.disableTileSelection = true;
		grid.initialize ();
		grid.disableCharacterSprites ();
		
		populateTileWithCharacter (grid, new BoardComponent (new OneUnit (Player.PLAYER_ONE), Location.GAME_BOARD, new Coordinate (7, 4, 67)));
	}

}

