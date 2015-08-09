using System;
using UnityEngine;
using UnityEngine.UI;
using com.cosmichorizons.basecomponents;

public interface TutorialPanelInterface
{
	String getHeaderText ();
	String getTutorialText ();

	BoardComponent[] getBoardComponents ();

	void processGameGrid (TaichoGameGrid grid);
}

