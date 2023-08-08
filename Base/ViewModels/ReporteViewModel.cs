using Base.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.ViewModels
{
	public class ReporteViewModel :  BaseViewModel
	{
		private Stream m_pdfDocumentStream;
		public Stream PdfDocumentStream
		{
			get
			{
				return m_pdfDocumentStream;
			}
			set
			{
				m_pdfDocumentStream = value;
				OnPropertyChanged("PdfDocumentStream");
			}
		}

		string strrq = "";
		public Command CommandConsultar { get; }
		public Command ShowPopUpCommand { get; }

		public ObservableCollection<clsEstablo> Establos { get; set; }
		//public Thread ThFaillog { get; set; }

		clsEstablo selEstablo;
		public clsEstablo SelEstablo
		{
			get => this.selEstablo;
			set => SetProperty(ref this.selEstablo, value);
		}
		
		public ReporteViewModel()
		{
			Establos = new ObservableCollection<clsEstablo>();
			CommandConsultar = new Command(ChangeEstablo);
			ShowPopUpCommand = new Command(x => ShowPopErrorWs = false);
		}

		
		private async void SetPdfDocumentStream(string URL)
		{
			try
			{
				HttpClient httpClient = new HttpClient();
				HttpResponseMessage response = await httpClient.GetAsync(URL);

				PdfDocumentStream = await response.Content.ReadAsStreamAsync();
			}catch (Exception ex)
			{
				ErrorPopWsMsg = ex.Message;
				ErrorPopWsMsg = "Intente de nuevo porfavor";
				ShowPopErrorWs =true;
				
			}
		}

		//public void OnPropertyChanged(string name)
		//{
		//	PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		//}
		void ChangeEstablo()
		{
			LoadPDF();
		}


		async public void OnAppearing()
		{
			ChecarVersion();
			Logged = Preferences.Get("Logged", false);
			if (!Logged)
			{
				IsOne = true;
				ErrorPopWsMsg = "No has iniciado session no puedes continuar.";
				await Task.Delay(100);
				ShowPopErrorWs = true;
			}
			else
			{
				IsOne = false;
				IsBusy = true;
				LoadData();
			}
		}

		void LoadPDF()
		{
			var strrq = new clsConsultas().ObtenerReporte(SelEstablo.ESTABLO_ID.ToString());
			SetPdfDocumentStream(strrq);
		}

		async void LoadData()
		{
			if (Pago())
			{
				Establos.Clear();

				try
				{
					clsUsuario objuser = new clsUsuario();
					string obj = Preferences.Get("objuser", "");
					objuser = Newtonsoft.Json.JsonConvert.DeserializeObject<clsUsuario>(obj);

					var establo = new clsEstablo();
					var propiedad = new clsPropiedades();

					//obtener establos
					var rqestablo = new clsConsultas();
					strrq = rqestablo.ObtenerEstablos(objuser.USUARIO_ID.ToString());

					var responseestablos = JsonConvert.DeserializeObject<List<clsEstablo>>(strrq);

					if (responseestablos.Count > 1)
					{
						foreach (var item in responseestablos)
						{
							establo = new clsEstablo();
							establo.ESTABLO_ID = item.ESTABLO_ID;
							establo.NOMBRE = item.NOMBRE;
							establo.LATITUD = item.LATITUD;
							establo.LONGITUD = item.LONGITUD;
							Establos.Add(establo);
						}

					}
					else
					{
						if (responseestablos[0].ESTABLO_ID == -1)
						{
							var error = JsonConvert.DeserializeObject<clsEstablo>(strrq);
							ErrorPopWsMsg = error.NOMBRE;
							ShowPopErrorWs = true;
						}
					}

					SelEstablo = Establos.Where(x => x.ESTABLO_ID.Equals(Preferences.Get("IdEstablo", 0))).FirstOrDefault();


				}
				catch (Exception ex)
				{
					IsBusy = false;
					ErrorPopWsMsg = ex.Message + Environment.NewLine + Environment.NewLine + strrq;
					ErrorPopWsMsg = "Intente de nuevo porfavor";
					ShowPopErrorWs = true;
				}

			}
			else
			{
				IsBusy = false;
				await Task.Delay(100);
				ShowPopUp = true;
			}
		}
		async void hidePopUp()
		{
			if (ErrorPopWsMsg.Length > 40)
			{
				await Task.Delay(2000);
			}
			else
			{
				await Task.Delay(2000);
			}
			ShowPopErrorWs = false;
		}

		Boolean Pago()
		{
			//revisar si esta pagada la app
			return true;
		}

	}
}