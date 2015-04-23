//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18063
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using com.cosmichorizons.enums;
using com.cosmichorizons.basecomponents;
using com.cosmichorizons.interfaces;
public class LevelThreeLegalMovesClass: LegalMoves
{
	public override MoveClass[] getMoves () {
		MoveClass[] moves = new MoveClass[12];
		moves [0] = new MoveClass (30);
		moves [1] = new MoveClass (24);
		moves [2] = new MoveClass (16);
		moves [3] = new MoveClass (20);
		moves [4] = new MoveClass (10);
		moves [5] = new MoveClass (8);
		moves [6] = new MoveClass (-30);
		moves [7] = new MoveClass (-24);
		moves [8] = new MoveClass (-16);
		moves [9] = new MoveClass (-20);
		moves [10] = new MoveClass (-10);
		moves [11] = new MoveClass (-8);
		return moves;
	}

	public override int getMove (int i) {
		return getMoves ()[i].getValue ();
	}

	public override int getBufferValue ()
	{
		return 3;
	}

	/**
	 * +8,+16,+24 --> M6,M3,M2
	 * -10,-20,-30  --> M11,M10,M7
	 * -8,-16,-24 --> M12,M9,M8
	 * +10,+20,+30  --> M5,M4,M1
	 */
	public override List<List<MoveClass>> getBlockablePathsOfMoves () {
		List<List<MoveClass>> moves = new List<List<MoveClass>>();
		MoveClass[] myMoves = getMoves ();

		for(int i = 0; i < 4; i++){
			moves.Add(new List<MoveClass>());
			switch(i){
			case 0:
				moves[i].Add(myMoves[5]);
				moves[i].Add(myMoves[2]);
				moves[i].Add(myMoves[1]);
				break;
			case 1:
				moves[i].Add(myMoves[10]);
				moves[i].Add(myMoves[9]);
				moves[i].Add(myMoves[6]);
				break;
			case 2:
				moves[i].Add(myMoves[11]);
				moves[i].Add(myMoves[8]);
				moves[i].Add(myMoves[7]);
				break;
			case 3:
				moves[i].Add(myMoves[4]);
				moves[i].Add(myMoves[3]);
				moves[i].Add(myMoves[0]);
				break;
			default:
				break;
			}
		}
		return moves;
	}
}

