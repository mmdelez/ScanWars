using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace ScanWars.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private bool _isBusy;
        private bool showActivityIndicator;

        public BaseViewModel()
        {
            
        }

        #region INotifyPropertyChanged

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RaiseAndUpdate<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            Raise(propertyName);
        }

        protected void Raise(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName) && PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public virtual Task InitAsync() => Task.Run(() => { });

        public INavigation Navigation { get; set; }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                RaiseAndUpdate(ref _isBusy, value);
                Raise(nameof(IsNotBusy));
            }
        }

        public bool IsNotBusy => !IsBusy;

        // in case you want to show the activity indicator but not set the entire viewmodel as "busy"
        public bool ShowActivityIndicator
        {
            get { return showActivityIndicator; }
            set
            {
                RaiseAndUpdate(ref showActivityIndicator, value);
                Raise(nameof(HideActivityIndicator));
            }
        }

        public bool HideActivityIndicator => !ShowActivityIndicator;

        /// <summary>
        /// push a page to the stack and lock the command to avoid the multiple tap
        /// </summary>
        /// <param name="page"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task PushAsync(Page page, ICommand command)
        {
            lock (command)
            {
                if (IsBusy) return;
                else IsBusy = true;
            }
            Debug.WriteLine($"PushAsync {page.GetType().Name}");
            await Navigation.PushAsync(page);
            IsBusy = false;
        }

        /// <summary>
        /// pop the given page from the stack and lock the command to avoid the multiple tap
        /// </summary>
        /// <param name="page"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<Page> PopAsync(Type page, ICommand command)
        {
            var returnPage = (Page)null;
            lock (command)
            {
                if (IsBusy) return returnPage;
                else IsBusy = true;
            }
            if (this.Navigation.ModalStack.FirstOrDefault(x => x.GetType() == page) == null)
                returnPage = await Navigation.PopAsync();
            else
                returnPage = await Navigation.PopModalAsync();
            IsBusy = false;
            return returnPage;
        }

    }
}
