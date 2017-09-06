using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemoryGameLogicLib.Model;
using WpfFundamentals.ViewModelBase;
using System.Collections.ObjectModel;
using WpfFundamentals.Helper;

namespace MemoryClient.ViewModels
{
    public class PlayBoardViewModel : ViewModel<PlayBoard>
    {
        public BoardDimensionsViewModel Dimensions
        {
            get { return _dimensions; }
            set
            {
                if (Dimensions != value)
                {
                    _dimensions = value;
                    Model.Dimensions = _dimensions.Model;
                    OnPropertyChanged(() => this.Dimensions);
                }
            }
        }
        BoardDimensionsViewModel _dimensions;

        public bool HasDimensions
        {
            get { return Model.HasDimensions; }
        }

        public ushort NumberOfSetsToFitBoard
        {
            get { return Model.NumberOfSetsToFitBoard; }
        }

        public ObservableCollection<CardViewModel> InGameCards
        {
            get { return _inGameCardsSynced.ObservableCollection; }
        }
        SyncedViewModelList<Card, CardViewModel> _inGameCardsSynced;

        public bool HasInGameCards
        {
            get { return Model.HasInGameCards; }
        }

        public ObservableCollection<CardRowViewModel> InGameRows
        {
            get
            {
                if (!HasDimensions || !HasInGameCards) return null;
                ObservableCollection<CardRowViewModel> result = new ObservableCollection<CardRowViewModel>();
                for (ushort i = 0; i < Dimensions.Height; i++)
                    result.Add(new CardRowViewModel(this, i));
                return result;
            }
        }

        public bool CanRevealCard
        {
            get { return Model.CanRevealCard; }
        }

        public CardViewModel UnrevealedCard1
        {
            get { return _unrevealedCard1; }
        }
        CardViewModel _unrevealedCard1;

        public bool HasUnrevealedCard1
        {
            get { return Model.HasUnrevealedCard1; }
        }

        public CardViewModel UnrevealedCard2
        {
            get { return _unrevealedCard2; }
        }
        CardViewModel _unrevealedCard2;

        public bool HasUnrevealedCard2
        {
            get { return Model.HasUnrevealedCard2; }
        }

        public bool HasBothCardsUnrevealed
        {
            get { return Model.HasBothCardsUnrevealed; }
        }

        public ReadOnlyObservableCollection<BoardDimensionsViewModel> DefaultDimensions
        {
            get { return _defaultDimensions; }
        }
        ReadOnlyObservableCollection<BoardDimensionsViewModel> _defaultDimensions;

        public PlayBoardViewModel()
            : base(new PlayBoard())
        {
            _unrevealedCard1 = new CardViewModel();
            _unrevealedCard2 = new CardViewModel();
            _dimensions = new BoardDimensionsViewModel();
            _inGameCardsSynced = new SyncedViewModelList<Card, CardViewModel>(new List<Card>(), m => new CardViewModel(m));
            Init();
        }

        public PlayBoardViewModel(PlayBoard model)
            : base(model)
        {
            _unrevealedCard1 = model.HasUnrevealedCard1 ? new CardViewModel(model.UnrevealedCard1) : new CardViewModel();
            _unrevealedCard2 = model.HasUnrevealedCard2 ? new CardViewModel(model.UnrevealedCard2) : new CardViewModel();
            _dimensions = model.HasDimensions ? new BoardDimensionsViewModel(model.Dimensions) : new BoardDimensionsViewModel();
            _inGameCardsSynced = new SyncedViewModelList<Card, CardViewModel>(model.InGameCards, m => new CardViewModel(m));
            Init();
        }

        public PlayBoardViewModel(PlayBoardViewModel other)
            : base(other)
        {
            _unrevealedCard1 = other.HasUnrevealedCard1 ? new CardViewModel(other.UnrevealedCard1) : new CardViewModel();
            _unrevealedCard2 = other.HasUnrevealedCard2 ? new CardViewModel(other.UnrevealedCard2) : new CardViewModel();
            _dimensions = other.HasDimensions ? new BoardDimensionsViewModel(other.Dimensions) : new BoardDimensionsViewModel();
            _inGameCardsSynced = new SyncedViewModelList<Card, CardViewModel>(new List<Card>(other.InGameCards.Models()), m => new CardViewModel(m));
            Init();
        }

        public void Init()
        {
            _defaultDimensions = GetDefaultDimensions();
            Dimensions = DefaultDimensions.FirstOrDefault();
        }

        private ReadOnlyObservableCollection<BoardDimensionsViewModel> GetDefaultDimensions()
        {
            ObservableCollection<BoardDimensionsViewModel> defaultDimensions = new ObservableCollection<BoardDimensionsViewModel>();
            foreach (string dimension in Properties.Settings.Default.DefaultDimensions)
            {
                defaultDimensions.Add(new BoardDimensionsViewModel(
                    new BoardDimensions(ushort.Parse(dimension.Split(';')[0]), ushort.Parse(dimension.Split(';')[1])))
                {
                    DisplayImage = dimension.Split(';')[2]
                });
            }
            return defaultDimensions.ToReadOnlyObservableCollection();
        }

        public void UnrevealCards()
        {
            Model.UnrevealCards();
            ResyncInGameCards();
            UnrevealedCards_PropertyChanged();
        }

        public void ResyncInGameCards()
        {
            _inGameCardsSynced.AdaptList(Model.InGameCards);
            OnPropertyChanged(() => this.InGameCards);
        }

        public void UnrevealedCards_PropertyChanged()
        {
            OnPropertyChanged(() => this.UnrevealedCard1);
            OnPropertyChanged(() => this.UnrevealedCard2);
            OnPropertyChanged(() => this.HasUnrevealedCard1);
            OnPropertyChanged(() => this.HasUnrevealedCard2);
            OnPropertyChanged(() => this.CanRevealCard);
            OnPropertyChanged(() => this.HasBothCardsUnrevealed);
            OnPropertyChanged(() => this.InGameRows);
        }
    }


}
