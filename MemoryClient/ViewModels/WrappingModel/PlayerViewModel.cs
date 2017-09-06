using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemoryGameLogicLib.Model;
using WpfFundamentals.ViewModelBase;

namespace MemoryClient.ViewModels
{
	public class PlayerViewModel : ViewModel<Player>
	{
		public CardRackViewModel Rack
		{
			get { return _rack; }
			set
			{
				if (Rack != value)
				{
					_rack = value;
					Model.Rack = _rack.Model;
					OnPropertyChanged(() => this.Rack);
				}
			}
		}
		CardRackViewModel _rack;

		public string Name
		{
			get { return Model.Name; }
			set
			{
				if (Name != value)
				{
					Model.Name = value;
					OnPropertyChanged(() => this.Name);
				}
			}
		}

        public string AvatarFileName
        {
            get { return Model.AvatarFileName; }
            set 
            {
                if (AvatarFileName != value)
                {
                    Model.AvatarFileName = value;
                    OnPropertyChanged(() => this.AvatarFileName);
                }
            }
        }

		public bool IsSelected
		{
			get { return _isSelected; }
			set
			{
				if (this.IsSelected != value)
				{
					_isSelected = value;
					OnPropertyChanged(() => this.IsSelected);
				}
			}
		}
		bool _isSelected;

		public bool IsCurrent
		{
			get { return _isCurrent; }
			set
			{
				if (IsCurrent != value)
				{
					_isCurrent = value;
					OnPropertyChanged(() => this.IsCurrent);
				}
			}
		}
		bool _isCurrent;

		public int MissCount
		{
			get { return Model.MissCount; }
			set
			{
				if (MissCount != value)
				{
					Model.MissCount = value;
					OnPropertyChanged(() => this.MissCount);
				}
			}
		}

		public PlayerViewModel()
			: base(new Player(String.Empty))
		{
			_rack = new CardRackViewModel();
		}

		public PlayerViewModel(Player model)
			: base(model)
		{
			_rack = model != null ? new CardRackViewModel(model.Rack) : new CardRackViewModel();
		}

		public PlayerViewModel(PlayerViewModel other)
			: base(other)
		{
			_rack = other != null ? new CardRackViewModel(other.Rack) : new CardRackViewModel();
		}

		public override string ToString()
		{
			return Model.ToString();
		}

		internal void Clear()
		{
			this.Rack.Clear();
			this.MissCount = 0;
		}

		public void IncreaseMissedCount()
		{
			this.MissCount++;
		}
	}
}
