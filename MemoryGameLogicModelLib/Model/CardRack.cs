using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MemoryGameLogicLib.Model
{
	[DataContract]
    public class CardRack
    {
        public int NumberOfSets
        {
			get
			{
				return DiscardedCards.Count;
			}
        }

		[DataMember]
        public IList<CardSet> DiscardedCards
        {
            get;
            private set;
        }

		[DataMember]
		public Player Owner { get; set; }

        public CardRack(Player owner)
        {
            this.DiscardedCards = new List<CardSet>();
			this.Owner = owner;
        }

        public void DiscardCards(Card card1, Card card2)
        {
			card1.DiscardCard(Owner);
			card2.DiscardCard(Owner);
            this.DiscardedCards.Add(new CardSet(card1, card2));
        }		
    }
}

