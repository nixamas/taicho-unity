using System.Collections.Generic;

namespace com.cosmichorizons.characters
{
	using MovableObject = com.cosmichorizons.basecomponents.MovableObject;
	using ComponentImages = com.cosmichorizons.enums.ComponentImages;
	using Player = com.cosmichorizons.enums.Player;
	using Ranks = com.cosmichorizons.enums.Ranks;


	/// <summary>
	/// reference basecomponents/MovableObject.java for more information
	/// @author Ryan
	/// </summary>
	public class ThreeUnit : MovableObject
	{

		internal IList<MovableObject> components;

		public ThreeUnit(Player p) : base(p, Ranks.LEVEL_THREE)
		{
			combatValue = 3;
		}

		public ThreeUnit(Player p, MovableObject comp1, MovableObject comp2, MovableObject comp3) : base(p, Ranks.LEVEL_THREE)
		{
			components = new List<MovableObject>();
			components.Add(comp1);
			components.Add(comp2);
			components.Add(comp3);
			combatValue = 3;
		}

		public ThreeUnit(Player p, MovableObject comp1, MovableObject comp2) : base(p, Ranks.LEVEL_THREE)
		{
			components = new List<MovableObject>();
			List<MovableObject> tempList;
			if (comp1.Rank == Ranks.LEVEL_TWO)
			{
				TwoUnit tempUnit = (TwoUnit) comp1;
				tempList = tempUnit.Components;
				components.Add(tempList[0]);
				components.Add(tempList[1]);
				components.Add(comp2);
			}
			else if (comp2.Rank == Ranks.LEVEL_TWO)
			{
				TwoUnit tempUnit = (TwoUnit) comp2;
				tempList = tempUnit.Components;
				components.Add(tempList[0]);
				components.Add(tempList[1]);
				components.Add(comp1);
			}
			combatValue = 3;
		}

		public virtual List<MovableObject> Components
		{
			get
			{
				return (List<MovableObject>) this.components;
			}
		}

		public override Player Player
		{
			set
			{
				player = value;
			}
			get
			{
				return player;
			}
		}


	//	@Override
	//	public Color getColor(){
	//		if( this.player == Player.PLAYER_ONE ){
	//			return TaichoColors.PLAYER_ONE_LVL3.getColor();
	//		}else if( this.player == Player.PLAYER_TWO ){
	//			return TaichoColors.PLAYER_TWO_LVL3.getColor();
	//		}else{
	//			return Color.WHITE;
	//		}
	////		return Utils.blendColor(this.getPlayer().getColor(), Color.DARK_GRAY, 0.4);
	//	}

		public virtual MovableObject removeUnitFromStack()
		{
			//remove last unit from list
			MovableObject comp = this.components [this.components.Count - 1];
			this.components.Remove(this.components[this.components.Count-1]);
			return comp;
		}
	}

}