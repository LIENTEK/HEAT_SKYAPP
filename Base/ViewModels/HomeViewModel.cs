using Microsoft.Maui.Controls.Internals;
using System.Windows.Input;
using Plugin.Maui.Audio;
using System;
using Plugin.LocalNotification;
using Base.Models;
using System.Collections.ObjectModel;
using DevExpress.Maui.DataForm;
using DevExpress.Maui.Editors;
using System.Collections.Generic;
using BitMiracle.LibTiff.Classic;
using System.Linq;

namespace Base.ViewModels
{
	public class HomeViewModel : BaseViewModel
	{
		public HomeViewModel()
		{
			Items = new ObservableCollection<clsPropiedadesMet>();
			Establos = new ObservableCollection<clsEstablo>();
			Propiedades = new ObservableCollection<clsPropiedades>();

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
		public DateTime FechaHoy
		{
			get => DateTime.Now;
		}
		public DateTime FechaSemana
		{
			get => DateTime.Now.AddDays(7);
		}

		public ObservableCollection<clsPropiedadesMet> Items { get;  set; }
		public ObservableCollection<clsEstablo> Establos { get;  set; }
		public ObservableCollection<clsPropiedades> Propiedades { get;  set; }
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
				IsOne= false;
				LoadData();
			}
			
		}

		async void LoadData()
		{
			if (Pago())
			{
				
				Items.Clear();
				Establos.Clear();
				Propiedades.Clear();

				var elemento = new clsPropiedadesMet();
				var establo = new clsEstablo();
				var propiedad = new clsPropiedades();

				for (int i = 0; i <= 15; i++)
				{
					elemento = new clsPropiedadesMet();
					elemento.Hora = DateTime.Now.AddHours(i).ToString("HH:mm");
					elemento.Fecha = DateTime.Now.AddDays(i).ToString("dd/MM");
					elemento.Valor = i.ToString();

					establo = new clsEstablo();
					establo.Id = i.ToString();
					establo.Nombre ="Establo "+ i.ToString();

					propiedad = new clsPropiedades();
					propiedad.Id = i.ToString();
					propiedad.Propiedad = "Propiedad "+i.ToString();

					Items.Add(elemento);
					Establos.Add(establo);
					Propiedades.Add(propiedad);
				}

				SelEstablo = Establos.Where(x => x.Id == Preferences.Get("IdEstablo","1")).FirstOrDefault();
				SelPropiedad = Propiedades.Where(x => x.Id == "1").FirstOrDefault();

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
			Pressed= false;
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