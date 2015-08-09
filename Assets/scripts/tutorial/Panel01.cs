using System;
using UnityEngine;
using UnityEngine.UI;
using com.cosmichorizons.basecomponents;
using com.cosmichorizons.enums;
using com.cosmichorizons.characters;

public class Panel01: AbstractPanel
{

	public override String getHeaderText () {
		return "Taicho";
	}

	
	public override String getTutorialText () {
		return "Taicho is a two-player strategy board game with the objective " +
			"of defeating your opponent in glorious battle. Each player start with" +
			"15 apprentice samurai and a single taicho housed in your respective castle. ";
	}

	public override void processGameGrid (TaichoGameGrid grid) {
		grid.erasePossibleMoves ();
		grid.disableTileSelection = true;
		grid.initialize ();
		grid.disableCharacterSprites ();

		//populateTileWithCharacter (grid, new BoardComponent (new OneUnit (Player.PLAYER_ONE), Location.GAME_BOARD, new Coordinate (7, 4, 67)));
	}
}

