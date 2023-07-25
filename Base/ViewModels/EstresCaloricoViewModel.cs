using Base.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using static Base.Models.clsChart;

namespace Base.ViewModels
{
	public class EstresCaloricoViewModel: BaseViewModel
	{
		public clsChart Hoy { get; set; }
		//public IList<clsValores> Temperatura { get; set; }
		//public ObservableCollection<> Humedad { get; set; }
		//public ObservableCollection<> ITH { get; set; }
		public ObservableCollection<Item> UV { get; set; }
		public ObservableCollection<Item> Items { get; set; }

		public EstresCaloricoViewModel() {

			Establos = new ObservableCollection<clsEstablo>();
			CommandConsultar = new Command(LoadPropiedades, BlockButton);

			LoadData();
		}



		#region Variables
		string nombre = string.Empty;
		string date;
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

		public clsEstablo SelEstablo
		{
			get => this.selEstablo;
			set => SetProperty(ref this.selEstablo, value);
		}
		#endregion

		#region Commands
		public ICommand CommandConsultar { get; }

		clsEstablo selEstablo;




		#endregion

		#region Funciones

		public void OnAppearing()
		{
			LoadData();
		}

		async void LoadData()
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
				
				if (Pago())
				{
					Date = DateTime.Now.ToString("dd/MM/yyyy");
					Establos.Clear();
					//Temperatura = null;
					//Humedad = null;
					//ITH = null;
					//UV = null;
					//Hoy = new clsChart();

					var establo = new clsEstablo();

					for (int i = 0; i <= 15; i++)
					{
						establo = new clsEstablo();
						

						Establos.Add(establo);

					}

					SelEstablo = Establos.Where(x => x.ESTABLO_ID.Equals( Preferences.Get("IdEstablo", "1"))).FirstOrDefault();

					//Temperatura = Hoy.LTEMPERATURA.Values;
					//ITH = Hoy.LITH;
					UV = new ObservableCollection<Item>();
					//Humedad = Hoy.LHUMEDAD;
					Items = new ObservableCollection<Item>();
					DateTime baseDate = DateTime.Today;
					var x = new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description = "This is an item description.", StartTime = baseDate.AddHours(1), EndTime = baseDate.AddHours(2), Value = 17.098 };
					Items.Add(x);
					UV.Add(x);
					x = new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description = "This is an item description.", StartTime = baseDate.AddHours(1), EndTime = baseDate.AddHours(2), Value = 27.098 };
					Items.Add(x);
					UV.Add(x);
					x = new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description = "This is an item description.", StartTime = baseDate.AddHours(1), EndTime = baseDate.AddHours(2), Value = 37.098 };
					Items.Add(x);
					UV.Add(x);
				}
				else
				{
					await Task.Delay(100);
					ShowPopUp = true;
				}
			}
		}

		void LoadPropiedades()
		{
			Preferences.Set("IdEstablo", SelEstablo.ESTABLO_ID);
			Pressed = true;
			//consultar
			
			Pressed = false;
		}

		Boolean Pago()
		{
			if (Preferences.Get("IdEstablo", "1").Equals("15"))
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		#endregion
	}
}