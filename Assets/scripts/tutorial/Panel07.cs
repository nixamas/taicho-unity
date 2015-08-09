using System;
using UnityEngine;
using UnityEngine.UI;
using com.cosmichorizons.basecomponents;
using com.cosmichorizons.enums;
using com.cosmichorizons.characters;


public class Panel07: AbstractPanel
{

	public override String getHeaderText () {
		return "Taicho";
	}

	public override String getTutorialText () {
		return "Taicho is the captain of your squad, and as such he is the most important character and " +
			"must be protected. Once your Taicho is eliminated, you lose the battle. " +
			"Taicho is able to move to any unoccupied tile in their respective castle. It's attack strength is equal " +
			"to an Apprentice samurai (TODO Is this right???) so remember to protect it!";
	}

	public override void processGameGrid (TaichoGameGrid grid) {
		grid.erasePossibleMoves ();
		grid.disableTileSelection = true;
		grid.initialize ();
		grid.disableCharacterSprites ();
		
		//populateTileWithCharacter (grid, new BoardComponent (new ThreeUnit (Player.PLAYER_ONE), Location.GAME_BOARD, new Coordinate (7, 4, 67)));
	}

}

