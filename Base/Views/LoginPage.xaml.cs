using Base.ViewModels;

namespace Base.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{

		LoginViewModel vm;

		public LoginPage()
		{
			vm = new LoginViewModel();
			InitializeComponent();
			BindingContext = vm;
		}
		
		protected override void OnAppearing()
		{
			base.OnAppearing();
			animexBrincos();
		}

		protected override bool OnBackButtonPressed()
		{
			Shell.Current.GoToAsync("//Home/AboutPage");
			return true;
		}

		void lientek(Object e, EventArgs s)
		{
			animexBrincos();
		}

		async void animex()
		{
			btnLientek.Rotation = 0;
			await Task.WhenAny<bool>
			(
				btnLientek.ScaleTo(.80, 400),
				btnLientek.RotateTo(360, 700)
			);
			await btnLientek.ScaleTo(1, 400);
		}

		async void animexBrincos()
		{
			int a = 0;
			while (a<4)
			{
				await btnLientek.TranslateTo(0, 10, 50);
				await btnLientek.TranslateTo(0, -10, 50);
				a++;
			}
			
		}
	}
}
