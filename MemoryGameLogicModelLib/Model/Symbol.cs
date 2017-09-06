using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace MemoryGameLogicLib.Model
{
    [DebuggerDisplay("{Name}")]
	[DataContract]
    public class Symbol : IEquatable<Symbol>
    {
		[DataMember]
        public string Name { get; set; }

        public bool HasName 
		{ 
            get { return !String.IsNullOrEmpty(Name); }
        }

		[DataMember]
		public string FileName { get; set; }

		public bool HasFileName
		{
			get { return !String.IsNullOrEmpty(FileName);  }
		}

        public Symbol(string name)
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public bool Equals(Symbol other)
        {
            if (other == null) return false;
            else if (this.HasName && other.HasName && other.Name.Equals(this.Name)) return true;
            else return false;
        }
    }
}

