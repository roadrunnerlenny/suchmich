using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemoryGameLogicLib.Model;
using WpfFundamentals.ViewModelBase;

namespace MemoryClient.ViewModels
{
	public class CardSetViewModel : ViewModel<CardSet>
	{
		public CardViewModel Card1 
		{
			get { return _card1;  }
			set
			{
				if (Card1 != value)
				{
					_card1 = value;
					Model.Card1 = _card1.Model;
					OnPropertyChanged(() => this.Card1);
				}
			}
		} 
		CardViewModel _card1;

        public CardViewModel Card2 
		{
			get { return _card2;  }
			set
			{
				if (Card2 != value)
				{
					_card2 = value;
					Model.Card2 = _card2.Model;
					OnPropertyChanged(() => this.Card2);
				}
			}
		} 
		CardViewModel _card2;

		public CardSetViewModel() : base(new CardSet(new Card(), new Card()))
		{
			Card1 = new CardViewModel();
			Card2 = new CardViewModel();
		}

		public CardSetViewModel(CardSet model) : base(model)
		{
			Card1 = new CardViewModel(model.Card1);
			Card2 = new CardViewModel(model.Card2);
		}

		public CardSetViewModel(CardSetViewModel other) : base(other)
		{
			Card1 = new CardViewModel(other.Card1);
			Card2 = new CardViewModel(other.Card2);
		}
	}
}
