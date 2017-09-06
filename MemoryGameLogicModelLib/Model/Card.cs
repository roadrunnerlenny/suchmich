using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace MemoryGameLogicLib.Model
{
    [DebuggerDisplay("{CardSymbol}({InGamePosition})")]
	[DataContract]
    public class Card 
    {
		[DataMember]
        public Position InGamePosition { get; set; }

		public bool HasInGamePosition
		{
			get { return InGamePosition != null; }
		}

		[DataMember]
        public Symbol CardSymbol { get; set; }

		[DataMember]
        public bool IsRevealed { get; private set; }

		[DataMember]
		public bool IsDiscarded { get; private set; }

		[DataMember]
		public Player OwningPlayer { get; private set; }

        public void Reveal()
        {
            this.IsRevealed = true;
        }

        public void Unreveal()
        {
            this.IsRevealed = false;
        }

		public void DiscardCard(Player owner)
		{
			this.IsDiscarded = true;
			this.InGamePosition = null;			
			this.OwningPlayer = owner;
		}

        public override string ToString()
        {
            return this.CardSymbol.ToString() + "(" + InGamePosition.ToString() + ")";
        }
				
		internal static List<Card> ConvertSetsToCards(IList<CardSet> cardSets)
		{
			List<Card> result = new List<Card>();
			foreach (CardSet set in cardSets)
			{
				result.Add(set.Card1);
				result.Add(set.Card2);
			}
			return result;
		}
	}
}

