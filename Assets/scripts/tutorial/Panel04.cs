using System;
using UnityEngine;
using UnityEngine.UI;
using com.cosmichorizons.basecomponents;
using com.cosmichorizons.enums;
using com.cosmichorizons.characters;


public class Panel04: AbstractPanel
{

	public override String getHeaderText () {
		return "Second Lieutenant Samurai";
	}

	public override String getTutorialText () {
		return "characters are able to move in a 2 square radius perpendicular to the character.";
	}

	public override void processGameGrid (TaichoGameGrid grid) {
		grid.erasePossibleMoves ();
		grid.disableTileSelection = true;
		grid.initialize ();
		grid.disableCharacterSprites ();

		populateTileWithCharacter (grid, new BoardComponent (new TwoUnit (Player.PLAYER_ONE), Location.GAME_BOARD, new Coordinate (7, 4, 67)));
	}

}

