using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScanWars.ViewModels.Main;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScanWars.Pages.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : BasePage
    {
        public HomePage(HomePageViewModel homePageViewModel) : base(homePageViewModel)
        {
            InitializeComponent();
        }

        // disable android back button
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}