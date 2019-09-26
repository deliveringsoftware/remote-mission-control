using Android.App;
using Android.Content.PM;
using Android.OS;

namespace AzureDevops.Droid
{
    [Activity(Label = "Azure Devops App",
              Theme = "@style/Theme.Splash",
              Icon = "@mipmap/icon",
              ConfigurationChanges = ConfigChanges.Orientation,
              ScreenOrientation = ScreenOrientation.Portrait,
              MainLauncher = true,
              NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
        }
    }
}