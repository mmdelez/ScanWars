using ScanWars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ScanWars.ViewModels.Scanner
{
    public class ScannerPageViewModel : BaseViewModel
    {
        private bool _isAnalyzing = true;
        private bool _isScanning = true;
        private bool _cameraAccessGranted;

        public ScannerPageViewModel(User user)
        {
            CameraAccessGranted = true;
            BarcodeScanResultCommand = new Command(OnBarcodeScanResult);
        }

        public ICommand BarcodeScanResultCommand { get; set; }

        public ZXing.Result Result { get; set; }

        public bool IsAnalyzing
        {
            get { return _isAnalyzing; }
            set { RaiseAndUpdate(ref _isAnalyzing, value); }
        }

        public bool IsScanning
        {
            get { return _isScanning; }
            set { RaiseAndUpdate(ref _isScanning, value); }
        }

        public bool CameraAccessGranted
        {
            get { return _cameraAccessGranted; }
            set { RaiseAndUpdate(ref _cameraAccessGranted, value); }
        }

        private void OnBarcodeScanResult()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                IsAnalyzing = false;
                IsScanning = false;
            });
        }
    }
}
