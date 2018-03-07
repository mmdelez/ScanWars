using ScanWars.Models;
using ScanWars.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ScanWars.ViewModels.Main
{
    public class HomePageViewModel : BaseViewModel
    {
        private User _user;
        private IFacebookService _facebookService;

        public HomePageViewModel(User user)
        {
            _facebookService = DependencyService.Get<IFacebookService>();
            _user = user;
            ScanButtonCommand = new Command(OnScanButtonTapped);
            LogoutCommand = new Command(OnLogoutButtonTapped);
        }

        public User User
        {
            get { return _user; }
            set { RaiseAndUpdate(ref _user, value); }
        }

        public ICommand ScanButtonCommand { get; set; }
        public ICommand LogoutCommand { get; set; }

        private async void OnScanButtonTapped()
        {
            await Navigation.PushAsync(new Pages.Scanner.ScannerPage(new Scanner.ScannerPageViewModel(User)));
        }

        private void OnLogoutButtonTapped()
        {
            _facebookService?.Logout();
            App.Current.MainPage = new Pages.Login.LoginPage(new Login.LoginViewModel());
        }
    }
}
