﻿using Base.Models;
using Newtonsoft.Json;
using System.Windows.Input;

namespace Base.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		public LoginViewModel()
		{
			LoggedAccount();
			ChecarVersion();
			

			ShowPopUpLCommand = new Command(PopUpVisible);
			ResetTextBoxUserCommand = new Command(x => ErrorUser = false);
			ResetTextBoxPasswordCommand = new Command(x => ErrorPassword = false);
			LoginCommand = new Command(OnLoginClicked);
			LogOutCommand = new Command(OnLogOutClicked);
			ResetCommand = new Command(OnResetClicked);
			ForgettCommand = new Command(OnForgetClick);
			OpenFbCommand = new Command(Facebook);
			OpenCallCommand = new Command(Call);
			OpenCorreoCommand = new Command(Correo);
			OpenWebCommand = new Command(WebPage);
			ShowPopUpCommand = new Command(x => ShowPopErrorWs = false);

		}

		#region variables
		bool logIn = false;
		bool logOut = true;
		bool errorPassword = false;
		bool errorUser = false;

		string userName;
		string password;
		string cliente;
		string nombre;

		#endregion

		#region Comands
		public ICommand ShowPopUpCommand { get; }
		public ICommand ShowPopUpLCommand { get; }
		public ICommand LoginCommand { get; }
		public ICommand LogOutCommand { get; }
		public ICommand ResetCommand { get; }
		public ICommand ResetTextBoxUserCommand { get; }
		public ICommand ResetTextBoxPasswordCommand { get; }
		public ICommand OpenFbCommand { get; }
		public ICommand OpenCallCommand { get; }
		public ICommand OpenCorreoCommand { get; }
		public ICommand OpenWebCommand { get; }
		public ICommand ForgettCommand { get; }

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

		public string Cliente
		{
			get => this.cliente;
			set => SetProperty(ref this.cliente, value);
		}
		public string Nombre
		{
			get => this.nombre;
			set => SetProperty(ref this.nombre, value);
		}

		#endregion

		public Thread ThFaillog { get; set; }

		#region funciones
		public void LoggedAccount()
		{
			Logged = Preferences.Get("Logged", false);
			UserName = Preferences.Get("User", "");
			Password = Preferences.Get("Password", "");
			ShowUserInfo();

			if (Logged)
			{
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

		async void hidePopUpE()
		{
			await Task.Delay(2000);
			ShowPopExitoWs = false;
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

			
			var rq = new clsConsultas();
			
			var strrq = rq.LogInWs(userName,Password);

			try
			{
				var response = JsonConvert.DeserializeObject<clsUsuario>(strrq);
				if (response.USUARIO_ID == -1)
				{
					ErrorPopWsMsg = response.NOMBRE;
					IsReady = true;
					IsBusy = false;
					return false;
				}

				Nombre = response.NOMBRE.ToString();
				Cliente = response.CLIENTE_ID.ToString();

				Preferences.Set("objuser", Newtonsoft.Json.JsonConvert.SerializeObject(response));
				IsBusy = false;
				IsReady = true;
				return true;
			}
			catch
			{
				ErrorPopWsMsg = "Intente de nuevo porfavor";
				IsBusy = false;
				IsReady = true;
				return false;
			}

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
			//if (ShowPopUp) {
			//	ShowPopUp = false;
			//}
			//else
			//{
			//	await Task.Delay(200);
			//	ShowPopUp = true;
			//}
			await Browser.OpenAsync(clsUriWs.Cliente);
		}

		async void OnResetClicked()
		{
			Pressed = true;
			await Navigation.NavigateToAsync<ResetearContraseñaViewModel>(null);
			await Task.Delay(500);
			Pressed = false;
		}

		async void OnForgetClick()
		{
			

			Pressed = true;
			//await Navigation.NavigateToAsync<RegistrarViewModel>(null);
			if (UserName.Length >= clsUriWs.ShortUser && UserName.Length < clsUriWs.LongUser)
			{
				var str = new clsConsultas().OlvidoContraseña(userName);

				if (str.Equals("OK"))
				{
					ExitoPopWsMsg = "Se envio tu contraseña al correo vinculado";
					ShowPopExitoWs = true;
					ThFaillog = new Thread(new ThreadStart(hidePopUpE));
					ThFaillog.Start();
				}
				else
				{
					ErrorPopWsMsg = str;
					ShowPopErrorWs = true;
					
				}
			}
			else
			{
				ErrorPopWsMsg = "Ingresa un usuario valido";
				ShowPopErrorWs = true;
				
			}
			await Task.Delay(500);
			Pressed = false;
		}

		#endregion
	}
}