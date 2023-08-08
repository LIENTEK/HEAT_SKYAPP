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

		public ObservableCollection<Item> ESTRES1
		{
			get => this.estres1;
			set => SetProperty(ref this.estres1, value);
		}

		public ObservableCollection<Item> ESTRES2
		{
			get => this.estres2;
			set => SetProperty(ref this.estres2, value);
		}

		public EstresCaloricoViewModel() {

			Establos = new ObservableCollection<clsEstablo>();
			CommandConsultar = new Command(ChangeEstablo);
			ShowPopUpCommand = new Command(x => ShowPopErrorWs = false);
			LeftCommand = new Command(LeftConsulta);
			RightCommand = new Command(RightConsulta);
			//LoadData();

		}



		#region Variables
		string nombre = string.Empty;
		string date;
		List<clsChart> res;
		ObservableCollection<Item> estres1 = new ObservableCollection<Item>();
		ObservableCollection<Item> estres2 = new ObservableCollection<Item>();
		ObservableCollection<Item> temp = new ObservableCollection<Item>();
		ObservableCollection<Item> uv = new ObservableCollection<Item>();
		ObservableCollection<Item> ith = new ObservableCollection<Item>();
		ObservableCollection<Item> humedad = new ObservableCollection<Item>();
		string p1,p2,p3,p4,p5,p6 = string.Empty;
		string h1,h2,h3,h4,h5,h6 = string.Empty;
		DateTime fecha;	
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

		public string H1
		{
			get => this.h1;
			set => SetProperty(ref this.h1, value);
		}
		public string P1
		{
			get => this.p1;
			set => SetProperty(ref this.p1, value);
		}
		public string H2
		{
			get => this.h2;
			set => SetProperty(ref this.h2, value);
		}
		public string P2
		{
			get => this.p2;
			set => SetProperty(ref this.p2, value);
		}
		public string H3
		{
			get => this.h3;
			set => SetProperty(ref this.h3, value);
		}
		public string P3
		{
			get => this.p3;
			set => SetProperty(ref this.p3, value);
		}
		public string H4
		{
			get => this.h4;
			set => SetProperty(ref this.h4, value);
		}
		public string P4
		{
			get => this.p4;
			set => SetProperty(ref this.p4, value);
		}
		public string H5
		{
			get => this.h5;
			set => SetProperty(ref this.h5, value);
		}
		public string P5
		{
			get => this.p5;
			set => SetProperty(ref this.p5, value);
		}
		public string H6
		{
			get => this.h6;
			set => SetProperty(ref this.h6, value);
		}
		public string P6
		{
			get => this.p6;
			set => SetProperty(ref this.p6, value);
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

		public Command ShowPopUpCommand { get; }

		public Command LeftCommand { get; }

		public Command RightCommand { get; }

		public Thread ThFaillog { get; set; }

		#endregion

		#region Funciones

		async public void OnAppearing()
		{
			ChecarVersion();
			Logged = Preferences.Get("Logged", false);
			if (!Logged)
			{
				IsOne = false;
				ErrorPopWsMsg = "No has iniciado session no puedes continuar.";
				await Task.Delay(100);
				ShowPopErrorWs = true;
			}
			else
			{
				IsOne = false;
				IsBusy = true;
				await Task.Delay(100);
				fecha = DateTime.Today;
				Date = fecha.ToString("dd/MM/yyyy");
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
							
						}
					}

					UV = new ObservableCollection<Item>();
					TEMP = new ObservableCollection<Item>();
					ITH = new ObservableCollection<Item>();
					HUMEDAD = new ObservableCollection<Item>();
					ESTRES1 = new ObservableCollection<Item>();
					ESTRES2 = new ObservableCollection<Item>();

					var iteme= new Item();
					for (int i=0; i<=23;i++)
					{
						iteme = new Item();
						iteme.Hora = i;
						iteme.Value = 69;
						iteme.Value2 = 74;
						ESTRES1.Add(iteme);
					}

					SelEstablo = Establos.Where(x => x.ESTABLO_ID.Equals(Preferences.Get("IdEstablo", 0))).FirstOrDefault();					
					
				}
				catch (Exception ex)
				{
					IsBusy = false;
					ErrorPopWsMsg = ex.Message;
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
		
		async void LoadPropiedades()
		{
			UV.Clear();
			TEMP.Clear();
			ITH.Clear();
			HUMEDAD.Clear();

			try
			{
				res = new List<clsChart>();
				var id = int.Parse(SelEstablo.ESTABLO_ID.ToString());
				Preferences.Set("IdEstablo", id);
				var strrq = new clsConsultas().DatosChart(SelEstablo.LATITUD.ToString(), SelEstablo.LONGITUD.ToString(),Date);
				res = JsonConvert.DeserializeObject<List<clsChart>>(strrq);


				//res = res.OrderBy(x=> x.hora).ToList();
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

				strrq = new clsConsultas().ResumenEstres(SelEstablo.LATITUD.ToString(), SelEstablo.LONGITUD.ToString());
				var resestres = JsonConvert.DeserializeObject<List<clsResumenEstres>>(strrq);

				foreach (var item in resestres)
				{
					if (item.Concepto.Equals("SIN ESTRES"))
					{
						H1 = item.Horas.ToString();
						P1 = item.Porc.ToString();
					}
					if (item.Concepto.Equals("ESTRES LIGERO"))
					{
						H2 = item.Horas.ToString();
						P2 = item.Porc.ToString();
					}
					if (item.Concepto.Equals("ESTRES MODERADO"))
					{
						H3 = item.Horas.ToString();
						P3 = item.Porc.ToString();
					}
					if (item.Concepto.Equals("ESTRES ALTO"))
					{
						H4 = item.Horas.ToString();
						P4 = item.Porc.ToString();
					}
					if (item.Concepto.Equals("PELIGRO"))
					{
						H5 = item.Horas.ToString();
						P5 = item.Porc.ToString();
					}
					if (item.Concepto.Equals("HORAS DE ESTRES"))
					{
						H6 = item.Horas.ToString();
						P6 = item.Porc.ToString();
					}



				}

				await Task.Delay(1500);
				IsBusy = false;
				IsOne = true;
			}
			catch (Exception ex)
			{
				IsBusy = false;
				ErrorPopWsMsg = ex.Message;
				ErrorPopWsMsg = "Intente de nuevo porfavor";
				ShowPopErrorWs = true;
			}


		}

		void ChangeEstablo()
		{
			IsBusy = true;
			IsOne = false;
			LoadPropiedades();
			
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

		void LeftConsulta()
		{
			IsBusy = true;
			IsOne = false;
			fecha = fecha.AddDays(-1);
			Date = fecha.ToString("dd/MM/yyyy");
			LoadPropiedades();
			
		}

		void RightConsulta()
		{
			IsBusy = true;
			IsOne = false;
			fecha = fecha.AddDays(1);
			Date = fecha.ToString("dd/MM/yyyy");
			LoadPropiedades();
		}
		Boolean Pago()
		{
			
				return true;
			
		}

		#endregion
	}
}