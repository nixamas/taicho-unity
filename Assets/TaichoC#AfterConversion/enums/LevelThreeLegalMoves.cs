namespace com.cosmichorizons.enums
{

	using MoveManager = com.cosmichorizons.interfaces.MoveManager;

	/// <summary>
	/// Legal move values for lvl3 ranked characters
	/// @author Ryan
	/// 
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Enums cannot implement interfaces in .NET:
//ORIGINAL LINE: public enum LevelThreeLegalMoves implements MoveManager
	public enum LevelThreeLegalMoves
	{
		MOVE_ONE = 30,
		MOVE_TWO = 24,
		MOVE_THREE = 16,
		MOVE_FOUR = 20,
		MOVE_FIVE = 10,
		MOVE_SIX = 8,
		MOVE_SEVEN = -30,
		MOVE_EIGHT = -24,
		MOVE_NINE = -16,
		MOVE_TEN = -20,
		MOVE_ELEVEN = -10,
		MOVE_TWELVE = -8

//		private int numVal;
//
//		LevelThreeLegalMoves(int numVal)
//		{
//			this.numVal = numVal
//		}
//
//		public int getNumVal()
//		{
//			return numVal;
//		}
//
//		public Object[] getMoves()
//		{
//			return = 
//		}
//
//		public int getMove(int i)
//		{
//			LevelThreeLegalMoves[] array = values();
//			return = 
//		}
//
//		public static int getBufferValue()
//		{
//			return 3;
//		}
//
//		/// <summary>
//		/// +8,+16,+24 --> M6,M3,M2
//		/// -10,-20,-30  --> M11,M10,M7
//		/// -8,-16,-24 --> M12,M9,M8
//		/// +10,+20,+30  --> M5,M4,M1
//		/// </summary>
//		public static java.util.ArrayList<java.util.ArrayList<com.cosmichorizons.interfaces.MoveManager>> getBlockablePathsOfMoves()
//		{
//			java.util.ArrayList<java.util.ArrayList<com.cosmichorizons.interfaces.MoveManager>> moves = new java.util.ArrayList<java.util.ArrayList<com.cosmichorizons.interfaces.MoveManager>>();
////JAVA TO C# CONVERTER TODO TASK: The following line could not be converted:
//			for (int i = 0; i < 4; i++)
//			{
//				moves = new java.util.ArrayList<com.cosmichorizons.interfaces.MoveManager>()
////JAVA TO C# CONVERTER TODO TASK: The following line could not be converted:
//				switch (i)
//				{
//					case 0:
//						moves = MOVE_SIX
//						moves = MOVE_THREE
//						moves = MOVE_TWO
//						break
//					case 1:
//						moves = MOVE_ELEVEN
//						moves = MOVE_TEN
//						moves = MOVE_SEVEN
//						break
//					case 2:
//						moves = MOVE_TWELVE
//						moves = MOVE_NINE
//						moves = MOVE_EIGHT
//						break
//					case 3:
//						moves = MOVE_FIVE
//						moves = MOVE_FOUR
//						moves = MOVE_ONE
//						break
//					default:
//						break
//				}
//			}
//			return moves;
//		}
	}

//	public static class LevelThreeLegalMovesExtensions {
//		public static int getBufferValue(this LevelOneLegalMoves lolm) {
//			return 3;
//		}
//	}

}