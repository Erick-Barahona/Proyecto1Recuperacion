using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Proyecto1
{
    public partial class App : Application
    {
        public static string UBICACIONDB = string.Empty;
        public App()
        {
            InitializeComponent();

            if ((Preferences.Get("usuario", "") != "") && (Preferences.Get("password", "") != ""))
            {

                MainPage = new NavigationPage(new MainPage());



            }
            else
            {
                MainPage = new NavigationPage(new LoginUser());
            }
        }
        public App(string dblocal)
        {

            InitializeComponent();
            UBICACIONDB = dblocal;
            if ( (Preferences.Get("usuario", "") != "") && (Preferences.Get("password", "") != ""))
            {

                MainPage = new NavigationPage(new MainPage());

              

            }
            else
            {
                MainPage = new NavigationPage(new LoginUser());
            }
           

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
