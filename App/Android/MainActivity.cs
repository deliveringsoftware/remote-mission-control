using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Plugin.CurrentActivity;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace AzureDevops.Droid
{
    [Activity(Label = "Azure Devops App",
              Icon = "@mipmap/icon",
              Theme = "@style/MainTheme",
              MainLauncher = false,
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Forms.SetFlags("Shell_Experimental",
                           "Visual_Experimental",
                           "CollectionView_Experimental",
                           "FastRenderers_Experimental");

            Forms.Init(this, bundle);
            Platform.Init(this, bundle);
            FormsMaterial.Init(this, bundle);
            CrossCurrentActivity.Current.Init(this, bundle);
            UserDialogs.Init(this);

            LoadApplication(new App());
            ConfigureAndroidSpecific();
        }

        public override void OnRequestPermissionsResult(int requestCode,
                                                        string[] permissions,
                                                        [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void ConfigureAndroidSpecific()
        {
            var android = Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>();
            android.UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Pan);
        }
    }
}