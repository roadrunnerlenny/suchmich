using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MemoryGameLogicLib.DataReader
{
    [DataContract]
    public class DeckDescription : IEquatable<DeckDescription>
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string FileName { get; set; }

        public bool Equals(DeckDescription other)
        {
            if (other != null)
                return this.Name.Equals(other.Name);
            else
                return false;
        }
    }
}
