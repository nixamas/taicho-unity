using UnityEngine;
using System.Collections.Generic;

namespace com.cosmichorizons.enums
{

	/// <summary>
	/// Enum for all possible players of the game
	/// @author Ryan
	/// 
	/// </summary>
	public enum Player
	{
		NONE,
		PLAYER_ONE,
		PLAYER_TWO
	}
	public static class PlayerExtensions {
		public static Color getPlayerColor(this Player player) {
			switch (player) {
			case Player.PLAYER_ONE:
				return Color.red;
			case Player.PLAYER_TWO:
				return Color.blue;
			case Player.NONE:
			default:
				return Color.white;

			}
		}
	}

}