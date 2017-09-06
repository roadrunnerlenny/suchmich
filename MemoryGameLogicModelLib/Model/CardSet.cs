using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MemoryGameLogicLib.Model
{
	[DataContract]
    public class CardSet
    {
		[DataMember]
        public Card Card1 { get; set; }

		[DataMember]
        public Card Card2 { get; set; }

        public CardSet(Card card1, Card card2)
        {
            this.Card1 = card1;
            this.Card2 = card2;
        }

		
    }
}
