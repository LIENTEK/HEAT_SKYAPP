using Base.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using static Base.Models.clsChart;

namespace Base.ViewModels
{
	public class EstresCaloricoViewModel: BaseViewModel
	{		
		public ObservableCollection<Item> UV {
			get => this.uv;
			set => SetProperty(ref this.uv, value);
		}
		public ObservableCollection<Item> ITH {
			get => this.ith;
			set => SetProperty(ref this.ith, value);
		}
		public ObservableCollection<Item> HUMEDAD {
			get => this.humedad;
			set => SetProperty(ref this.humedad, value);
		}
		public ObservableCollection<Item> TEMP {
			get => this.temp;
			set => SetProperty(ref this.temp, value);
		}

		public EstresCaloricoViewModel() {

			Establos = new ObservableCollection<clsEstablo>();
			CommandConsultar = new Command(LoadPropiedades);
			//LoadData();

		}



		#region Variables
		string nombre = string.Empty;
		string date;
		List<clsChart> res;
		ObservableCollection<Item> temp = new ObservableCollection<Item>();
		ObservableCollection<Item> uv = new ObservableCollection<Item>();
		ObservableCollection<Item> ith = new ObservableCollection<Item>();
		ObservableCollection<Item> humedad = new ObservableCollection<Item>();

		#endregion

		#region Propiedades bool

		#endregion

		#region Propiedades string
		public string Nombre
		{
			get => this.nombre;
			set => SetProperty(ref this.nombre, value);
		}

		public string Date
		{
			get => this.date;
			set => SetProperty(ref this.date, value);
		}

		public ObservableCollection<clsEstablo> Establos { get; set; }

		clsEstablo selEstablo;
		public clsEstablo SelEstablo
		{
			get => this.selEstablo;
			set => SetProperty(ref this.selEstablo, value);
		}

		#endregion

		#region Commands
		public ICommand CommandConsultar { get; }

		public Thread ThFaillog { get; set; }

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
					var strrq = rqestablo.ObtenerEstablos(objuser.USUARIO_ID.ToString());

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

										
					
					Date = DateTime.Now.ToString("dd/MM/yyyy");
					UV = new ObservableCollection<Item>();
					TEMP = new ObservableCollection<Item>();
					ITH = new ObservableCollection<Item>();
					HUMEDAD = new ObservableCollection<Item>();
					SelEstablo = Establos.Where(x => x.ESTABLO_ID.Equals(Preferences.Get("IdEstablo", 0))).FirstOrDefault();
					//LoadPropiedades();
					
					
				}
				catch (Exception ex)
				{
					IsBusy = false;
					ErrorPopWsMsg = ex.Message;
					ShowPopErrorWs = true;
					ThFaillog = new Thread(new ThreadStart(hidePopUp));
					ThFaillog.Start();
				}

			}
			else
			{
				IsBusy = false;
				await Task.Delay(100);
				ShowPopUp = true;
			}

		}
		
		void LoadPropiedades()
		{
			UV.Clear();
			TEMP.Clear();
			ITH.Clear();
			HUMEDAD.Clear();

			try
			{
				res = new List<clsChart>();

				Preferences.Set("IdEstablo", SelEstablo.ESTABLO_ID);
				var strrq = new clsConsultas().DatosChart(SelEstablo.LATITUD.ToString(), SelEstablo.LONGITUD.ToString());
				res = JsonConvert.DeserializeObject<List<clsChart>>(strrq);


				res = res.OrderBy(x=> x.hora).ToList();
				var elemento = new Item();
				int a = 0;

				foreach (var row in res)
				{
					elemento = new Item();
					elemento.Hora = row.hora.Hour;
					elemento.Value = row.humedad;
					HUMEDAD.Add(elemento);
					elemento = new Item();
					elemento.Hora = row.hora.Hour;
					elemento.Value = row.temp;
					TEMP.Add(elemento);
					elemento = new Item();
					elemento.Hora = row.hora.Hour;
					elemento.Value = row.ITH;
					ITH.Add(elemento);
					
				}

				while(a<=23){
					elemento = new Item();
					elemento.Hora = a;
					elemento.Value = 74;
					UV.Add(elemento);
					a++;
				}
				


				IsBusy = false;
			}
			catch (Exception ex)
			{
				IsBusy = false;
				ErrorPopWsMsg = ex.Message;
				ShowPopErrorWs = true;
				ThFaillog = new Thread(new ThreadStart(hidePopUp));
				ThFaillog.Start();
			}


		}
		async void hidePopUp()
		{
			if (ErrorPopWsMsg.Length > 40)
			{
				await Task.Delay(7000);
			}
			else
			{
				await Task.Delay(5000);
			}
			ShowPopErrorWs = false;
		}
		Boolean Pago()
		{
			
				return true;
			
		}

		#endregion
	}
}