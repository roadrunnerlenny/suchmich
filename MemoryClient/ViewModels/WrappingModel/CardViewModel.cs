using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemoryGameLogicLib.Model;
using WpfFundamentals.ViewModelBase;

namespace MemoryClient.ViewModels
{
	public class CardViewModel : ViewModel<Card>
	{
		public PositionViewModel InGamePosition
		{
			get { return _inGamePosition; }
			set
			{
				if (InGamePosition != value)
				{
					_inGamePosition = value;
					Model.InGamePosition = _inGamePosition.Model;
					OnPropertyChanged(() => this.InGamePosition);
				}
			}
		} 
		PositionViewModel _inGamePosition;

		public bool HasInGamePosition
		{
			get { return Model.HasInGamePosition; }			
		}		

		public SymbolViewModel CardSymbol
		{
			get { return _cardSymbol; }
			set
			{
				if (CardSymbol != value)
				{
					_cardSymbol = value;
					Model.CardSymbol = _cardSymbol.Model;
					OnPropertyChanged(() => this.CardSymbol);
				}
			}
		} 
		SymbolViewModel _cardSymbol;

		public bool IsRevealed
		{
			get { return Model.IsRevealed; }			
		}

		public bool IsDiscarded {
			get { return Model.IsDiscarded; }
		}

		public bool IsEmptyCard
		{
			get { return false; }
		}

		//TODO: Rekursion auflösen
		public PlayerViewModel OwningPlayer
		{
			get { return _owningPlayer; }
			private set
			{
			    if (OwningPlayer != value)
			    {
			        _owningPlayer = value;
			        OnPropertyChanged(() => this.OwningPlayer);
			    }
			}
		} 
		PlayerViewModel _owningPlayer;

		public CardViewModel() : base(new Card())
		{
			InGamePosition = new PositionViewModel();
			CardSymbol = new SymbolViewModel();
			//OwningPlayer = new PlayerViewModel(); TODO: Rekursion
		}

		public CardViewModel(Card model) :base(model)
		{
			InGamePosition = new PositionViewModel(model.InGamePosition);
			CardSymbol = new SymbolViewModel(model.CardSymbol);
			//OwningPlayer = new PlayerViewModel(model.OwningPlayer); TODO: Rekursion
		}

		public CardViewModel(CardViewModel other) : base(other)
		{
			InGamePosition = new PositionViewModel(other.InGamePosition);
			CardSymbol = new SymbolViewModel(other.CardSymbol);
			//OwningPlayer = new PlayerViewModel(other.OwningPlayer); TODO: Rekursion
		}			

		public override string ToString()
		{
			return Model.ToString();
		}	
		
	}
}
