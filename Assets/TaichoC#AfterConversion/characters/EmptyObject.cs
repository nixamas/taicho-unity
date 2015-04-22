namespace com.cosmichorizons.characters
{

	using MovableObject = com.cosmichorizons.basecomponents.MovableObject;
	using ComponentImages = com.cosmichorizons.enums.ComponentImages;
	using Player = com.cosmichorizons.enums.Player;
	using Ranks = com.cosmichorizons.enums.Ranks;


	/// <summary>
	/// fills BC's that are not occupied by other (actual game) characters
	/// Empty, non null, values
	/// Player = Player.None, Rank = Ranks.None, ComponentImages = ComponentImages.None
	/// @author Ryan
	/// 
	/// </summary>
	public class EmptyObject : MovableObject
	{

		public EmptyObject() : base(Player.NONE, Ranks.NONE)
		{
		}

	}

}