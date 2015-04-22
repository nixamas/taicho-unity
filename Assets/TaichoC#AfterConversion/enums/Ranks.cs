using System.Collections.Generic;

namespace com.cosmichorizons.enums
{

	/// <summary>
	/// Enum for all possible character ranks in the game
	/// LEVEL_ONE, LEVEL_TWO, LEVEL_THREE, TAICHO, NONE
	/// @author Ryan
	/// 
	/// </summary>
	public enum Ranks
	{
		LEVEL_ONE,
		LEVEL_TWO,
		LEVEL_THREE,
		TAICHO,
		NONE
//			public static readonly Ranks LEVEL_ONE = new Ranks("LEVEL_ONE", InnerEnum.LEVEL_ONE, 1,"LEVEL_ONE");
//			public static readonly Ranks LEVEL_TWO = new Ranks("LEVEL_TWO", InnerEnum.LEVEL_TWO, 2,"LEVEL_TWO");
//			public static readonly Ranks LEVEL_THREE = new Ranks("LEVEL_THREE", InnerEnum.LEVEL_THREE, 3,"LEVEL_THREE");
//			public static readonly Ranks TAICHO = new Ranks("TAICHO", InnerEnum.TAICHO, 4,"TAICHO");
//			public static readonly Ranks NONE = new Ranks("NONE", InnerEnum.NONE, 0,"NONE");
//
//			private static readonly IList<Ranks> valueList = new List<Ranks>();
//
//			static Ranks()
//			{
//				valueList.Add(LEVEL_ONE);
//				valueList.Add(LEVEL_TWO);
//				valueList.Add(LEVEL_THREE);
//				valueList.Add(TAICHO);
//				valueList.Add(NONE);
//			}
//
//			public enum InnerEnum
//			{
//				LEVEL_ONE,
//				LEVEL_TWO,
//				LEVEL_THREE,
//				TAICHO,
//				NONE
//			}
//
//			private readonly string nameValue;
//			private readonly int ordinalValue;
//			private readonly InnerEnum innerEnumValue;
//			private static int nextOrdinal = 0;
//
//			private int numVal;
//			private string name;
//
//			internal Ranks(string name, InnerEnum innerEnum, int numVal, string _name)
//			{
//				this.numVal = numVal;
//				this.name = _name;
//
//				nameValue = name;
//				ordinalValue = nextOrdinal++;
//				innerEnumValue = innerEnum;
//			}
//
//			public int NumVal
//			{
//				get
//				{
//					return numVal;
//				}
//			}
//
//			public string Name
//			{
//				get
//				{
//					return this.name;
//				}
//			}
//
//		public static IList<Ranks> values()
//		{
//			return valueList;
//		}
//
//		public InnerEnum InnerEnumValue()
//		{
//			return innerEnumValue;
//		}
//
//		public int ordinal()
//		{
//			return ordinalValue;
//		}
//
//		public override string ToString()
//		{
//			return nameValue;
//		}
//
//		public static Ranks valueOf(string name)
//		{
//			foreach (Ranks enumInstance in Ranks.values())
//			{
//				if (enumInstance.nameValue == name)
//				{
//					return enumInstance;
//				}
//			}
//			throw new System.ArgumentException(name);
//		}
	}

}