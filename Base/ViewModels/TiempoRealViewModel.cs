using Base.Models;
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

			CommandConsultar = new Command(LoadPropiedades, BlockButton);
			CommandConsultarPropiedad = new Command(ChangePropiedad, BlockButton);
		}



		#region Variables
		string nombre = string.Empty;
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
		public ICommand CommandConsultarPropiedad { get; }


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
				LoadData();
			}

		}

		async void LoadData()
		{
			if (Pago())
			{			

				Items.Clear();
				Establos.Clear();

				var elemento = new clsTiempoReal();
				var establo = new clsEstablo();

				for (int i = 0; i <= 15; i++)
				{
					elemento = new clsTiempoReal();
					elemento.Id = i.ToString();
					elemento.Corral = "Corral "+i.ToString();
					elemento.Temperatura = (32+i).ToString();
					elemento.Humedad = (42 + i).ToString();
					elemento.ITH = (80 + i).ToString();

					establo = new clsEstablo();
					//establo.Id = i.ToString();
					//establo.Nombre = "Establo " + i.ToString();

					Items.Add(elemento);
					Establos.Add(establo);
				}

				SelEstablo = Establos.Where(x => x.ESTABLO_ID.Equals(Preferences.Get("IdEstablo", "1"))).FirstOrDefault();

			}
			else
			{
				await Task.Delay(100);
				ShowPopUp = true;
			}

		}

		void LoadPropiedades()
		{
			Preferences.Set("IdEstablo", SelEstablo.ESTABLO_ID);
			Pressed = true;
			//consultar
			Pressed = false;
		}

		void ChangePropiedad()
		{
			Pressed = true;
			// cambiar el Items
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