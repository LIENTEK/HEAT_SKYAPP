using Base.Models;
using System.Windows.Input;


namespace Base.ViewModels
{
	public class ResetearContraseñaViewModel : BaseViewModel
	{
		public ResetearContraseñaViewModel() {
			Title = "Resetear Contraseña";
			IsReady = true;
			IsOne = false;
			IsBusy = false;
			ShowToken = true;
			Pressed = false;

			ShowPopUpLCommand = new Command(PopUpVisible);
			ResetCommand = new Command(OnResetClick, BlockButton);
			CambioCommand = new Command(OnSendTokenClick, BlockButton);

			RTUsuarioCommand = new Command(x => ErrorUsuario = false);
			RTPasswordCommand = new Command(CheckPassword);
			RTCPasswordCommand = new Command(CheckPassword);
			RTTokenCommand = new Command(x=> ErrorToken=false);
			ShowPopUpCommand = new Command(x => ShowPopErrorWs = false);
		}

		bool showToken = false;
		bool errorUsario = false;
		bool errorPassword = false;
		bool errorCpassword = false;
		bool errorToken = false;

		string token= string.Empty;
		string correo = string.Empty;
		string usuario = string.Empty;
		string password = string.Empty;
		string cpassword = string.Empty;
		string helpPassword = clsUriWs.HelperPassword;

		#region Comands

		public ICommand ShowPopUpLCommand { get; }

		public ICommand ShowPopUpCommand { get; }

		public ICommand CambioCommand { get; }
		public ICommand ResetCommand { get; }

		public ICommand RTUsuarioCommand { get; }
		public ICommand RTPasswordCommand { get; }
		public ICommand RTCPasswordCommand { get; }
		public ICommand RTTokenCommand { get; }

		#endregion

		#region Propiedades Publicas bool

		public bool ErrorUsuario
		{
			get => this.errorUsario;
			set => SetProperty(ref this.errorUsario, value);
		}
		public bool ErrorPassword
		{
			get => this.errorPassword;
			set => SetProperty(ref this.errorPassword, value);
		}
		public bool ErrorCpassword
		{
			get => this.errorCpassword;
			set => SetProperty(ref this.errorCpassword, value);
		}
		public bool ErrorToken
		{
			get => this.errorToken;
			set => SetProperty(ref this.errorToken, value);
		}
		public bool ShowToken
		{
			get => this.showToken;
			set => SetProperty(ref this.showToken, value);
		}

		#endregion

		#region propiedades publicas stringg

		public string HelpPassword
		{
			get => this.helpPassword;
			set => SetProperty(ref this.helpPassword, value);
		}

		public string Usuario
		{
			get => this.usuario;
			set => SetProperty(ref this.usuario, value);
		}

		public string Token
		{
			get => this.token;
			set => SetProperty(ref this.token, value);
		}

		public string Password
		{
			get => this.password;
			set => SetProperty(ref this.password, value);
		}

		public string Cpassword
		{
			get => this.cpassword;
			set => SetProperty(ref this.cpassword, value);
		}

		public string Correo
		{
			get => this.correo;
			set => SetProperty(ref this.correo, value);
		}

		#endregion

		public Thread ThFailReset { get; set; }

		#region funciones

		async void OnResetClick()
		{
			Pressed=true;
			await Task.Delay(500);
			ErrorPopWsMsg = string.Empty;
			IsBusy = true;
			IsReady = false;

			if (ValidateReset())
			{
				await Task.Delay(2000);
				IsBusy = false;
				IsReady = true;
				IsOne = false;
				Pressed = false;
				ShowToken = true;
			}
			else
			{
				IsBusy = false;
				IsReady = true;
				Pressed = false;
				if (string.IsNullOrWhiteSpace(ErrorPopWsMsg))
				{
					return;
				}
				ShowPopErrorWs = true;
				

			}
			Pressed = false;
		}

		bool ValidateReset()
		{
			
			if (string.IsNullOrWhiteSpace(Usuario) || usuario.Length > clsUriWs.LongUser || usuario.Length < clsUriWs.ShortUser)
			{
				ErrorUsuario = true;
				return false;
			}

			if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
			{
				ErrorPopWsMsg = clsUriWs.ErrorRed;
				return false;
			}

			var rq = new clsConsultas();
			var strrq = rq.CambiarContraseña(User,Password);
			strrq= "rubendiaznt@live.com.mx";

			if (strrq.Contains(clsUriWs.ErrorHttp))
			{
				strrq = strrq.Replace(clsUriWs.ErrorHttp, string.Empty);
				ErrorPopWsMsg = strrq;
				return false;
			}
			else if (strrq.Contains(clsUriWs.ErrorHttpLientek))
			{
				strrq = strrq.Replace(clsUriWs.ErrorHttpLientek, string.Empty);
				ErrorPopWsMsg = strrq;
				return false;
			}

			Correo = strrq;
			return true;
		}

		async void OnSendTokenClick()
		{
			Pressed = true;
			await Task.Delay(500);
			ErrorPopWsMsg = string.Empty;
			IsBusy = true;
			IsReady = false;

			if (ValidateNewPassword())
			{
				await Task.Delay(2000);
				IsBusy = false;
				IsReady = true;
				ShowPopExitoWs= true;
				await Task.Delay(2000);
				await Shell.Current.Navigation.PopAsync();
			}
			else
			{
				IsBusy = false;
				IsReady = true;
				Pressed = false;
				if (string.IsNullOrWhiteSpace(ErrorPopWsMsg))
				{
					return;
				}
				ShowPopErrorWs = true;
				
				
			};
		}

		bool ValidateNewPassword()
		{
			//if (string.IsNullOrWhiteSpace(Token)) // || Password.Length > clsUriWs.LongPassword || usuario.Length < clsUriWs.ShortPassword)
			//{
			//	ErrorToken = true;
			//	return false;
			//}

			if (string.IsNullOrWhiteSpace(Password) || Password.Length > clsUriWs.LongPassword || Password.Length < clsUriWs.ShortPassword)
			{
				ErrorPassword = true;
				return false;
			}

			if (string.IsNullOrWhiteSpace(Cpassword) || Cpassword.Length > clsUriWs.LongPassword || Cpassword.Length < clsUriWs.ShortPassword || !Cpassword.Equals(Password))
			{
				ErrorCpassword = true;
				return false;
			}

			if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
			{
				ErrorPopWsMsg = clsUriWs.ErrorRed;
				return false;
			}

			var rq = new clsConsultas();
			var strrq = rq.CambiarContraseña(User,Password);

			if (strrq.Contains(clsUriWs.ErrorHttp))
			{
				strrq = strrq.Replace(clsUriWs.ErrorHttp, string.Empty);
				ErrorPopWsMsg = strrq;
				return false;
			}
			else if (strrq.Contains(clsUriWs.ErrorHttpLientek))
			{
				strrq = strrq.Replace(clsUriWs.ErrorHttpLientek, string.Empty);
				ErrorPopWsMsg = strrq;
				return false;
			}else if (strrq.Equals("OK"))
			{
				ExitoPopWsMsg = "Cambio de contraseña exitoso.";
				return true;
			}
			else
			{
				ExitoPopWsMsg = strrq;
				return false;
			}
			
		}

		async void PopUpVisible()
		{
			//if (ShowPopUp)
			//{
			//	ShowPopUp = false;
			//}
			//else
			//{
			//	await Task.Delay(200);
			//	ShowPopUp = true;
			//}
			await Browser.OpenAsync(clsUriWs.Cliente);
		}

		void CheckPassword()
		{
			ErrorCpassword = false;
			ErrorPassword = false;
		}

		async void hidePopUp()
		{
			if (ErrorPopWsMsg.Length > 40)
			{
				await Task.Delay(6000);
			}
			else
			{
				await Task.Delay(3000);
			}
			ShowPopErrorWs = false;
		}

		#endregion
	}
}
