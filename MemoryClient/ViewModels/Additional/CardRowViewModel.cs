using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfFundamentals.ViewModelBase;
using System.Collections.ObjectModel;
using MemoryGameLogicLib.Model;

namespace MemoryClient.ViewModels
{
	public class CardRowViewModel : ViewModel
	{
		public ObservableCollection<CardViewModel> InGameCardsColumn
		{
			get { return _inGameCardsColumn; }
			set
			{
				if (InGameCardsColumn != value)
				{
					_inGameCardsColumn = value;
					OnPropertyChanged(() => this.InGameCardsColumn);
				}
			}
		} ObservableCollection<CardViewModel> _inGameCardsColumn;

		public ushort RowNo
		{
			get { return _rowNo; }
			set
			{
				if (RowNo != value)
				{
					_rowNo = value;
					OnPropertyChanged(() => this.RowNo);
				}
			}
		} ushort _rowNo;


		public CardRowViewModel(PlayBoardViewModel parent, ushort currentRowNo)
		{
			SetColumns(parent, currentRowNo);
			RowNo = currentRowNo;
		}

		void SetColumns(PlayBoardViewModel parent, ushort currentRowNr)
		{
			this.InGameCardsColumn = new ObservableCollection<CardViewModel>();
			for (ushort column = 0; column < parent.Dimensions.Width; column++)
			{
				var inGameCard = (from card in parent.InGameCards
								  where card.HasInGamePosition
								  where card.InGamePosition.YPos == currentRowNr
								  where card.InGamePosition.XPos == column
								  select card).FirstOrDefault();
				if (inGameCard == null)
					inGameCard = new EmptyCardViewModel(new PositionViewModel(new Position(column, currentRowNr)));
				InGameCardsColumn.Add(inGameCard);
			}
		}
	}
}
