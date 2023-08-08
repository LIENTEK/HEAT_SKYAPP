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
			IsBusy = true;
			Items = new ObservableCollection<clsTrampas>();
			Establos = new ObservableCollection<clsEstablo>();
			CommandConsultar = new Command(ChangeEstablo);
			ShowPopUpCommand = new Command(x => ShowPopErrorWs = false);
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

		public Command ShowPopUpCommand { get; }
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
				await Task.Delay(1000);
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
							
						}
					}

					SelEstablo = Establos.Where(x => x.ESTABLO_ID.Equals(Preferences.Get("IdEstablo", 0))).FirstOrDefault();


				}
				catch (Exception ex)
				{
					IsBusy = false;
					ErrorPopWsMsg = ex.Message + Environment.NewLine+Environment.NewLine+ strrq;
					ErrorPopWsMsg = "Intente de nuevo porfavor";
					ShowPopErrorWs = true;
					
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
				var id = int.Parse(SelEstablo.ESTABLO_ID.ToString());
				Preferences.Set("IdEstablo", id);

				strrq = new clsConsultas().ObtenerTrampas(SelEstablo.ESTABLO_ID.ToString());
				if (strrq.Equals("[]"))
				{
					IsBusy = false;
					return;
				}

				var res = JsonConvert.DeserializeObject<List<clsTrampas>>(strrq);

				
				Items.Clear();
				var header = new clsTrampas();
				header.CORRAL = "0";
				foreach (var item in res)
				{
					if (!header.CORRAL.Equals(item.CORRAL))
					{
						header.CORRAL = item.CORRAL;
						header.ESTATUS = "Estatus";
						header.TRAMPA = "Trampa";
						header.DURACION = "Tiempo (hr)";
						header.AlertaEstatus = Colors.Transparent;
						header.AlertaOffline = Colors.Transparent;
						header.AlertaHeader = Colors.Transparent;
						Items.Add(header);
					}
					item.AlertaEstatus = Colors.White;
					item.AlertaOffline = Colors.White;
					item.AlertaHeader = Colors.White;

					if (string.IsNullOrEmpty(item.ESTATUS))
					{
						item.AlertaEstatus = Colors.Red;
					}
					else
					{
						if (item.ESTATUS.Equals("ENTRAMPADO"))
						{
							item.AlertaEstatus = Colors.Green;
						}

						if (item.ESTATUS.Equals("OFFLINE"))
						{
							item.AlertaEstatus = Colors.Red;
						}
					}

					

					var n = double.Parse(item.DURACION);
					if (n>200)
					{
						item.AlertaOffline = Colors.Red;
					}

					Items.Add(item);
				}

				IsBusy = false;
			}
			catch (Exception ex)
			{

				await Task.Delay(500);
				IsBusy = false;
				ErrorPopWsMsg = ex.Message + Environment.NewLine + Environment.NewLine + strrq;
				ErrorPopWsMsg = "Intente de nuevo porfavor";
				ShowPopErrorWs = true;
				
			}

		}

		async void ChangeEstablo()
		{
			IsBusy = true;
			await Task.Delay(1000);
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