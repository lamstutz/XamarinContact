using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
//using ImageCircle.Forms.Plugin.Droid;
using XLabs.Forms;

namespace MyContacts.Droid
{
    [Activity(Label = "Contacts", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : XFormsApplicationDroid
    {
        //: global::Xamarin.Forms.Platform.Android.FormsApplicationActivity 
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
          //  ImageCircleRenderer.Init();
            string path = FileAccessHelper.GetLocalFilePath("contacts.db3");
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
                Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                Window.SetStatusBarColor(Android.Graphics.Color.Argb(255,254, 142, 0));
            }
            LoadApplication(new App(path));
        }
    }
}

