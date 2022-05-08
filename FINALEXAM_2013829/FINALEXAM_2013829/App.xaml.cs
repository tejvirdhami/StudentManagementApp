using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FINALEXAM_2013829
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            //Connection singleton will be initialized here
            DataSource.Init();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}