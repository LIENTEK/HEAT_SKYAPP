using Base.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Base.ViewModels
{
	public class RecomendacionViewModel : BaseViewModel
	{
		public RecomendacionViewModel() {
			Items = new ObservableCollection<clsRecomendacion>();
			Establos = new ObservableCollection<clsEstablo>();

			CommandConsultar = new Command(LoadPropiedades, BlockButton);
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


		public ObservableCollection<clsRecomendacion> Items { get; set; }
		public ObservableCollection<clsEstablo> Establos { get; set; }
		#endregion

		#region Commands
		public ICommand CommandCalendario { get; }
		public ICommand CommandConsultar { get; }


		clsEstablo selEstablo;
		public clsEstablo SelEstablo
		{
			get => this.selEstablo;
			set => SetProperty(ref this.selEstablo, value);
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

				var elemento = new clsRecomendacion();
				var establo = new clsEstablo();

				for (int i = 0; i <= 15; i++)
				{
					elemento = new clsRecomendacion();
					elemento.Id = i.ToString();
					elemento.Titulo = "Titulo " + i.ToString();
					elemento.Recomendacion = "Esto es la recomendacion: "+ i.ToString() +" para el establo: "+ Preferences.Get("IdEstablo", "1");

					establo = new clsEstablo();
					establo.Id = i.ToString();
					establo.Nombre = "Establo " + i.ToString();

					Items.Add(elemento);
					Establos.Add(establo);
				}

				SelEstablo = Establos.Where(x => x.Id == Preferences.Get("IdEstablo", "1")).FirstOrDefault();

			}
			else
			{
				await Task.Delay(100);
				ShowPopUp = true;
			}

		}

		void LoadPropiedades()
		{
			Preferences.Set("IdEstablo", SelEstablo.Id);
			Pressed = true;
			//consultar
			Pressed = false;
		}


		Boolean Pago()
		{
			if (Preferences.Get("IdEstablo", "1").Equals("0"))
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