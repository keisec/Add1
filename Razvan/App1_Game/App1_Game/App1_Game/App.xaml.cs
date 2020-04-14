using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1_Game.Services;
using App1_Game.Views;

namespace App1_Game
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
