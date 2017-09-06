using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;

namespace MemoryGameLogicLib.Model
{	
    public class Dealer
    {
		[DataMember]
        public IList<Card> Deck { get; set; }

        Random Rand { get; set; }        

        CardPositioner Positioner { get; set; }

        public Dealer(IList<Card> deck,BoardDimensions dimensions)
        {
            this.Deck = deck;
			Rand = new Random();
			Positioner = new CardPositioner(dimensions);            
        }
		        
        public void GiveNewCards(PlayBoard board)
        {
            board.InGameCards = ShuffleAndPositionCards();
        }

        IList<Card> ShuffleAndPositionCards()
        {
            List<Card> cardsDealt = new List<Card>();
            while (cardsDealt.Count < Deck.Count)
            {
				Card cardToDeal = GetRandomCard();

                if (IsCardNotDealt(cardToDeal))
                {
                    cardToDeal.InGamePosition = Positioner.GetNextAvailablePosition();
                    cardsDealt.Add(cardToDeal);
                }
            }
            return cardsDealt;
        }

		Card GetRandomCard()
		{
			int cardNo = Rand.Next(0, Deck.Count);
			Card cardToDeal = Deck.ElementAt(cardNo);
			return cardToDeal;
		}

        bool IsCardNotDealt(Card cardToDeal)
        {
			return !cardToDeal.HasInGamePosition;
        }
    }

    class CardPositioner 
    {
        public BoardDimensions BoardDimensions { get; set; }

        public List<Position> AvailablePositions { get; set; }

        public CardPositioner(BoardDimensions dimensions)
        {
            this.BoardDimensions = dimensions;
            CreateAvailabePositions();
        }

        void CreateAvailabePositions()
        {
            AvailablePositions = new List<Position>();
            for (ushort xpos = 0; xpos < BoardDimensions.Width; xpos++)
                for (ushort ypos = 0; ypos < BoardDimensions.Height; ypos++)
                    AvailablePositions.Add(new Position(xpos, ypos));
        }

        public Position GetNextAvailablePosition()
        {
            if (AvailablePositions.Count > 0)
            {
                Position availablePosition = AvailablePositions.First();
                AvailablePositions.Remove(availablePosition);
                return availablePosition;
            }
            else
            {
                return new Position(null, null);
            }
        }
    }
}
