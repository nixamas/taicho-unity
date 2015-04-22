using System.Collections.Generic;

namespace com.cosmichorizons.characters
{

	//import java.awt.Color;

	//using Color = com.badlogic.gdx.graphics.Color;
	using MovableObject = com.cosmichorizons.basecomponents.MovableObject;
	using ComponentImages = com.cosmichorizons.enums.ComponentImages;
	using Player = com.cosmichorizons.enums.Player;
	using Ranks = com.cosmichorizons.enums.Ranks;
	// TaichoColors = com.cosmichorizons.enums.TaichoColors;


	/// <summary>
	/// reference basecomponents/MovableObject.java for more information
	/// @author Ryan
	/// </summary>
	public class TwoUnit : MovableObject
	{

		internal IList<MovableObject> components;

		public TwoUnit(Player p) : base(p, Ranks.LEVEL_TWO)
		{
		}

		public TwoUnit(Player p, MovableObject comp1, MovableObject comp2) : base(p, Ranks.LEVEL_TWO)
		{
			components = new List<MovableObject>();
			components.Add(comp1);
			components.Add(comp2);
		}

		public virtual List<MovableObject> Components
		{
			get
			{
				return (List<MovableObject>) this.components;
			}
		}

		public virtual MovableObject removeUnitFromStack()
		{
				//remove last unit from list
			MovableObject comp = this.components [this.components.Count - 1];
			this.components.Remove(this.components[this.components.Count-1]);
			return comp;
		}

	//	@Override
	//	public Color getColor(){
	//		if( this.player == Player.PLAYER_ONE ){
	//			return TaichoColors.PLAYER_ONE_LVL2.getColor();
	//		}else if( this.player == Player.PLAYER_TWO ){
	//			return TaichoColors.PLAYER_TWO_LVL2.getColor();
	//		}else{
	//			return Color.WHITE;
	//		}
	////		return Utils.blendColor(this.getPlayer().getColor(), Color.GRAY, 0.4);// (, 70);
	//	}
	}

}