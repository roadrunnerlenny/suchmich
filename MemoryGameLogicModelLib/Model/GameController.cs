using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using MemoryGameLogicLib.DataReader;

namespace MemoryGameLogicLib.Model
{
    [DataContract]
    public class GameController
    {
        [DataMember]
        public PlayBoard Board { get; set; }

        [DataMember]
        public bool HasBoard
        {
            get { return Board != null; }
        }

        [DataMember]
        public bool IsGameRunning { get; set; }

        [DataMember]
        public PlayerHandler PlayerHandler { get; set; }

        [DataMember]
        public DeckDescription Deck { get; set; }

        [DataMember]
        public List<DeckDescription> Decks { get; set; }

        public bool HasDeck
        {
            get { return Deck != null; }
        }

        public GameController()
        {
            Board = new PlayBoard();
            Init();
        }

        public GameController(BoardDimensions dimensions)
        {
            Board = new PlayBoard(dimensions);
            Init();
        }

        void Init()
        {
            PlayerHandler = new PlayerHandler();
            Decks = DeckReader.ReadDeckNames();
            Deck = Decks.FirstOrDefault();
        }

        public void StartGame(List<Player> players)
        {
            Board.UnrevealCards();
            PlayerHandler.InitPlayers(players);
            IsGameRunning = true;
        }

        public void StopGame()
        {
            IsGameRunning = false;
            Board.ClearBoard();
        }

        public void RevealCard(Position position)
        {
            if (Board.CanRevealCard && Board.HasInGameCardAtPosition(position))
            {
                Card card = Board.FindInGameCard(position);
                card.Reveal();
                Board.SetNextUnrevealedCard(card);
            }
        }

        public bool CheckIfCardsMatchAndDiscard()
        {
            if (Board.HasBothCardsUnrevealed)
            {
                Board.CanRevealCard = false;
                if (Board.UnrevealedCard1.CardSymbol.Equals(Board.UnrevealedCard2.CardSymbol))
                {
                    DiscardCards(Board.UnrevealedCard1, Board.UnrevealedCard2);
                    return true;
                }
            }
            return false;
        }

        void DiscardCards(Card UnrevealedCard1, Card UnrevealedCard2)
        {
            PlayerHandler.CurrentPlayer.Rack.DiscardCards(UnrevealedCard1, UnrevealedCard2);
            Board.RemoveInGameCards(UnrevealedCard1, UnrevealedCard2);
        }

        public bool CheckIfGameIsOver()
        {
            if (Board.InGameCards.Count <= 0)
            {
                IsGameRunning = false;
                return true;
            }
            else
            {
                return false;
            }
        }


        public void LayCardsOnBoard()
        {
            if (HasBoard && Board.HasDimensions && HasDeck)
            {
                CardDeck deck = new CardDeck(Board.NumberOfSetsToFitBoard, this.Deck.Name);
                Dealer dealer = new Dealer(deck.AllCards, Board.Dimensions);
                dealer.GiveNewCards(Board);
            }
        }
    }
}

