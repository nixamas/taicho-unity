namespace com.cosmichorizons.enums
{

	using MoveManager = com.cosmichorizons.interfaces.MoveManager;


	/// <summary>
	/// Legal move values for lvl1 ranked characters
	/// @author Ryan
	/// 
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Enums cannot implement interfaces in .NET:
//ORIGINAL LINE: public enum LevelOneLegalMoves implements MoveManager
	public enum LevelOneLegalMoves
	{
		MOVE_ONE = 1,
		MOVE_TWO = 10,
		MOVE_THREE = 8,
		MOVE_FOUR = 9,
		MOVE_FIVE = -1,
		MOVE_SIX = -10,
		MOVE_SEVEN = -8,
		MOVE_EIGHT = -9,

//		private int numVal;
//
//		LevelOneLegalMoves(int numVal)
//		{
//			this.numVal = numVal
//		}
//
//		public int getNumVal()
//		{
//			return numVal;
//		}
//
//
//		public Object[] getMoves()
//		{
//			return = 
//		}
//
//		public int getMove(int i)
//		{
//			LevelOneLegalMoves[] array = values();
//			return = 
//		}
//
//		public static int getBufferValue()
//		{
//			return 1;
//		}
//
//		public static java.util.ArrayList<java.util.ArrayList<com.cosmichorizons.interfaces.MoveManager>> getBlockablePathsOfMoves()
//		{
//			java.util.ArrayList<java.util.ArrayList<com.cosmichorizons.interfaces.MoveManager>> moves = new java.util.ArrayList<java.util.ArrayList<com.cosmichorizons.interfaces.MoveManager>>();
////JAVA TO C# CONVERTER TODO TASK: The following line could not be converted:
//			for (int i = 0; i < 8; i++)
//			{
//				//order doesnt matter here because LevelOne can only move
//					//one block in each direction
//				moves = new java.util.ArrayList<com.cosmichorizons.interfaces.MoveManager>()
//				moves = values()[i]
//			}
//			return moves;
//		}
	}
}