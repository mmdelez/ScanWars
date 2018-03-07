using ScanWars.ViewModels.Scanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace ScanWars.Pages.Scanner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerPage : BasePage
    {
        private ScannerPageViewModel _scannerPageViewModel;
        private ZXingScannerView _zxingScannerView;

        public ScannerPage(ScannerPageViewModel scannerPageViewModel) : base(scannerPageViewModel)
        {
            InitializeComponent();
            _scannerPageViewModel = scannerPageViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (!zxingContainer.Children.Any(c => c is ZXingScannerView))
            {
                _zxingScannerView = new ZXingScannerView();
                _zxingScannerView.BindingContext = _scannerPageViewModel;
                _zxingScannerView.SetBinding(ZXingScannerView.IsScanningProperty, new Binding("IsScanning"));
                _zxingScannerView.SetBinding(ZXingScannerView.IsAnalyzingProperty, new Binding("IsAnalyzing"));
                _zxingScannerView.SetBinding(ZXingScannerView.ResultProperty, new Binding("Result", BindingMode.TwoWay));
                _zxingScannerView.SetBinding(ZXingScannerView.ScanResultCommandProperty, new Binding("QRScanResultCommand"));
                zxingContainer.Children.Add(_zxingScannerView);
            }
        }
    }
}