using Base.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Base.ViewModels
{
    public class TrampasViewModel : BaseViewModel
	{
		public TrampasViewModel()
		{
			Items = new ObservableCollection<clsTrampas>();
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


		public ObservableCollection<clsTrampas> Items { get; set; }
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
							ThFaillog = new Thread(new ThreadStart(hidePopUp));
							ThFaillog.Start();
						}
					}

					SelEstablo = Establos.Where(x => x.ESTABLO_ID.Equals(Preferences.Get("IdEstablo", 0))).FirstOrDefault();


				}
				catch (Exception ex)
				{
					IsBusy = false;
					ErrorPopWsMsg = ex.Message + Environment.NewLine+Environment.NewLine+ strrq;
					ShowPopErrorWs = true;
					ThFaillog = new Thread(new ThreadStart(hidePopUp));
					ThFaillog.Start();
				}

			}
			else
			{
				IsBusy = false;
				//await Task.Delay(100);
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
			

			try
			{
				Preferences.Set("IdEstablo", SelEstablo.ESTABLO_ID);

				strrq = new clsConsultas().ObtenerTrampas(SelEstablo.ESTABLO_ID.ToString());
				if (strrq.Equals("[]"))
				{
					IsBusy = false;
					return;
				}

				if (strrq.Contains("null"))
				{
					IsBusy = false;
					return;
				}

				var res = JsonConvert.DeserializeObject<List<clsTrampas>>(strrq);

				
				Items.Clear();
				foreach (var item in res)
				{
					item.AlertaEstatus = Colors.White;
					item.AlertaOffline = Colors.White;

					
					if (item.ESTATUS.Equals("ENTRAMPADO"))
					{
						item.AlertaEstatus = Colors.Green;
					}

					if (item.ESTATUS.Equals("OFFLINE"))
					{
						item.AlertaEstatus = Colors.Red;
					}

					if (item.DURACION>200)
					{
						item.AlertaOffline = Colors.Red;
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