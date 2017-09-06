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
using System.Diagnostics;

namespace MemoryClient.Views
{
    /// <summary>
    /// Interaction logic for Info.xaml
    /// </summary>
    public partial class Info : UserControl
    {
        public Info()
        {
            InitializeComponent();
        }

        public GameWindowPresenter Presenter
        {
            get { return (GameWindowPresenter)GetValue(PresenterProperty); }
            set { SetValue(PresenterProperty, value); }
        }

        public static readonly DependencyProperty PresenterProperty =
            DependencyProperty.Register("Presenter", typeof(GameWindowPresenter), typeof(Info), new UIPropertyMetadata(null));

		private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
		{
			 Hyperlink hl = (Hyperlink)sender;  
			 string navigateUri = hl.NavigateUri.ToString();  
			 Process.Start(new ProcessStartInfo(navigateUri));  
			 e.Handled = true;  
		}

    }
}
