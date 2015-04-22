using System.Collections.Generic;

namespace com.cosmichorizons.enums
{

	/// <summary>
	/// Not important to game functionality but used to display icons instead of shapes
	/// Each enum corresponds to a icon that will be used. The location field stores the full directory path of the image
	/// and the name member obviously stores the name. 
	/// @author Ryan
	/// 
	/// </summary>
	public sealed class ComponentImages
	{
		public static readonly ComponentImages LEVEL_ONE_IMAGE = new ComponentImages("LEVEL_ONE_IMAGE", InnerEnum.LEVEL_ONE_IMAGE, "images/", "levelOneImage.jpg");
		public static readonly ComponentImages LEVEL_TWO_IMAGE = new ComponentImages("LEVEL_TWO_IMAGE", InnerEnum.LEVEL_TWO_IMAGE, "images/", "levelTwoImage.jpg");
		public static readonly ComponentImages LEVEL_THREE_IMAGE = new ComponentImages("LEVEL_THREE_IMAGE", InnerEnum.LEVEL_THREE_IMAGE, "images/", "levelThreeImage.jpg");
		public static readonly ComponentImages TAICHO_IMAGE = new ComponentImages("TAICHO_IMAGE", InnerEnum.TAICHO_IMAGE, "images/", "TaichoImage.png");
		public static readonly ComponentImages GAME_BOARD_IMAGE = new ComponentImages("GAME_BOARD_IMAGE", InnerEnum.GAME_BOARD_IMAGE, "images/", "");
		public static readonly ComponentImages OUT_OF_BOUNDS_IMAGE = new ComponentImages("OUT_OF_BOUNDS_IMAGE", InnerEnum.OUT_OF_BOUNDS_IMAGE, "images/", "");
		public static readonly ComponentImages NONE = new ComponentImages("NONE", InnerEnum.NONE, "", "");

		private static readonly IList<ComponentImages> valueList = new List<ComponentImages>();

		static ComponentImages()
		{
			valueList.Add(LEVEL_ONE_IMAGE);
			valueList.Add(LEVEL_TWO_IMAGE);
			valueList.Add(LEVEL_THREE_IMAGE);
			valueList.Add(TAICHO_IMAGE);
			valueList.Add(GAME_BOARD_IMAGE);
			valueList.Add(OUT_OF_BOUNDS_IMAGE);
			valueList.Add(NONE);
		}

		public enum InnerEnum
		{
			LEVEL_ONE_IMAGE,
			LEVEL_TWO_IMAGE,
			LEVEL_THREE_IMAGE,
			TAICHO_IMAGE,
			GAME_BOARD_IMAGE,
			OUT_OF_BOUNDS_IMAGE,
			NONE
		}

		private readonly string nameValue;
		private readonly int ordinalValue;
		private readonly InnerEnum innerEnumValue;
		private static int nextOrdinal = 0;

		private string location;
		private string name;

		internal ComponentImages(string name, InnerEnum innerEnum, string loc, string n)
		{
			this.location = loc + n;
			this.name = n;

			nameValue = name;
			ordinalValue = nextOrdinal++;
			innerEnumValue = innerEnum;
		}

		public string ImageLocation
		{
			get
			{
				return this.location;
			}
		}

		public string ImageName
		{
			get
			{
				return this.name;
			}
		}

		public string ThumbnailLocation
		{
			get
			{
				return "thumbnails/" + this.name;
			}
		}


		public static IList<ComponentImages> values()
		{
			return valueList;
		}

		public InnerEnum InnerEnumValue()
		{
			return innerEnumValue;
		}

		public int ordinal()
		{
			return ordinalValue;
		}

		public override string ToString()
		{
			return nameValue;
		}

		public static ComponentImages valueOf(string name)
		{
			foreach (ComponentImages enumInstance in ComponentImages.values())
			{
				if (enumInstance.nameValue == name)
				{
					return enumInstance;
				}
			}
			throw new System.ArgumentException(name);
		}
	}

}