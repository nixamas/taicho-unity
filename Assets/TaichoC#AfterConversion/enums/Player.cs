using UnityEngine;
using System.Collections.Generic;

namespace com.cosmichorizons.enums
{

	//using Color = com.badlogic.gdx.graphics.Color;

	//import java.awt.Color;

	/// <summary>
	/// Enum for all possible players of the game
	/// @author Ryan
	/// 
	/// </summary>
	public enum Player
	{
//JAVA TO C# CONVERTER TODO TASK: The following line could not be converted:
		NONE
//		{
//	//		Color color = Color.WHITE;
//	//		public void setColor(Color col){
//	//			color = col;
//	//		}
//	//		public Color getColor(){
//	//			return color;
//	//		}
//			public string UserReadableString
//			{
//				get
//				{
//					return "None";
//				}
//			}
//			public string Name
//			{
//				get
//				{
//					return "NONE";
//				}
//			}
//		}
//JAVA TO C# CONVERTER TODO TASK: The following line could not be converted:
	   , PLAYER_ONE
//	   {
//	//		Color color = TaichoColors.PLAYER_ONE_LVL1.getColor();
//	//		public void setColor(Color col){
//	//			color = col;
//	//		}
//	//		public Color getColor(){
//	//			return color;
//	//		}
//			public string UserReadableString
//			{
//				get
//				{
//					return "Player One";
//				}
//			}
//			public string Name
//			{
//				get
//				{
//					return "PLAYER_ONE";
//				}
//			}
//	   }
//JAVA TO C# CONVERTER TODO TASK: The following line could not be converted:
	   , PLAYER_TWO
//	   {
//	//		Color color = TaichoColors.PLAYER_TWO_LVL1.getColor();
//	//		public void setColor(Color col){
//	//			color = col;
//	//		}
//	//		public Color getColor(){
//	//			return color;
//	//		}
//			public string UserReadableString
//			{
//				get
//				{
//					return "Player Two";
//				}
//			}
//			public string Name
//			{
//				get
//				{
//					return "PLAYER_TWO";
//				}
//			}
//	   }

	//	public abstract void setColor(Color col);
	//	public abstract Color getColor();

//		public static IList<Player> values()
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
//		public static Player valueOf(string name)
//		{
//			foreach (Player enumInstance in Player.values())
//			{
//				if (enumInstance.nameValue == name)
//				{
//					return enumInstance;
//				}
//			}
//			throw new System.ArgumentException(name);
//		}
	}
	public static class PlayerExtensions {
		public static Color getPlayerColor(this Player player) {
			switch (player) {
			case Player.PLAYER_ONE:
				return Color.magenta;
			case Player.PLAYER_TWO:
				return Color.cyan;
			case Player.NONE:
			default:
				return Color.white;

			}
		}
	}

}