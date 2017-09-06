using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MemoryClient.Presenter;

namespace MemoryClient.Views
{
	/// <summary>
	/// Interaktionslogik für GameWindow.xaml
	/// </summary>
	public partial class GameWindow : Window
	{
		public GameWindow()
		{
			InitializeComponent();
		}

		#region IGameWindowView Member

		public GameWindowPresenter Presenter
		{
			get
			{
				return _presenter;
			}
			set
			{
				_presenter = value;
				this.DataContext = _presenter;
			}
		} private GameWindowPresenter _presenter;

		#endregion
	}
}
