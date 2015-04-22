using System.Collections.Generic;

namespace com.cosmichorizons.enums
{

//	using Color = com.badlogic.gdx.graphics.Color;
//
//	//import java.awt.Color;
//
//	/// <summary>
//	/// Enum for custom colors. 
//	/// 
//	/// Provides an easy way to change the colors of 
//	/// the board without hunting down code
//	/// @author Ryan
//	/// 
//	/// </summary>
//	public sealed class TaichoColors
//	{
//		public static readonly TaichoColors PLAYER_ONE_LVL1 = new TaichoColors("PLAYER_ONE_LVL1", InnerEnum.PLAYER_ONE_LVL1, new com.badlogic.gdx.graphics.Color((float)247, (float)213, (float)59, (float)1.0)); //'yellow'
//		public static readonly TaichoColors PLAYER_ONE_LVL2 = new TaichoColors("PLAYER_ONE_LVL2", InnerEnum.PLAYER_ONE_LVL2, new com.badlogic.gdx.graphics.Color((float)191, (float)162, (float)46, (float)1.0));
//		public static readonly TaichoColors PLAYER_ONE_LVL3 = new TaichoColors("PLAYER_ONE_LVL3", InnerEnum.PLAYER_ONE_LVL3, new com.badlogic.gdx.graphics.Color((float)122, (float)104, (float)29, (float)1.0));
//		public static readonly TaichoColors PLAYER_TWO_LVL1 = new TaichoColors("PLAYER_TWO_LVL1", InnerEnum.PLAYER_TWO_LVL1, new com.badlogic.gdx.graphics.Color((float)42, (float)88, (float)172, (float)1.0)); //'blue'
//		public static readonly TaichoColors PLAYER_TWO_LVL2 = new TaichoColors("PLAYER_TWO_LVL2", InnerEnum.PLAYER_TWO_LVL2, new com.badlogic.gdx.graphics.Color((float)28, (float)60, (float)115, (float)1.0));
//		public static readonly TaichoColors PLAYER_TWO_LVL3 = new TaichoColors("PLAYER_TWO_LVL3", InnerEnum.PLAYER_TWO_LVL3, new com.badlogic.gdx.graphics.Color((float)11, (float)24, (float)46, (float)1.0));
//		public static readonly TaichoColors GAME_BOARD_LIGHT = new TaichoColors("GAME_BOARD_LIGHT", InnerEnum.GAME_BOARD_LIGHT, new com.badlogic.gdx.graphics.Color((float)171, (float)171, (float)171, (float)1.0)); //gray
//		public static readonly TaichoColors GAME_BOARD_DARK = new TaichoColors("GAME_BOARD_DARK", InnerEnum.GAME_BOARD_DARK, new com.badlogic.gdx.graphics.Color((float)84, (float)84, (float)84, (float)1.0)); //dark gray
//		public static readonly TaichoColors GAME_BOARD_HIGHLIGHT = new TaichoColors("GAME_BOARD_HIGHLIGHT", InnerEnum.GAME_BOARD_HIGHLIGHT, new com.badlogic.gdx.graphics.Color((float)0, (float)69, (float)40, (float)1.0)); //green
//		public static readonly TaichoColors GAME_BOARD_SELECTED = new TaichoColors("GAME_BOARD_SELECTED", InnerEnum.GAME_BOARD_SELECTED, new com.badlogic.gdx.graphics.Color((float)0, (float)60, (float)69, (float)1.0)); //light bluish
//		public static readonly TaichoColors GAME_BOARD_STACKABLE = new TaichoColors("GAME_BOARD_STACKABLE", InnerEnum.GAME_BOARD_STACKABLE, new com.badlogic.gdx.graphics.Color((float)64, (float)0, (float)70, (float)1.0)); //purple
//		public static readonly TaichoColors GAME_BOARD_ATTACKABLE = new TaichoColors("GAME_BOARD_ATTACKABLE", InnerEnum.GAME_BOARD_ATTACKABLE, new com.badlogic.gdx.graphics.Color((float)70, (float)0, (float)7, (float)1.0)); //red
//
//		private static readonly IList<TaichoColors> valueList = new List<TaichoColors>();
//
//		static TaichoColors()
//		{
//			valueList.Add(PLAYER_ONE_LVL1);
//			valueList.Add(PLAYER_ONE_LVL2);
//			valueList.Add(PLAYER_ONE_LVL3);
//			valueList.Add(PLAYER_TWO_LVL1);
//			valueList.Add(PLAYER_TWO_LVL2);
//			valueList.Add(PLAYER_TWO_LVL3);
//			valueList.Add(GAME_BOARD_LIGHT);
//			valueList.Add(GAME_BOARD_DARK);
//			valueList.Add(GAME_BOARD_HIGHLIGHT);
//			valueList.Add(GAME_BOARD_SELECTED);
//			valueList.Add(GAME_BOARD_STACKABLE);
//			valueList.Add(GAME_BOARD_ATTACKABLE);
//		}
//
//		public enum InnerEnum
//		{
//			PLAYER_ONE_LVL1,
//			PLAYER_ONE_LVL2,
//			PLAYER_ONE_LVL3,
//			PLAYER_TWO_LVL1,
//			PLAYER_TWO_LVL2,
//			PLAYER_TWO_LVL3,
//			GAME_BOARD_LIGHT,
//			GAME_BOARD_DARK,
//			GAME_BOARD_HIGHLIGHT,
//			GAME_BOARD_SELECTED,
//			GAME_BOARD_STACKABLE,
//			GAME_BOARD_ATTACKABLE
//		}
//
//		private readonly string nameValue;
//		private readonly int ordinalValue;
//		private readonly InnerEnum innerEnumValue;
//		private static int nextOrdinal = 0;
//
//		private com.badlogic.gdx.graphics.Color col;
//
//		internal TaichoColors(string name, InnerEnum innerEnum, com.badlogic.gdx.graphics.Color c)
//		{
//			this.col = c;
//
//			nameValue = name;
//			ordinalValue = nextOrdinal++;
//			innerEnumValue = innerEnum;
//		}
//
//		public com.badlogic.gdx.graphics.Color Color
//		{
//			get
//			{
//				return this.col;
//			}
//		}
//
//
//
//		public static IList<TaichoColors> values()
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
//		public static TaichoColors valueOf(string name)
//		{
//			foreach (TaichoColors enumInstance in TaichoColors.values())
//			{
//				if (enumInstance.nameValue == name)
//				{
//					return enumInstance;
//				}
//			}
//			throw new System.ArgumentException(name);
//		}
//	}

}