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
	public class TaichoUnit : MovableObject
	{

		public TaichoUnit(Player p) : base(p, Ranks.TAICHO)
		{
		}

	//	@Override
	//	public Color getColor(){
	////		return Utils.blendColor(this.getPlayer().getColor(), Color.WHITE, 0.4);
	//	}

	}

}