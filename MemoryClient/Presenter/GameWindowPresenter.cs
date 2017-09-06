using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfFundamentals.ViewModelBase;
using MemoryClient.Views;
using MemoryClient.ViewModels;
using MemoryGameLogicLib.Model;
using WpfFundamentals.Helper;

namespace MemoryClient.Presenter
{
	public class GameWindowPresenter : Presenter<GameWindow>
	{
		public DelegateCommand StartGameCommand { get; set; }
		public DelegateCommand StopGameCommand { get; set; }
		public DelegateCommand FinishRenamingCommand { get; set; }
		public DelegateCommand CloseWinnerWindowCommand { get; set; }

		public DelegateCommand<CardViewModel> RevealCardCommand { get; set; }

		public GameControllerViewModel Game { get; set; }

		public bool IsBoardBlocked
		{
			get { return _isBoardBlocked; }
			set
			{
				if (IsBoardBlocked != value)
				{
					_isBoardBlocked = value;
					OnPropertyChanged(() => this.IsBoardBlocked);
				}
			}
		} bool _isBoardBlocked;

		public bool IsDisplayingWinnerScreen
		{
			get { return _isDisplayingWinnerScreen; }
			set
			{
				if (IsDisplayingWinnerScreen != value)
				{
					_isDisplayingWinnerScreen = value;
					OnPropertyChanged(() => this.IsDisplayingWinnerScreen);
				}
			}
		}
		bool _isDisplayingWinnerScreen;

		bool WasGameAborted { get; set; }
				
		public GameWindowPresenter()
			: base()
		{
			InitLogic();
			InitView();			
			InitCommands();
			RegisterPropertyChanges();			
		}

		void InitLogic()
		{		
			Game = new GameControllerViewModel(new GameController());
		}

		void InitView()
		{
			this.View.Presenter = this;
		}

		void InitCommands()
		{
			StartGameCommand = new DelegateCommand(
				() => CanDoStartGame(),
				() => DoStartGame()
				);
			StopGameCommand = new DelegateCommand(
				() => CanDoStopGame(),
				() => DoStopGame()
				);
			CloseWinnerWindowCommand = new DelegateCommand(
				() => DoCloseWinnerWindow()
				);
			RevealCardCommand = new DelegateCommand<CardViewModel>(
				card => CanDoRevealCard(card),
				card => DoRevealCard(card));
		}
			
		void RegisterPropertyChanges()
		{
			Game.PropertyChanged += Game_PropertyChanged;
			Game.Board.PropertyChanged += Board_PropertyChanged;
			Game.PlayerHandler.PropertyChanged += PlayerHandler_PropertyChanged;
		}

		void Game_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName.Equals(Reflect.GetPropertyName(() => Game.IsGameRunning)))
			{
				if (!Game.IsGameRunning && !WasGameAborted)
					DisplayWinners();
			}
		}

		void PlayerHandler_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName.Equals(Reflect.GetPropertyName(() => Game.PlayerHandler.HasSelectedPlayers)))
			{
				StartGameCommand.RaiseCanExecuteChanged();
			}
		}

		void Board_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName.Equals(Reflect.GetPropertyName(() => Game.Board.Dimensions)))
			{
				StartGameCommand.RaiseCanExecuteChanged();							
			}
		}

		void DisplayWinners()
		{
			Game.PlayerHandler.Winners_PropertyChanged();
			IsDisplayingWinnerScreen = true;
		}

		private void DoCloseWinnerWindow()
		{
			Game.PlayerHandler.EmptyAllRacks();
			IsDisplayingWinnerScreen = false;
		}
			

		bool CanDoStartGame()
		{
			return Game.Board.Dimensions.HasValidDimensions && Game.PlayerHandler.HasSelectedPlayers;
		}

		void DoStartGame()
		{
			WasGameAborted = false;
			Game.LayCardsOnBoard();
			Game.StartGame();
		}

		bool CanDoStopGame()
		{
			return true;
		}

		void DoStopGame()
		{
			WasGameAborted = true;
			Game.StopGame();
		}
				
		bool CanDoRevealCard(CardViewModel card)
		{
			return card.HasInGamePosition && !card.IsRevealed;
		}

		void DoRevealCard(CardViewModel card)
		{
			Game.RevealCard(card.InGamePosition);

			if (Game.Board.HasBothCardsUnrevealed)
				CheckAndUnrevealDeferred();
		}

		void CheckAndUnrevealDeferred()
		{
			IsBoardBlocked = true;
			DeferredAction doDeferred = new DeferredAction(() =>
			{
				bool doMatch = Game.CheckIfCardsMatchAndDiscard();
				if (!doMatch)
				{
					Game.PlayerHandler.CurrentPlayer.IncreaseMissedCount();
					Game.PlayerHandler.NextTurn();					
				}
				Game.CheckIfGameIsOver();
				Game.Board.UnrevealCards();
				IsBoardBlocked = false;
			},
			App.Current.Dispatcher);
			doDeferred.Defer(GlobalParameter.UnrevealTimeSpan);
		}
	}
}
