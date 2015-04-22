using System;

namespace com.cosmichorizons.exceptions
{

	/// <summary>
	/// Exception that is thrown when a BC is not found. 
	/// Can be ignored, best if used with try/catch
	/// @author Ryan
	/// 
	/// </summary>
	public class BoardComponentNotFoundException : Exception
	{

		/// 
		private const long serialVersionUID = 1L;

		public BoardComponentNotFoundException(string message)
			: base(message)
		{
		}

		public BoardComponentNotFoundException()
			: base("BOARD COMPONENT NOT FOUND EXCEPTION  ::> ")
		{
		}

//		public virtual string Message
//		{
//			get
//			{
//				return "BOARD COMPONENT NOT FOUND EXCEPTION  ::> ";
//			}
//		} //getMessage()

	} //BoardComponentNotFoundException

}