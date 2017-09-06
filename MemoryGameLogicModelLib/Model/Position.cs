using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace MemoryGameLogicLib.Model
{
    [DebuggerDisplay("{XPos},{YPos}")]
	[DataContract]
    public class Position : IEquatable<Position>    
	{
		[DataMember]
        public ushort? XPos
        {
            get;
            set;
        }

		[DataMember]
        public ushort? YPos
        {
            get;
            set;
        }

        public Position(ushort? xPos, ushort? yPos)
        {
            this.XPos = xPos;
            this.YPos = yPos;
        }

        public override string ToString()
        {
            return XPos + "," + YPos;
        }

        public bool Equals(Position other)
        {
            if (other == null) return false;
            else if ((other.XPos == this.XPos) && (other.YPos == this.YPos)) return true;
            else return false;
        }
    }
}

