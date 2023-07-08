using Base.Models;
using System.Windows.Input;

namespace Base.ViewModels
{
	public class MainViewModel : BaseViewModel
	{
		public MainViewModel() {
			Logged = Preferences.Get("Logged", false);
			ChecarVersion();
			if (string.IsNullOrWhiteSpace(User))
			{
				User = "Bienvenido";
			}

			CommandInstagram = new Command(OnClickInstagram);
		}	
		
		public ICommand CommandInstagram { get; }

		async void OnClickInstagram()
		{
			await Browser.OpenAsync("https://www.instagram.com/");
			ShowPopUp = false;
		}
	}
}
