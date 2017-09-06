using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using MemoryClient.Views;
using MemoryClient.Presenter;

namespace MemoryClient
{
	/// <summary>
	/// Interaktionslogik für "App.xaml"
	/// </summary>
	public partial class App : Application
	{
		GameWindowPresenter GameWindow { get; set; }

		public App()
		{
			this.ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			GameWindow = new GameWindowPresenter();
			this.MainWindow = GameWindow.View;
			this.MainWindow.Show();
		}

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
		}
	}
}
