using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryGameLogicLib.Exceptions
{
	public class NoPlayerActiveException : Exception
	{
		public const string DefaultErrorMessage = "Es ist zur Zeit kein Spieler aktiv!";

		public NoPlayerActiveException()
			: base(DefaultErrorMessage)
        { }

        public NoPlayerActiveException(string message) : base(message)
        { }

		public NoPlayerActiveException(string message, Exception innerException)
			: base(message, innerException)
        { }
	}
}
