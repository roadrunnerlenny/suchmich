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
using System.Collections.ObjectModel;
using MemoryClient.ViewModels;

namespace MemoryClient.Views
{
    /// <summary>
    /// Interaktionslogik für PlayerInfo.xaml
    /// </summary>
    public partial class PlayerInfo : UserControl
    {
        public PlayerInfo()
        {
            InitializeComponent();
        }

        public GameWindowPresenter Presenter
        {
            get { return (GameWindowPresenter)GetValue(PresenterProperty); }
            set { SetValue(PresenterProperty, value); }
        }

        public static readonly DependencyProperty PresenterProperty =
            DependencyProperty.Register("Presenter", typeof(GameWindowPresenter), typeof(PlayerInfo), new UIPropertyMetadata(null));
    }
}
