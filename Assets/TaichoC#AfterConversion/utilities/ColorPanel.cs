//namespace com.cosmichorizons.utilities
//{
//
//
//	/// <summary>
//	/// Class used to draw image icons on the screen. 
//	/// Only used with drawing icons, not regular boxes
//	/// @author Ryan
//	/// 
//	/// </summary>
//	public class ColorPanel : JPanel
//	{
//		/// 
//		private const long serialVersionUID = 1L;
//		internal BufferedImage img;
//
//		public ColorPanel(BufferedImage image)
//		{
//		img = image;
//		}
//
//		public virtual void paintComponent(Graphics g)
//		{
//			base.paintComponent(g);
//			Graphics2D g2d = (Graphics2D) g;
//			g2d.drawImage(img, null, 50,50);
//		}
//	}
//
//}