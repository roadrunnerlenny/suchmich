using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryClient.ViewModels
{
	public class EmptyCardViewModel : CardViewModel
	{
		public new PositionViewModel InGamePosition
		{
			get { return _inGamePosition; }
			set
			{
				if (InGamePosition != value)
				{
					_inGamePosition = value;
					OnPropertyChanged(() => this.InGamePosition);
				}
			}
		}
		PositionViewModel _inGamePosition;

		public new bool HasInGamePosition
		{
			get { return true;  }
		}

		public new EmptySymbolViewModel CardSymbol
		{
			get { return _cardSymbol; }
			private set
			{
				if (CardSymbol != value)
				{
					_cardSymbol = value;
					OnPropertyChanged(() => this.CardSymbol);
				}
			}
		} EmptySymbolViewModel _cardSymbol;

		public new bool IsRevealed
		{
			get { return false; }
		}

		public new bool IsDiscarded
		{
			get { return false; }
		}

		public bool IsEmptyCard
		{
			get { return true; }
		}

		public new PlayerViewModel OwningPlayer
		{
			get { return null; }
		}
		PlayerViewModel _owningPlayer;


		public EmptyCardViewModel(PositionViewModel position)
		{
			InGamePosition = new PositionViewModel(position);
			CardSymbol = new EmptySymbolViewModel();
		}

		public override string ToString()
		{
			return this.CardSymbol.ToString() + "(" + InGamePosition.ToString() + ")";
		}
	}
	
}
