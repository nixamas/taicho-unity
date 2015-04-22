using System;
using System.Collections.Generic;
using com.cosmichorizons.enums;
using com.cosmichorizons.basecomponents;
using com.cosmichorizons.interfaces;
public abstract class LegalMoves
{
	public LegalMoves ()
	{
	}

	public abstract MoveClass[] getMoves ();

	public abstract int getMove (int i);

	public abstract int getBufferValue ();

	public abstract List<List<MoveClass>> getBlockablePathsOfMoves ();
}

