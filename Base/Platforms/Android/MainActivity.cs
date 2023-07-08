using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
//using Firebase;

namespace Base
{
	[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
	public class MainActivity : MauiAppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			Window.SetFlags(WindowManagerFlags.Secure, WindowManagerFlags.Secure);
			base.OnCreate(savedInstanceState);

			const int requestNotification = 0;
			string[] notiPermission = { Manifest.Permission.PostNotifications  };

			if ((int)Build.VERSION.SdkInt < 33) return;
			if (this.CheckSelfPermission(Manifest.Permission.PostNotifications) != Permission.Granted)
			{
				this.RequestPermissions(notiPermission, requestNotification);
			}
		}
	}
}
