using DevExpress.Maui.CollectionView;
using Base.ViewModels;

namespace Base.Views
{
	public partial class MainPage : Shell
	{

		MainViewModel vm;
		public MainPage()
		{
			InitializeComponent();
			vm = new MainViewModel();
			BindingContext = vm;

		}

		protected override void OnAppearing()
		{
			if (vm.Logged)
			{
				Shell.Current.GoToAsync("//AboutPage");
			}
			base.OnAppearing();
		}

		
		public async void OpenFeedBack(Object obj, EventArgs eventArgs)
		{
			this.FlyoutIsPresented = false;
			if (!vm.Pressed)
			{
				vm.Pressed = true;
				await Navigation.PushModalAsync(new FeedbackPage());
				vm.Pressed = false;
			}			
		}

		public async void OpenNotificaciones(Object obj, EventArgs eventArgs)
		{
			this.FlyoutIsPresented = false;
			if (!vm.Pressed)
			{
				vm.Pressed = true;
				await Navigation.PushModalAsync(new NotificacionesPage());
				vm.Pressed = false;
			}
		}

		public void CloseMenu(Object obj, EventArgs eventArgs)
		{
			this.FlyoutIsPresented = false;
		}

		
	}
}