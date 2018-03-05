using ScanWars.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ScanWars.Pages
{
    public abstract class BasePage : ContentPage
    {
        private BaseViewModel _viewModel;

        protected BasePage(BaseViewModel viewModel)
        {

            // set the ViewModel
            BindingContext = ViewModel = viewModel;
            ViewModel.Navigation = this.Navigation;

            Task.Run(async () => await ViewModel?.InitAsync());
        }

        protected BasePage()
        {

        }

        public BaseViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        //public void SendAppearing()
        //{
        //    OnAppearing();
        //}

        //public void SendDisappearing()
        //{
        //    OnDisappearing();
        //}
    }
}
