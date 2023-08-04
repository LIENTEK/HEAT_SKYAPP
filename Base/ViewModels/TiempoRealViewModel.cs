using Base.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Base.ViewModels
{
	public class TiempoRealViewModel : BaseViewModel
	{
		public TiempoRealViewModel()
		{
			Items = new ObservableCollection<clsTiempoReal>();
			Establos = new ObservableCollection<clsEstablo>();
			CommandConsultar = new Command(ChangeEstablo);
		}



		#region Variables
		string nombre = string.Empty;
		string strrq = "";
		#endregion

		#region Propiedades bool

		#endregion

		#region Propiedades string
		public string Nombre
		{
			get => this.nombre;
			set => SetProperty(ref this.nombre, value);
		}

		public ObservableCollection<clsTiempoReal> Items { get; set; }
		public ObservableCollection<clsEstablo> Establos { get; set; }
		#endregion

		#region Commands
		public ICommand CommandCalendario { get; }
		public ICommand CommandConsultar { get; }

		public Thread ThFaillog { get; set; }

		clsEstablo selEstablo;
		public clsEstablo SelEstablo
		{
			get => this.selEstablo;
			set => SetProperty(ref this.selEstablo, value);
		}


		clsPropiedades selPropiedad;
		public clsPropiedades SelPropiedad
		{
			get => this.selPropiedad;
			set => SetProperty(ref this.selPropiedad, value);
		}
		#endregion

		#region Funciones

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
				//ThFaillog = new Thread(new ThreadStart(LoadPropiedades));
				//ThFaillog.Start();
			}
		}

		void LoadData()
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

					if (strrq.Equals("[]"))
					{
						IsBusy = false;
						return;
					}

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
							var responerror = JsonConvert.DeserializeObject<clsEstablo>(strrq);
							ErrorPopWsMsg = responerror.NOMBRE;
							ShowPopErrorWs = true;
							ThFaillog = new Thread(new ThreadStart(hidePopUp));
							ThFaillog.Start();
						}
					}

					SelEstablo = Establos.Where(x => x.ESTABLO_ID.Equals(Preferences.Get("IdEstablo", 0))).FirstOrDefault();


				}
				catch (Exception ex)
				{
					IsBusy = false;
					ErrorPopWsMsg = ex.Message + Environment.NewLine + Environment.NewLine + strrq;
					ShowPopErrorWs = true;
					ThFaillog = new Thread(new ThreadStart(hidePopUp));
					ThFaillog.Start();
				}

			}
			else
			{
				IsBusy = false;
				Task.Delay(100);
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

		async void LoadPropiedades()
		{
			Items.Clear();

			try
			{
				Preferences.Set("IdEstablo", SelEstablo.ESTABLO_ID);

				strrq = new clsConsultas().ObtenerCorrales(SelEstablo.ESTABLO_ID.ToString());
				var res = JsonConvert.DeserializeObject<List<clsTiempoReal>>(strrq);

				foreach (var item in res)
				{
					item.AlertaTemp = Color.FromRgba("#ffffff");
					item.AlertaITH = Color.FromRgba("#ffffff");
					item.AlertaHumedad = Color.FromRgba("#ffffff");

					
					if (item.ITH >= 80)
					{
						item.AlertaITH = Color.FromRgb(255,117,117);
					}
					if (item.ITH >= 74 && item.ITH < 80)
					{
						item.AlertaITH = Color.FromRgb(255, 204, 102);
					}
					if (item.ITH >= 60 && item.ITH < 74)
					{
						item.AlertaITH = Color.FromRgb(255, 255, 153);
					}

					if (item.TEMPERATURA >= 39.5)
					{
						item.AlertaTemp = Color.FromRgb(255, 199, 206);
					}
					if (item.TEMPERATURA >= 39 && item.TEMPERATURA<39.5)
					{
						item.AlertaTemp = Color.FromRgb(255, 235, 156);
					}
					

					Items.Add(item);
				}

				await Task.Delay(1500);
				IsBusy = false;
			}
			catch (Exception ex)
			{
				await Task.Delay(1500);
				IsBusy = false;
				ErrorPopWsMsg = ex.Message + Environment.NewLine + Environment.NewLine + strrq;
				ShowPopErrorWs = true;
				ThFaillog = new Thread(new ThreadStart(hidePopUp));
				ThFaillog.Start();
			}

		}

		void ChangeEstablo()
		{
			LoadPropiedades();
		}
		
		Boolean Pago()
		{
			//revisar si esta pagada la app
			return true;
		}


		#endregion
	}
}