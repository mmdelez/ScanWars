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
        private FacebookUser _user;
        private IFacebookService _facebookService;

        public HomePageViewModel(FacebookUser user)
        {
            _facebookService = DependencyService.Get<IFacebookService>();
            _user = user;
            LogoutCommand = new Command(OnLogoutButtonTapped);
        }

        public FacebookUser FacebookUser
        {
            get { return _user; }
            set { RaiseAndUpdate(ref _user, value); }
        }

        public ICommand LogoutCommand { get; set; }

        private void OnLogoutButtonTapped()
        {
            _facebookService?.Logout();
            App.Current.MainPage = new Pages.Login.LoginPage(new Login.LoginViewModel());
        }
    }
}
