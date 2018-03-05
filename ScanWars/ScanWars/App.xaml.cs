using ScanWars.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ScanWars
{
    public partial class App : Application
    {
        private IFacebookAuthenticationDelegate _facebookAuthenticationDelegate;
        public App()
        {
            InitializeComponent();

            MainPage = new Pages.Login.LoginPage(new ViewModels.Login.LoginViewModel(_facebookAuthenticationDelegate));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
