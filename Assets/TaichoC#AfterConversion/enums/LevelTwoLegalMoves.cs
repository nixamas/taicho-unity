namespace com.cosmichorizons.enums
{


	using MoveManager = com.cosmichorizons.interfaces.MoveManager;

	/// <summary>
	/// Legal move values for lvl2 ranked characters
	/// @author Ryan
	/// 
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Enums cannot implement interfaces in .NET:
//ORIGINAL LINE: public enum LevelTwoLegalMoves implements MoveManager
	public enum LevelTwoLegalMoves
	{
		MOVE_ONE = 2,
		MOVE_TWO = 18,
		MOVE_THREE = 1,
		MOVE_FOUR = 9,
		MOVE_FIVE = -2,
		MOVE_SIX = -18,
		MOVE_SEVEN = -1,
		MOVE_EIGHT = -9

//		private int numVal;
//
//		LevelTwoLegalMoves(int numVal)
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
//			LevelTwoLegalMoves[] array = values();
//			return = 
//		}
//
//		public static int getBufferValue()
//		{
//			return 2;
//		}
//
//		/// <summary>
//		/// +9,+18 --> M4,M2
//		/// -1,-2  --> M7,M5
//		/// -9,-18 --> M8,M6
//		/// +1,+2  --> M3,M1
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
//						moves = MOVE_FOUR
//						moves = MOVE_TWO
//						break
//					case 1:
//						moves = MOVE_SEVEN
//						moves = MOVE_FIVE
//						break
//					case 2:
//						moves = MOVE_EIGHT
//						moves = MOVE_SIX
//						break
//					case 3:
//						moves = MOVE_THREE
//						moves = MOVE_ONE
//						break
//					default:
//						break
//				}
//			}
//			return moves;
//		}
	}


	public static class LevelTwoLegalMovesExtensions {
		public static int getBufferValue() {
			return 2;
		}
	}

}