using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemoryGameLogicLib.Exceptions;
using System.Runtime.Serialization;

namespace MemoryGameLogicLib.Model
{
	[DataContract]
	public class PlayBoard
	{
		[DataMember]
		public BoardDimensions Dimensions { get; set; }

		public bool HasDimensions
		{
			get
			{
				return Dimensions != null && Dimensions.Width.HasValue &&
					Dimensions.Height.HasValue &&
					Dimensions.Width > 0 &&
					Dimensions.Height > 0;
			}
		}

		public ushort NumberOfSetsToFitBoard
		{
			get
			{
				if (HasDimensions)
					return (ushort)( (Dimensions.Height ?? 0) * (Dimensions.Width ?? 0) / 2);
				else
					return 0;
			}
		}

		[DataMember]
		public IList<Card> InGameCards { get; set; }

		public bool HasInGameCards
		{
			get { return this.InGameCards != null && this.InGameCards.Count > 0; }
		}
				
		public bool CanRevealCard { get; set; }
		
		public Card UnrevealedCard1 { get; private set; }

		public bool HasUnrevealedCard1
		{
			get { return UnrevealedCard1 != null; }
		}

		public Card UnrevealedCard2 { get; private set; }

		public bool HasUnrevealedCard2
		{
			get { return UnrevealedCard2 != null; }
		}

		public bool HasBothCardsUnrevealed
		{
			get { return HasUnrevealedCard1 && HasUnrevealedCard2; }
		}

		public PlayBoard()
		{
			Init();
		}

		public PlayBoard(BoardDimensions dimensions)
		{
			this.Dimensions = dimensions;
			Init();
		}

		private void Init()
		{
			this.InGameCards = new List<Card>();
		}

		#region Interface Methods 
		
		public void UnrevealCards()
		{
			if (HasUnrevealedCard1 && !UnrevealedCard1.IsDiscarded)
				UnrevealedCard1.Unreveal();
			UnrevealedCard1 = null;
			if (HasUnrevealedCard2 && !UnrevealedCard2.IsDiscarded)
				UnrevealedCard2.Unreveal();
			UnrevealedCard2 = null;
			CanRevealCard = true;
		}

		public void RemoveInGameCards(Card UnrevealedCard1, Card UnrevealedCard2)
		{
			InGameCards.Remove(UnrevealedCard1);
			InGameCards.Remove(UnrevealedCard2);
		}

		#endregion

		public Card FindInGameCard(Position position)
		{
			return (from card in InGameCards
					where card.InGamePosition.Equals(position)
					select card).FirstOrDefault();
		}

		public bool CardStillInGame(Position position)
		{
			return FindInGameCard(position) != null;
		}


		internal bool HasInGameCardAtPosition(Position position)
		{
			return InGameCards.Any(card => card.InGamePosition.Equals(position));
		}

		internal void SetNextUnrevealedCard(Card card)
		{
			if (!HasUnrevealedCard1)
				UnrevealedCard1 = card;
			else if (!HasUnrevealedCard2)
				UnrevealedCard2 = card;
			else
				throw new InvalidMoveException();
		}

		internal void ClearBoard()
		{
			this.InGameCards.Clear();
			this.UnrevealedCard1 = null;
			this.UnrevealedCard2 = null;
			this.CanRevealCard = false;
			
		}
	}
}

