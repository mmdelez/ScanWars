using ScanWars.Models;
using ScanWars.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Auth;
using Xamarin.Forms;

namespace ScanWars.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel
    {
        private string _message;
        private bool _isLoggedIn;
        private FacebookUser _user;
        private IFacebookService _facebookService;

        public LoginViewModel()
        {
            _facebookService = DependencyService.Get<IFacebookService>();

            LoginButtonTappedCommand = new Command(OnLoginButtonTapped);
        }

        public ICommand LoginButtonTappedCommand { get; set; }

        public string Message
        {
            get { return _message; }
            set { RaiseAndUpdate(ref _message, value); }
        }

        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set { RaiseAndUpdate(ref _isLoggedIn, value); }
        }

        public FacebookUser User
        {
            get { return _user; }
            set { RaiseAndUpdate(ref _user, value); }
        }

        private void OnLoginButtonTapped()
        {
            _facebookService?.Login(OnLoginCompleted);
        }

        private void OnLoginCompleted(FacebookUser user, Exception exception)
        {
            if (exception == null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    User = user;
                    IsLoggedIn = true;
                    App.Current.MainPage = new NavigationPage(new Pages.Main.HomePage(new Main.HomePageViewModel(_user)));
                });
            }
            else
            {
                Debug.WriteLine("Error: " + exception.Message);
                App.Current.MainPage.DisplayAlert("Error", exception.Message, "OK");
            }
        }
    }
}
