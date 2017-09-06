using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryGameLogicLib.Exceptions
{
    public class InvalidBoardDimensionsException : Exception
    {
		public const string DefaultErrorMessage = "Ungültige Dimensionen für das Spielfeld!";

		public InvalidBoardDimensionsException()
			: base(DefaultErrorMessage)
        { }

        public InvalidBoardDimensionsException(string message) : base(message)
        { }

        public InvalidBoardDimensionsException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
