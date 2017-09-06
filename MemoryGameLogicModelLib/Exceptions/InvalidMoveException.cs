using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryGameLogicLib.Exceptions
{
	public class InvalidMoveException : Exception
	{ 
		public const string DefaultErrorMessage = "Ungültiger Spielzug!";
		 public InvalidMoveException() : base(DefaultErrorMessage)
        { }

        public InvalidMoveException(string message) : base(message)
        { }

		public InvalidMoveException(string message, Exception innerException)
			: base(message, innerException)
        { }
	}
}
