using Base.Models;
using System.Windows.Input;

namespace Base.ViewModels
{
	public class FeedbackViewModel : BaseViewModel
	{
		public FeedbackViewModel() 
		{
			IsReady = true;
			IsOne = true;
			IsBusy = false;
			Pressed = false;

			SendCommand = new Command(OnClickEnviar, BlockButton);
			S1TCommand = new Command(OnS1);
			S2TCommand = new Command(OnS2);
			S3TCommand = new Command(OnS3);
			S4TCommand = new Command(OnS4);
			S5TCommand = new Command(OnS5);
		}

		#region Variables
		string errorComentario = string.Empty;
		string comentario = string.Empty;

		static string sw = "about";
		static string sg = "popup";

		string s1 = sw;
		string s2 = sw;
		string s3 = sw;
		string s4 = sw;
		string s5 = sw;

		
		int evaluacion = 0;
		#endregion

		#region Propiedades bool
		
		
		#endregion

		#region Propiedades string
		public string Comentario
		{
			get => this.comentario;
			set => SetProperty(ref this.comentario, value);
		}
		public string ErrorComentario
		{
			get => this.errorComentario;
			set => SetProperty(ref this.errorComentario, value);
		}
		
		public string S1
		{
			get => this.s1;
			set => SetProperty(ref this.s1, value);
		}
		public string S2
		{
			get => this.s2;
			set => SetProperty(ref this.s2, value);
		}
		public string S3
		{
			get => this.s3;
			set => SetProperty(ref this.s3, value);
		}
		public string S4
		{
			get => this.s4;
			set => SetProperty(ref this.s4, value);
		}
		public string S5
		{
			get => this.s5;
			set => SetProperty(ref this.s5, value);
		}
		#endregion

		#region Commands
		public ICommand SendCommand { get; }
		public ICommand S1TCommand { get; }
		public ICommand S2TCommand { get; }
		public ICommand S3TCommand { get; }
		public ICommand S4TCommand { get; }
		public ICommand S5TCommand { get; }
		#endregion

		public Thread ThSendFeedBack { get; set; }

		#region Funciones
		async void OnClickEnviar()
		{
			Pressed = true;
			IsBusy = true;
			IsReady = false;
			await Task.Delay(500);
			var feedback = ValidateFeedBack();
			if (feedback)
			{
				await Task.Delay(3000);
				IsBusy = false;
				IsReady = true;
				Pressed = false;
				ExitoPopWsMsg = "Nos importa tu opinion, gracias por ayudarnos a mejorar";
				ShowPopExitoWs = true;
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
				ThSendFeedBack = new Thread(new ThreadStart(hidePopUp));
				ThSendFeedBack.Start();
			}

		}
		bool ValidateFeedBack()
		{
			
			if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
			{
				ErrorPopWsMsg = clsUriWs.ErrorRed;
				return false;
			}


			var rq = new clsUsuario();
			var strrq = rq.LogInWs();
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

			return true;

		}

		void OnS1()
		{
			if (evaluacion == 1)
			{
				evaluacion = 0;
				S1 = sw;
				S2 = sw;
				S3 = sw;
				S4 = sw;
				S5 = sw;
				return;
			}
			evaluacion = 1;
			S1 =sg;
			S2 =sw;
			S3 =sw;
			S4 =sw;
			S5 =sw;
		}
		void OnS2()
		{
			if (evaluacion == 2)
			{
				OnS1();
				return;
			}
			evaluacion = 2;
			S1 = sg;
			S2 = sg;
			S3 = sw;
			S4 = sw;
			S5 = sw;
		}
		void OnS3()
		{
			if (evaluacion == 3)
			{
				OnS2();
				return;
			}
			evaluacion = 3;
			S1 = sg;
			S2 = sg;
			S3 = sg;
			S4 = sw;
			S5 = sw;
		}
		void OnS4()
		{
			if (evaluacion == 4)
			{
				OnS3();
				return;
			}
			evaluacion = 4;
			S1 = sg;
			S2 = sg;
			S3 = sg;
			S4 = sg;
			S5 = sw;
		}
		void OnS5()
		{
			if (evaluacion == 5)
			{
				OnS4();
				return;
			}
			evaluacion = 5;
			S1 = sg;
			S2 = sg;
			S3 = sg;
			S4 = sg;
			S5 = sg;
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
