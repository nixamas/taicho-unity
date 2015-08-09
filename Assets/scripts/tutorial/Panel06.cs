using System;
using UnityEngine;
using UnityEngine.UI;
using com.cosmichorizons.basecomponents;
using com.cosmichorizons.enums;
using com.cosmichorizons.characters;


public class Panel06: AbstractPanel
{
//	int [] idsOfBarrierBoardComponentsForTutorial = new int [] {47,48,49,50,51,56,60,65,69,74,78,83,84,85,86,87};

	public override String getHeaderText () {
		return "Stacking and Unstacking";
	}

	public override String getTutorialText () {
		return "In order to create higher level characters you must stack your lower level characters. " +
			"A Second Lieutenant and apprentice samurais stacked become a First Lieutenant samurai, etc...";
	}

	public override void processGameGrid (TaichoGameGrid grid) {
		grid.erasePossibleMoves ();
		grid.disableTileSelection = false;
		grid.initialize ();
		grid.disableCharacterSprites ();



		populateTileWithCharacter (grid, new BoardComponent (new OneUnit (Player.PLAYER_ONE), Location.GAME_BOARD, new Coordinate (6, 3, 57)), false);
		populateTileWithCharacter (grid, new BoardComponent (new OneUnit (Player.PLAYER_ONE), Location.GAME_BOARD, new Coordinate (8, 5, 77)), false);
		populateTileWithCharacter (grid, new BoardComponent (new OneUnit (Player.PLAYER_ONE), Location.GAME_BOARD, new Coordinate (7, 4, 67)));//draw last because this method erases valid moves in grid

		foreach (Tile tile in grid.tiles) {
			if (!isTutorialSandbox(tile.boardComponent.Id)) {
				tile.hide ();
				tile.boardComponent.removeCharacter ();
				tile.boardComponent.Location = Location.OUT_OF_BOUNDS;
			}
		}
	}

	private bool isTutorialSandbox (int testId) {
		if ((testId > 47 && testId < 51) ||
			(testId > 56 && testId < 60) ||
			(testId > 65 && testId < 69) ||
			(testId > 74 && testId < 78) ||
			(testId > 83 && testId < 87)) {
			return true;
		} else {
			return false;
		}
	}

}

