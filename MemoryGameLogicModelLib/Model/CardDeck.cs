using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MemoryGameLogicLib.DataReader;
using System.Runtime.Serialization;

namespace MemoryGameLogicLib.Model
{
	[DataContract]
    public class CardDeck
    {
		[DataMember]
        public IList<CardSet> AllSets { get; private set; }

        public bool HasCards
        {
            get
            {
                return this.AllSets != null && this.AllSets.Count > 0;
            }
        }
				
        public IList<Card> AllCards
        {
            get
            {
                if (HasCards)
                    return Card.ConvertSetsToCards(AllSets);
                else
                    return null;
            }
        }

		[DataMember]
        public ushort NumberOfSets { get; set; }

		[DataMember]
		public string Name { get; set; }

        public CardDeck(ushort numberOfSets, string name)
        {
			NumberOfSets = numberOfSets;
			Name = name;						
			AllSets = DeckReader.ReadCardsFromData(numberOfSets, name);
        }
       
    }
}
