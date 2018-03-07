using System;
using System.Collections.Generic;
using System.Linq;
using Facebook.CoreKit;
using Foundation;
using ScanWars.iOS.Services;
using ScanWars.Services.Interfaces;
using UIKit;
using Xamarin.Forms;

namespace ScanWars.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            InitializeServices();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            base.OnActivated(uiApplication);
            #region FacebookService
            AppEvents.ActivateApp();
            #endregion
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            #region FacebookService
            return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
            #endregion
        }

        private void InitializeServices()
        {
            #region Facebook
            DependencyService.Register<IFacebookService, FacebookService>();
            #endregion
        }
    }
}
