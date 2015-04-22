namespace com.cosmichorizons.characters
{

	//import java.awt.Color;

	//using Color = com.badlogic.gdx.graphics.Color;
	using MovableObject = com.cosmichorizons.basecomponents.MovableObject;
	using ComponentImages = com.cosmichorizons.enums.ComponentImages;
	using Player = com.cosmichorizons.enums.Player;
	using Ranks = com.cosmichorizons.enums.Ranks;
	//using TaichoColors = com.cosmichorizons.enums.TaichoColors;


	/// <summary>
	/// reference basecomponents/MovableObject.java for more information
	/// @author Ryan
	/// </summary>
	public class OneUnit : MovableObject
	{

		public OneUnit(Player p) : base(p, Ranks.LEVEL_ONE)
		{
		}

	//	@Override
	//	public Color getColor(){
	//		if( this.player == Player.PLAYER_ONE ){
	//			return TaichoColors.PLAYER_ONE_LVL1.getColor();
	//		}else if( this.player == Player.PLAYER_TWO ){
	//			return TaichoColors.PLAYER_TWO_LVL1.getColor();
	//		}else{
	//			return Color.WHITE;
	//		}
	//	}

	}

}