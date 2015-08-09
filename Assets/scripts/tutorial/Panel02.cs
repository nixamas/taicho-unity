using System;
using UnityEngine;
using UnityEngine.UI;
using com.cosmichorizons.basecomponents;
using com.cosmichorizons.enums;
using com.cosmichorizons.characters;

public class Panel02: AbstractPanel
{

	public override String getHeaderText () {
		return "Goal of Taicho";
	}

	public override String getTutorialText () {
		return "The objective of the game is to eliminate your opponents taicho. " +
			"Each character has different move patterns based on their rank.";
	}

	public override void processGameGrid (TaichoGameGrid grid) {
		grid.erasePossibleMoves ();
		grid.disableTileSelection = true;
		grid.initialize ();
		grid.disableCharacterSprites ();
	}

}

