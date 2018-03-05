using ScanWars.Interfaces;
using ScanWars.Models;
using System;
using System.Collections.Generic;
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
        private OAuth2Authenticator _authenticator;
        private IFacebookAuthenticationDelegate _authenticationDelegate;

        public LoginViewModel(IFacebookAuthenticationDelegate authenticationDelegate)
        {
            _authenticationDelegate = authenticationDelegate;

            #region Google Auth
            //var authenticator = new OAuth2Authenticator(
            //                        "789305869203-akr5j5oh0d54r7tqsko9qp25ilpktnse.apps.googleusercontent.com",
            //                        null,
            //                        "https://www.googleapis.com/auth/plus.login",
            //                        new Uri(Constants.AuthorizeUrl),
            //                        new Uri("https://console.developers.google.com/apis/credentials?project=applied-abbey-111204"),
            //                        new Uri(Constants.AccessTokenUrl),
            //                        null,
            //                        true);
            #endregion

            #region Facebook Auth
            _authenticator = new OAuth2Authenticator(
                                    clientId: "185501372058546",
                                    scope: "email",
                                    authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                                    redirectUrl: new Uri("https://www.facebook.com/connect/login_success.html"),
                                    // switch for new Native UI API
                                    //      true = Android Custom Tabs and/or iOS Safari View Controller
                                    //      false = Embedded Browsers used (Android WebView, iOS UIWebView)
                                    //  default = false  (not using NEW native UI)
                                    isUsingNativeUI: true);

            _authenticator.Completed += OnAuthCompleted;
            _authenticator.Error += OnAuthError;

            #endregion

            LoginButtonTappedCommand = new Command(OnLoginButtonTapped);
        }

        public ICommand LoginButtonTappedCommand { get; set; }

        private void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                var token = new FacebookOAuthToken
                {
                    AccessToken = e.Account.Properties["access_token"]
                };
                _authenticationDelegate.OnAuthenticationCompleted(token);
            }
            else
            {
                _authenticationDelegate.OnAuthenticationCanceled();
            }
        }

        private void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            _authenticationDelegate.OnAuthenticationFailed(e.Message, e.Exception);
        }

        private void OnLoginButtonTapped()
        {
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(_authenticator);
        }
    }
}
