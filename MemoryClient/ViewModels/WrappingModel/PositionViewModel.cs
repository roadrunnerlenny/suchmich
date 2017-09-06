using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemoryGameLogicLib.Model;
using WpfFundamentals.ViewModelBase;

namespace MemoryClient.ViewModels
{
	public class PositionViewModel : ViewModel<Position>, IEquatable<PositionViewModel>, IEquatable<Position>
	{
		public ushort? XPos
		{
			get { return Model.XPos; }
			set
			{
				if (XPos != value)
				{
					Model.XPos = value;
					OnPropertyChanged(() => this.XPos);
				}
			}
		}

		public ushort? YPos
		{
			get { return Model.YPos; }
			set
			{
				if (YPos != value)
				{
					Model.YPos = value;
					OnPropertyChanged(() => this.YPos);
				}
			}
		}

		public PositionViewModel()
			: base(new Position(0, 0))
		{ }

		public PositionViewModel(Position model)
			: base(model)
		{ }

		public PositionViewModel(PositionViewModel other) : base(other)
		{ }

		public override string ToString()
		{
			return Model.ToString();
		}

		public bool Equals(PositionViewModel other)
		{
			if (other != null)
				return other.Model.Equals(this.Model);
			else
				return false;
		}

		#region IEquatable<Position> Member

		public bool Equals(Position other)
		{
			if (other != null)
				return other.Equals(this.Model);
			else
				return false;
		}

		#endregion
	}
}
