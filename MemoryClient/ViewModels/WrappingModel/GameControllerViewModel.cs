using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemoryGameLogicLib.Model;
using WpfFundamentals.ViewModelBase;
using WpfFundamentals.Helper;
using System.Collections.ObjectModel;
using MemoryGameLogicLib.DataReader;

namespace MemoryClient.ViewModels
{
    public class GameControllerViewModel : ViewModel<GameController>
    {
        public PlayBoardViewModel Board
        {
            get { return _board; }
            set
            {
                if (Board != value)
                {
                    _board = value;
                    Model.Board = _board.Model;
                    OnPropertyChanged(() => this.Board);
                }
            }
        }
        PlayBoardViewModel _board;

        public bool HasBoard
        {
            get { return this.Model.HasBoard; }
        }

        public bool IsGameRunning
        {
            get { return this.Model.IsGameRunning; }
        }

        public PlayerHandlerViewModel PlayerHandler
        {
            get { return _playerHandler; }
            set
            {
                if (PlayerHandler != value)
                {
                    _playerHandler = value;
                    Model.PlayerHandler = _playerHandler.Model;
                    OnPropertyChanged(() => this.PlayerHandler);
                }
            }
        }
        PlayerHandlerViewModel _playerHandler;

        public DeckDescriptionViewModel Deck
        {
            get { return _deck; }
            set
            {
                if (Deck != value)
                {
                    _deck = value;
                    Model.Deck = _deck.Model;
                    OnPropertyChanged(() => this.Deck);
                }
            }
        }
        DeckDescriptionViewModel _deck;

        public ObservableCollection<DeckDescriptionViewModel> Decks
        {
            get { return _decks.ObservableCollection; }
            //set {
            //    if (DeckNames != value)
            //    {
            //        _deckNames = value;
            //        OnPropertyChanged(() => this.DeckNames);
            //    }
            //}
        } SyncedViewModelList<DeckDescription, DeckDescriptionViewModel> _decks;

        public GameControllerViewModel()
            : base(new GameController())
        {
            _playerHandler = new PlayerHandlerViewModel();
            _board = new PlayBoardViewModel();
            _decks = new SyncedViewModelList<DeckDescription, DeckDescriptionViewModel>(new List<DeckDescription>(),
                model => new DeckDescriptionViewModel(model));
            _deck = new DeckDescriptionViewModel();
            Init();
        }

        public GameControllerViewModel(GameController model)
            : base(model)
        {
            _playerHandler = new PlayerHandlerViewModel(model.PlayerHandler);
            _board = new PlayBoardViewModel(model.Board);
            _decks = new SyncedViewModelList<DeckDescription, DeckDescriptionViewModel>(model.Decks,
                m => new DeckDescriptionViewModel(m));
            _deck = (from deck in Decks where deck.Equals(model.Deck) select deck).FirstOrDefault();
            Init();
        }

        public GameControllerViewModel(GameControllerViewModel other)
            : base(other)
        {
            _playerHandler = new PlayerHandlerViewModel(other.PlayerHandler);
            _board = new PlayBoardViewModel(other.Board);
            _decks = new SyncedViewModelList<DeckDescription, DeckDescriptionViewModel>(
                new List<DeckDescription>(other.Decks.Models()),
                m => new DeckDescriptionViewModel(m));
            _deck = (from deck in Decks where deck.Equals(other.Deck) select deck).FirstOrDefault();
            Init();
        }

        void Init()
        { }

        public void StartGame()
        {
			PlayerHandler.EmptyAllRacks();
            Model.StartGame(PlayerHandler.SelectedPlayers.Models().ToList());
            PlayerHandler.ResyncPlayers();
            GameStatus_PropertyChanged();
        }

        public void RevealCard(PositionViewModel position)
        {
            Model.RevealCard(position.Model);
            Board.ResyncInGameCards();
            Board.UnrevealedCards_PropertyChanged();
        }

        public void LayCardsOnBoard()
        {
            Model.LayCardsOnBoard();
            Board.ResyncInGameCards();
            Board_PropertyChanged();
        }

        public bool CheckIfCardsMatchAndDiscard()
        {
            bool doMatch = Model.CheckIfCardsMatchAndDiscard();
            PlayerHandler.CurrentPlayer.Rack.ResyncDiscardedCards();
            return doMatch;
        }

        public bool CheckIfGameIsOver()
        {
            bool isOver = Model.CheckIfGameIsOver();
            GameStatus_PropertyChanged();
            return isOver;
        }

        void GameStatus_PropertyChanged()
        {
            OnPropertyChanged(() => this.IsGameRunning);
        }

        void Board_PropertyChanged()
        {
            OnPropertyChanged(() => this.Board);
            OnPropertyChanged(() => this.HasBoard);
        }

        public void StopGame()
        {
            Model.StopGame();
            GameStatus_PropertyChanged();
            Board.UnrevealedCards_PropertyChanged();
        }
    }
}
