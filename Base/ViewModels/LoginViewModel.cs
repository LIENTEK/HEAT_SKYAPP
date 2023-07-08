using Base.Models;
using System.Windows.Input;

namespace Base.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		public LoginViewModel()
		{
			LoggedAccount();
			ChecarVersion();
			

			ShowPopUpCommand = new Command(PopUpVisible);
			ResetTextBoxUserCommand = new Command(x => ErrorUser = false);
			ResetTextBoxPasswordCommand = new Command(x => ErrorPassword = false);
			LoginCommand = new Command(OnLoginClicked);
			LogOutCommand = new Command(OnLogOutClicked);
			NuevoRegistroCommand = new Command(OnNuevoRegistroClicked, BlockButton);
			NewPasswordComand = new Command(OnNewPasswordClick, BlockButton);
			OpenFbCommand = new Command(Facebook);
			OpenCallCommand = new Command(Call);
			OpenCorreoCommand = new Command(Correo);
			OpenWebCommand = new Command(WebPage);
			PropertyChanged +=
				(_, __) => LoginCommand.ChangeCanExecute();

		}

		#region variables
		bool logIn = false;
		bool logOut = true;
		bool errorPassword = false;
		bool errorUser = false;

		string userName;
		string password;

		#endregion

		#region Comands
		public ICommand ShowPopUpCommand { get; }
		public Command LoginCommand { get; }
		public Command LogOutCommand { get; }
		public Command NuevoRegistroCommand { get; }
		public ICommand ResetTextBoxUserCommand { get; }
		public ICommand ResetTextBoxPasswordCommand { get; }
		public ICommand OpenFbCommand { get; }
		public ICommand OpenCallCommand { get; }
		public ICommand OpenCorreoCommand { get; }
		public ICommand OpenWebCommand { get; }
		public Command NewPasswordComand { get; }

		#endregion

		#region Propiedades Publicas bool
		public bool ErrorPassword
		{
			get => this.errorPassword;
			set => SetProperty(ref this.errorPassword, value);
		}
		public bool ErrorUser
		{
			get => this.errorUser;
			set => SetProperty(ref this.errorUser, value);
		}

		public bool LogIn
		{
			get => this.logIn;
			set => SetProperty(ref this.logIn, value);
		}
		public bool LogOut
		{
			get => this.logOut;
			set => SetProperty(ref this.logOut, value);
		}

		#endregion

		#region Propiedades Publicas string
		public string UserName
		{
			get => this.userName;
			set => SetProperty(ref this.userName, value);
		}
		public string Password
		{
			get => this.password;
			set => SetProperty(ref this.password, value);
		}

		#endregion

		public Thread ThFaillog { get; set; }

		#region funciones
		public void LoggedAccount()
		{
			Logged = Preferences.Get("Logged", false);
			ShowUserInfo();

			if (Logged)
			{
				UserName = Preferences.Get("User", "");
				Password = Preferences.Get("Password", "");
				OnLoginClicked();
			}
			else
			{
				OnLogOutClicked();
			}
		}

		async void OnLoginClicked()
		{
			Pressed = true;
			await Task.Delay(500);
			var logged =  ValidateLogin();
			if (logged)
			{
				Preferences.Set("Logged", true);
				Logged =true;
				ShowUserInfo();
				SaveLogInData();
				await Shell.Current.GoToAsync("//Home");
				Pressed = false;
			}
			else
			{
				Preferences.Set("Logged", false);
				Logged = false;
				Pressed = false;
				if (string.IsNullOrWhiteSpace(ErrorPopWsMsg))
				{
					IsBusy = false;
					IsReady = true;
					return;
				}
				IsBusy = false;
				IsReady = true;
				ShowPopErrorWs= true;
				ThFaillog = new Thread(new ThreadStart(hidePopUp));
				ThFaillog.Start();
			}
		}

		public void OnLogOutClicked()
		{
			RemoveLogInData();
			Password = string.Empty;
			LogIn = false;
			LogOut = true;
			Logged = false;
		}

		async void hidePopUp(){
			if (ErrorPopWsMsg.Length>40)
			{
				await Task.Delay(7000);
			}
			else
			{
				await Task.Delay(5000);
			}
			ShowPopErrorWs = false;
		}

		void SaveLogInData()
		{
			Preferences.Set("User", UserName);
			Preferences.Set("Password", Password);
		}

		void RemoveLogInData(){
			Preferences.Set("Keep", false);
			Preferences.Set("User", "");
			Preferences.Set("Password", "");
			Preferences.Set("Logged", false);
		}

		void ShowUserInfo()
		{
			if (Logged)
			{
				LogOut = false;
				LogIn = true;
			}
			else
			{
				LogOut = true;
				LogIn = false;
			}
		}
		bool ValidateLogin()
		{
			IsBusy = true;
			IsReady= false;
			if (string.IsNullOrWhiteSpace(UserName))
			{
				ErrorPopWsMsg = string.Empty;
				ErrorUser = true;
				IsBusy = false;
				IsReady = true;
				return false;
			}

			if (string.IsNullOrWhiteSpace(Password))
			{
				ErrorPopWsMsg = string.Empty;
				ErrorPassword = true;
				IsBusy = false;
				IsReady = true;
				return false;
			}

			if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
			{
				ErrorPopWsMsg = clsUriWs.ErrorRed;
				IsBusy = false;
				IsReady = true;
				return false;
			}

			
			var rq = new clsUsuario();
			rq.Amb = Amb;
			rq.User = userName;
			rq.Password = Password;
			var strrq = rq.LogInWsJson();

			if (strrq.Contains(clsUriWs.ErrorHttp))
			{
				strrq=strrq.Replace(clsUriWs.ErrorHttp, string.Empty);
				ErrorPopWsMsg = strrq;
				IsReady = true;
				IsBusy = false;
				return false;
			}else if (strrq.Contains(clsUriWs.ErrorHttpLientek))
			{
				strrq = strrq.Replace(clsUriWs.ErrorHttpLientek, string.Empty);
				ErrorPopWsMsg = strrq;
				IsReady = true;
				IsBusy = false;
				return false;
			}

			IsBusy = false;
			IsReady = true;
			return true;

		}
		
		async void WebPage()
		{
			//no abre en ios
			if (DeviceInfo.Current.Platform.ToString().Equals(clsUriWs.iOS))
			{
				MainThread.BeginInvokeOnMainThread(() =>
				{
					Uri uri = new Uri(clsUriWs.StoreAppiOS);
					Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
				});
			}
			else
			{
				await Browser.OpenAsync(clsUriWs.GoogleLientek);
			}
			ShowPopUp = false;
		}

		async void Facebook()
		{
			await Browser.OpenAsync(clsUriWs.FbLientek);
			ShowPopUp = false;
		}

		async void Correo()
		{
			//probar a ios
			if (Email.Default.IsComposeSupported)
			{

				string subject = string.Empty;
				string body = clsUriWs.BodyContactoLientek;
				string[] recipients = new[] { clsUriWs.CorreoLientek };

				var message = new EmailMessage
				{
					Subject = subject,
					Body = body,
					BodyFormat = EmailBodyFormat.PlainText,
					To = new List<string>(recipients)
				};

				await Email.Default.ComposeAsync(message);
			}
			ShowPopUp = false;
		}

		void Call()
		{
			//probar en ios
			if (PhoneDialer.Default.IsSupported)
				PhoneDialer.Default.Open(clsUriWs.NumeroLientek);

			ShowPopUp = false;
		}

		async void PopUpVisible()
		{
			if (ShowPopUp) {
				ShowPopUp = false;
			}
			else
			{
				await Task.Delay(200);
				ShowPopUp = true;
			}
			
		}

		async void OnNuevoRegistroClicked()
		{
			Pressed = true;
			//await Navigation.NavigateToAsync<RegistrarViewModel>(null);
			await Task.Delay(500);
			Pressed = false;
		}

		async void OnNewPasswordClick()
		{
			Pressed = true;
			await Navigation.NavigateToAsync<ResetearContraseñaViewModel>(null);
			await Task.Delay(500);
			Pressed = false;
		}

		#endregion
	}
}