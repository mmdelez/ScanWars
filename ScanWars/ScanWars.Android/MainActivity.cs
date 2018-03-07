﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Facebook;
using Android.Content;
using Xamarin.Forms;
using ScanWars.Services.Interfaces;
using ScanWars.Droid.Services;

namespace ScanWars.Droid
{
    [Activity(Label = "ScanWars",
        Icon = "@drawable/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        LaunchMode = LaunchMode.SingleTop)]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            //FacebookSdk.SdkInitialize(this);
            //global::Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            InitializeServices();
            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            var service = DependencyService.Get<IFacebookService>();
            if (service != null)
            {
                (service as FacebookService).CallbackManager.OnActivityResult(requestCode, (int)resultCode, data);
            }
        }

        private void InitializeServices()
        {
            #region Facebook
            DependencyService.Register<IFacebookService, FacebookService>();
            #endregion
        }
    }
}

