using System;

namespace com.cosmichorizons.utilities
{

	/// <summary>
	/// Class i found online, used to to blend colors for different level units 
	/// 
	/// </summary>
	public class Utils
	{
		 /// <summary>
		 /// Blend two colors.
		 /// </summary>
		 /// <param name="color1">  First color to blend. </param>
		 /// <param name="color2">  Second color to blend. </param>
		 /// <param name="ratio">   Blend ratio. 0.5 will give even blend, 1.0 will return
		 ///                color1, 0.0 will return color2 and so on. </param>
		 /// <returns>        Blended color. </returns>
//		  public static Color blendColor(Color color1, Color color2, double ratio)
//		  {
//			float r = (float) ratio;
//			float ir = (float) 1.0 - r;
//
//			float[] rgb1 = new float[3];
//			float[] rgb2 = new float[3];
//
//			color1.getColorComponents(rgb1);
//			color2.getColorComponents(rgb2);
//
//			Color color = new Color(rgb1[0] * r + rgb2[0] * ir, rgb1[1] * r + rgb2[1] * ir, rgb1[2] * r + rgb2[2] * ir);
//
//			return color;
//		  }


		  /// <summary>
		  /// Make a color darker.
		  /// </summary>
		  /// <param name="color">     Color to make darker. </param>
		  /// <param name="fraction">  Darkness fraction. </param>
		  /// <returns>          Darker color. </returns>
//		  public static Color darkenColor(Color color, double fraction)
//		  {
//			int red = (int) Math.Round(color.Red * (1.0 - fraction));
//			int green = (int) Math.Round(color.Green * (1.0 - fraction));
//			int blue = (int) Math.Round(color.Blue * (1.0 - fraction));
//
//			if (red < 0)
//			{
//				red = 0;
//			}
//			else if (red > 255)
//			{
//				red = 255;
//			}
//			if (green < 0)
//			{
//				green = 0;
//			}
//			else if (green > 255)
//			{
//				green = 255;
//			}
//			if (blue < 0)
//			{
//				blue = 0;
//			}
//			else if (blue > 255)
//			{
//				blue = 255;
//			}
//
//			int alpha = color.Alpha;
//
//			return new Color(red, green, blue, alpha);
//		  }



		  /// <summary>
		  /// Make a color lighter.
		  /// </summary>
		  /// <param name="color">     Color to make lighter. </param>
		  /// <param name="fraction">  Darkness fraction. </param>
		  /// <returns>          Lighter color. </returns>
//		  public static Color lightenColor(Color color, double fraction)
//		  {
//			int red = (int) Math.Round(color.Red * (1.0 + fraction));
//			int green = (int) Math.Round(color.Green * (1.0 + fraction));
//			int blue = (int) Math.Round(color.Blue * (1.0 + fraction));
//
//			if (red < 0)
//			{
//				red = 0;
//			}
//			else if (red > 255)
//			{
//				red = 255;
//			}
//			if (green < 0)
//			{
//				green = 0;
//			}
//			else if (green > 255)
//			{
//				green = 255;
//			}
//			if (blue < 0)
//			{
//				blue = 0;
//			}
//			else if (blue > 255)
//			{
//				blue = 255;
//			}
//
//			int alpha = color.Alpha;
//
//			return new Color(red, green, blue, alpha);
//		  }
	}

}