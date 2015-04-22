namespace com.cosmichorizons.utilities
{

	/// <summary>
	/// Convienence class that allows us to dynamically create the onscreen board in different dimensions
	/// @author Ryan
	/// 
	/// </summary>
	public class BoardDimensions
	{
		private readonly int COMPONENT_DIMENSION;
		private readonly int CHARACTER_DIMENSION;
		private readonly int BOARD_LENGTH;
		private readonly int BOARD_WIDTH;
		private readonly int CHARACTER_OFFSET;
		private readonly int BORDER_SIZE;

		private int initialDimension = 20;

		public BoardDimensions(int i)
		{
			if (i > 20)
			{
				this.initialDimension = i;
			}
			this.COMPONENT_DIMENSION = this.initialDimension;
			this.BOARD_LENGTH = (this.initialDimension * 15 + 4);
			this.BOARD_WIDTH = (this.initialDimension * 9 + 4);
			this.CHARACTER_DIMENSION = (int)(this.initialDimension * 0.65); //- 10); //5
			this.CHARACTER_OFFSET = ((this.COMPONENT_DIMENSION - this.CHARACTER_DIMENSION) / 2);

			if ((i / 10) < 2)
			{
				this.BORDER_SIZE = 2;
			}
			else
			{
				this.BORDER_SIZE = i / 10;
			}
		}

		public virtual int InitialDimension
		{
			get
			{
				return this.initialDimension;
			}
		}

		public virtual int ComponentSize
		{
			get
			{
				return this.COMPONENT_DIMENSION;
			}
		}

		public virtual int BoardLength
		{
			get
			{
				return this.BOARD_LENGTH;
			}
		}
		public virtual int BoardWidth
		{
			get
			{
				return this.BOARD_WIDTH;
			}
		}

		public virtual int CharacterDimension
		{
			get
			{
				return this.CHARACTER_DIMENSION;
			}
		}

		public virtual int CharacterOffset
		{
			get
			{
				return this.CHARACTER_OFFSET;
			}
		}

		public virtual int BorderSize
		{
			get
			{
				return BORDER_SIZE;
			}
		}

	}

}