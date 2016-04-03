using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using LojaMobile.Helpers;
using PushNotification.Plugin;

namespace LojaMobile.Droid
{
    [Activity(Label = "LojaMobile", Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            CrossPushNotification.Initialize<CrossPushNotificationListener>("910326547168");

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());


            //CrossPushNotification.Current.Register();
        }
    }
}

