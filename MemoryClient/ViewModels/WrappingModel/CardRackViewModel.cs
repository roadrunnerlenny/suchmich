using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemoryGameLogicLib.Model;
using WpfFundamentals.ViewModelBase;
using WpfFundamentals.Helper;
using System.Collections.ObjectModel;

namespace MemoryClient.ViewModels
{
    public class CardRackViewModel : ViewModel<CardRack>
    {
        public int NumberOfSets
        {
            get { return Model.NumberOfSets; }
        }

        public ObservableCollection<CardSetViewModel> DiscardedCards
        {
            get { return _discardedCardsSynced.ObservableCollection; }
        }
        SyncedViewModelList<CardSet, CardSetViewModel> _discardedCardsSynced { get; set; }

        public CardRackViewModel()
            : base(new CardRack(new Player(String.Empty)))
        {
            _discardedCardsSynced = new SyncedViewModelList<CardSet, CardSetViewModel>(new List<CardSet>(), model => new CardSetViewModel(model));
        }

        public CardRackViewModel(CardRack model)
            : base(model)
        {
            _discardedCardsSynced = new SyncedViewModelList<CardSet, CardSetViewModel>(model.DiscardedCards, m => new CardSetViewModel(m));
        }

        public CardRackViewModel(CardRackViewModel other)
            : base(other)
        {
            _discardedCardsSynced = new SyncedViewModelList<CardSet, CardSetViewModel>(new List<CardSet>(other.DiscardedCards.Models()),
                model => new CardSetViewModel(model));
        }

        public void ResyncDiscardedCards()
        {
            _discardedCardsSynced.AdaptList(Model.DiscardedCards);
            OnPropertyChanged(() => this.DiscardedCards);
            OnPropertyChanged(() => this.NumberOfSets);
        }

        public void Clear()
        {
            this.DiscardedCards.Clear();
        }
    }
}
