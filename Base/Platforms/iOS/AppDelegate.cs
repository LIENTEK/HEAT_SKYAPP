using Foundation;
using UIKit;

namespace Base
{
	[Register("AppDelegate")]
	public class AppDelegate : MauiUIApplicationDelegate
	{
		protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

		public override void OnResignActivation(UIApplication application)
		{
			var view = new UIView(Window.Frame)
			{
				Tag = new nint(101),
				BackgroundColor = UIColor.White
			};

			Window.AddSubview(view);
			Window.BringSubviewToFront(view);
		}

		// Remove window hiding app content when app is resumed
		public override void OnActivated(UIApplication application)
		{
			var view = Window.ViewWithTag(new nint(101));
			view?.RemoveFromSuperview();
		}
	}

}