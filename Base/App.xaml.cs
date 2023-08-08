using Base.Services;
using Base.Views;

namespace Base
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			Application.Current.UserAppTheme = AppTheme.Light;
			Application.Current.RequestedThemeChanged += (s, a) =>
			{
				Application.Current.UserAppTheme = AppTheme.Light;
			};
			//DependencyService.Register<MockDataStore>();
			DependencyService.Register<NavigationService>();
			//Routing.RegisterRoute(typeof(NotificacionesPage).FullName, typeof(NotificacionesPage));
			//Routing.RegisterRoute(typeof(FeedbackPage).FullName, typeof(FeedbackPage));
			//Routing.RegisterRoute(typeof(SettingsPage).FullName, typeof(SettingsPage));
			Routing.RegisterRoute(typeof(ResetearContraseñaPage).FullName, typeof(ResetearContraseñaPage));

			//Routing.RegisterRoute(typeof(AlumnoPage).FullName, typeof(AlumnoPage));
			//Routing.RegisterRoute(typeof(BLMPage).FullName, typeof(BLMPage));
			//Routing.RegisterRoute(typeof(CalendarioPage).FullName, typeof(CalendarioPage));
			//Routing.RegisterRoute(typeof(PagoPage).FullName, typeof(PagoPage));
			//Routing.RegisterRoute(typeof(ComidaPage).FullName, typeof(ComidaPage));

			MainPage = new MainPage();
		}

	}
}