using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using MemoryGameLogicLib.Model;
using WpfFundamentals.ViewModelBase;
using WpfFundamentals.Helper;
using System.Collections.Specialized;

namespace MemoryClient.ViewModels
{
	public class PlayerHandlerViewModel : ViewModel<PlayerHandler>
	{
		public ObservableCollection<PlayerViewModel> AllPlayers
		{
			get { return _allPlayersSynced.ObservableCollection; }
		}
		SyncedViewModelList<Player, PlayerViewModel> _allPlayersSynced;

		public PlayerViewModel CurrentPlayer
		{
			get
			{
				if (HasAllPlayers)
					return (from player in AllPlayers where player.Model == Model.CurrentPlayer select player).FirstOrDefault();
				else
					return null;
			}			
		}

		public bool HasCurrentPlayer
		{
			get { return Model.HasCurrentPlayer; }
		}

		public bool HasAllPlayers
		{
			get { return Model.HasAllPlayers; }
		}

		public ObservableCollection<PlayerViewModel> DefaultPlayers
		{
			get { return _defaultPlayers; }
		} ObservableCollection<PlayerViewModel> _defaultPlayers;

		public ReadOnlyObservableCollection<PlayerViewModel> SelectedPlayers
		{
			get { return (from player in this.DefaultPlayers where player.IsSelected select player).ToReadOnlyObservableCollection(); }
		}

		public bool HasSelectedPlayers
		{
			get { return SelectedPlayers != null && SelectedPlayers.Count > 0; }
		}

		public ObservableCollection<PlayerViewModel> Winners
		{
			get
			{
				if (HasAllPlayers)
				{
					int winningAmount = this.AllPlayers.Max(player => player.Rack.NumberOfSets);
					return (from player in this.AllPlayers where player.Rack.NumberOfSets == winningAmount select player).ToObservableCollection();
				}
				else
					return null;
			}
		}

		public PlayerHandlerViewModel()
			: base(new PlayerHandler())
		{
			_allPlayersSynced = new SyncedViewModelList<Player, PlayerViewModel>(new List<Player>(), m => new PlayerViewModel(m));
			Init();
		}

		public PlayerHandlerViewModel(PlayerHandler model)
			: base(model)
		{
			_allPlayersSynced = new SyncedViewModelList<Player, PlayerViewModel>(model.AllPlayers, m => new PlayerViewModel(m));
			Init();
		}

		public PlayerHandlerViewModel(PlayerHandlerViewModel other)
			: base(other)
		{
			_allPlayersSynced = new SyncedViewModelList<Player, PlayerViewModel>(new List<Player>(other.AllPlayers.Models()), m => new PlayerViewModel(m));
			Init();
		}

		void Init()
		{
			_defaultPlayers = GetDefaultPlayers();
			RegisterPropertyChanges_DefaultPlayers();
			OnPropertyChanged(() => this.DefaultPlayers);
		}

		private void RegisterPropertyChanges_DefaultPlayers()
		{
			_defaultPlayers.Apply(playerVM => playerVM.PropertyChanged += playerVM_PropertyChanged);
		}

		void playerVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			PlayerViewModel player = sender as PlayerViewModel;
			if (player != null && e.PropertyName.Equals(Reflect.GetPropertyName(()=>player.IsSelected))) {
				SelectedPlayers_PropertyChanged();
			}
		}
		
		ObservableCollection<PlayerViewModel> GetDefaultPlayers()
		{
            ObservableCollection<PlayerViewModel> defaultPlayers = new ObservableCollection<PlayerViewModel>();
            foreach (string playerName in Properties.Settings.Default.DefaultPlayersNames)
            {
                defaultPlayers.Add(new PlayerViewModel(new Player(playerName.Split(';')[0]))
                {
                    AvatarFileName = playerName.Split(';')[1]
                });
            }
			defaultPlayers.First().IsSelected = true;
			return defaultPlayers;
		}

		public void ResyncPlayers()
		{
		    _allPlayersSynced.AdaptList(Model.AllPlayers.ToList());
			SetCurrentPlayer();
		    AllPropertiesChanged();
		}

		public void CurrentPlayer_PropertyChanged()
		{
			OnPropertyChanged(() => this.CurrentPlayer);
			OnPropertyChanged(() => this.HasCurrentPlayer);			
		}

		public void SelectedPlayers_PropertyChanged()
		{
			OnPropertyChanged(() => this.SelectedPlayers);
			OnPropertyChanged(() => this.HasSelectedPlayers);
		}

		public void NextTurn()
		{
			Model.NextTurn();
			SetCurrentPlayer();
			CurrentPlayer_PropertyChanged();
		}

		void SetCurrentPlayer()
		{
			if (HasAllPlayers)
			{
				AllPlayers.Apply(
					player =>
					{
						if (HasCurrentPlayer && player == CurrentPlayer)
							player.IsCurrent = true;
						else
							player.IsCurrent = false;
					});
			}
		}

		public void EmptyAllRacks()
		{
			AllPlayers.Apply(player =>
			{
				player.Clear();
			});
		}

		public void Winners_PropertyChanged()
		{
			OnPropertyChanged(() => this.Winners);
		}
	}
}
