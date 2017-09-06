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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MemoryClient.Presenter;

namespace MemoryClient.Views
{
	/// <summary>
	/// Interaktionslogik für Victory.xaml
	/// </summary>
	public partial class Victory : UserControl
	{
		public Victory()
		{
			InitializeComponent();
		}

		public GameWindowPresenter Presenter
		{
			get { return (GameWindowPresenter)GetValue(PresenterProperty); }
			set { SetValue(PresenterProperty, value); }
		}

		public static readonly DependencyProperty PresenterProperty =
			DependencyProperty.Register("Presenter", typeof(GameWindowPresenter), typeof(Victory), new UIPropertyMetadata(null));
	}
}
