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
using Newtonsoft.Json;
using System.Data;
using DevExpress.Maui.DataGrid;

namespace Base.ViewModels
{
	public class HomeViewModel : BaseViewModel
	{
		public HomeViewModel()
		{
			Items = new ObservableCollection<clsItemsMet>();
			Establos = new ObservableCollection<clsEstablo>();
			Propiedades = new ObservableCollection<clsPropiedades>();
			banderaleiado = 0;
			CommandConsultar = new Command(ChangeEstablo);
			CommandConsultarPropiedad = new Command(ChangePropiedad);
			LoadCommand = new Command(Refresh);
		}



		#region Variables
		string nombre = string.Empty;
		string d1=string.Empty;
		string d2 = string.Empty;
		string d3 = string.Empty;
		string d4 = string.Empty;
		string d5 = string.Empty;
		string d6 = string.Empty;
		string d7 = string.Empty;
		clsPropiedadesMet[][] res;
		int banderaleiado = 0;
		#endregion

		#region Propiedades bool

		#endregion

		#region Propiedades string
		public string Nombre
		{
			get => this.nombre;
			set => SetProperty(ref this.nombre, value);
		}

		public string D1
		{
			get => this.d1;
			set => SetProperty(ref this.d1, value);
		}
		public string D2
		{
			get => this.d2;
			set => SetProperty(ref this.d2, value);
		}
		public string D3
		{
			get => this.d3;
			set => SetProperty(ref this.d3, value);
		}
		public string D4
		{
			get => this.d4;
			set => SetProperty(ref this.d4, value);
		}
		public string D5
		{
			get => this.d5;
			set => SetProperty(ref this.d5, value);
		}
		public string D6
		{
			get => this.d6;
			set => SetProperty(ref this.d6, value);
		}
		public string D7
		{
			get => this.d7;
			set => SetProperty(ref this.d7, value);
		}

		public DateTime FechaHoy
		{
			get => DateTime.Now;
		}
		public DateTime FechaSemana
		{
			get => DateTime.Now.AddDays(7);
		}

		public ObservableCollection<clsItemsMet> Items { get;  set; }
		public ObservableCollection<clsEstablo> Establos { get;  set; }
		public ObservableCollection<clsPropiedades> Propiedades { get;  set; }
		#endregion

		#region Commands
		public ICommand LoadCommand { get; }
		public ICommand CommandCalendario { get; }
		public ICommand CommandConsultar { get; }
		public ICommand CommandConsultarPropiedad { get; }

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
				IsOne= false;
				IsBusy = true;
				ThFaillog = new Thread(new ThreadStart(LoadData));
				ThFaillog.Start();
			}
			
		}

		async void LoadData()
		{
			if (Pago())
			{
					Establos.Clear();
					Propiedades.Clear();
					Items.Clear();

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


						//obtener proiedades
						strrq = new clsConsultas().ObtenerParametros();
						var rqpropiedad = JsonConvert.DeserializeObject<List<string>>(strrq);

						try
						{
							foreach (string item in rqpropiedad)
							{
								propiedad = new clsPropiedades();
								propiedad.Id = item;
								propiedad.Propiedad = item;
								Propiedades.Add(propiedad);
							}
						}
						catch
						{
							throw new InvalidOperationException("No se descargaron las propiedades meteorologicas");
						}

						if (banderaleiado==0)
						{
							SelPropiedad = Propiedades.Where(x => x.Id.Equals(Preferences.Get("Propiedad", "Temperatura °C"))).FirstOrDefault();
						}
						
						SelEstablo = Establos.Where(x => x.ESTABLO_ID.Equals(Preferences.Get("IdEstablo", 0))).FirstOrDefault();


					}
					catch (Exception ex)
					{
						IsBusy = false;
						ErrorPopWsMsg = ex.Message;
						ShowPopErrorWs = true;
						ThFaillog = new Thread(new ThreadStart(hidePopUp));
						ThFaillog.Start();
					}


					//LoadPropiedades();
				
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
                await Task.Delay(7000);
            }
            else
            {
                await Task.Delay(5000);
            }
            ShowPopErrorWs = false;
        }

        void LoadPropiedades()
		{
			Items.Clear();

			try
			{

				Preferences.Set("IdEstablo", SelEstablo.ESTABLO_ID);
				var strrq = new clsConsultas().ObtenerAllData(SelEstablo.LATITUD.ToString(), SelEstablo.LONGITUD.ToString());
				res = JsonConvert.DeserializeObject<clsPropiedadesMet[][]>(strrq);


				D1 = res[0][0].FechaHora;
				D2 = res[1][0].FechaHora;
				D3 = res[2][0].FechaHora;
				D4 = res[3][0].FechaHora;
				D5 = res[4][0].FechaHora;
				D6 = res[5][0].FechaHora;
				D7 = res[6][0].FechaHora;
				var elemento = new clsItemsMet();

				if (SelPropiedad.Id.Equals("Temperatura °C"))
				{
					for (int i = 0; i <= 23; i++)
					{
						elemento = new clsItemsMet();
						elemento.Hora = res[0][i].Hora.ToString();
						elemento.Valor1 = res[0][i].temp_c.ToString();
						elemento.Valor2 = res[1][i].temp_c.ToString();
						elemento.Valor3 = res[2][i].temp_c.ToString();
						elemento.Valor4 = res[3][i].temp_c.ToString();
						elemento.Valor5 = res[4][i].temp_c.ToString();
						elemento.Valor6 = res[5][i].temp_c.ToString();
						elemento.Valor7 = res[6][i].temp_c.ToString();
						Items.Add(elemento);
					}
				}
				else if (SelPropiedad.Id.Equals("Humedad %"))
				{
					for (int i = 0; i <= 23; i++)
					{
						elemento = new clsItemsMet();
						elemento.Hora = res[0][i].Hora.ToString();
						elemento.Valor1 = res[0][i].humidity.ToString();
						elemento.Valor2 = res[1][i].humidity.ToString();
						elemento.Valor3 = res[2][i].humidity.ToString();
						elemento.Valor4 = res[3][i].humidity.ToString();
						elemento.Valor5 = res[4][i].humidity.ToString();
						elemento.Valor6 = res[5][i].humidity.ToString();
						elemento.Valor7 = res[6][i].humidity.ToString();
						Items.Add(elemento);
					}
				}
				else if (SelPropiedad.Id.Equals("ITH"))
				{
					for (int i = 0; i <= 23; i++)
					{
						elemento = new clsItemsMet();
						elemento.Hora = res[0][i].Hora.ToString();
						elemento.Valor1 = res[0][i].ITH.ToString();
						elemento.Valor2 = res[1][i].ITH.ToString();
						elemento.Valor3 = res[2][i].ITH.ToString();
						elemento.Valor4 = res[3][i].ITH.ToString();
						elemento.Valor5 = res[4][i].ITH.ToString();
						elemento.Valor6 = res[5][i].ITH.ToString();
						elemento.Valor7 = res[6][i].ITH.ToString();
						Items.Add(elemento);
					}
				}
				else if (SelPropiedad.Id.Equals("Nubosidad %"))
				{
					for (int i = 0; i <= 23; i++)
					{
						elemento = new clsItemsMet();
						elemento.Hora = res[0][i].Hora.ToString();
						elemento.Valor1 = res[0][i].cloud.ToString();
						elemento.Valor2 = res[1][i].cloud.ToString();
						elemento.Valor3 = res[2][i].cloud.ToString();
						elemento.Valor4 = res[3][i].cloud.ToString();
						elemento.Valor5 = res[4][i].cloud.ToString();
						elemento.Valor6 = res[5][i].cloud.ToString();
						elemento.Valor7 = res[6][i].cloud.ToString();
						Items.Add(elemento);
					}
				}
				else if (SelPropiedad.Id.Equals("Prob. Lluvia %"))
				{
					for (int i = 0; i <= 23; i++)
					{
						elemento = new clsItemsMet();
						elemento.Hora = res[0][i].Hora.ToString();
						elemento.Valor1 = res[0][i].chance_of_rain.ToString();
						elemento.Valor2 = res[1][i].chance_of_rain.ToString();
						elemento.Valor3 = res[2][i].chance_of_rain.ToString();
						elemento.Valor4 = res[3][i].chance_of_rain.ToString();
						elemento.Valor5 = res[4][i].chance_of_rain.ToString();
						elemento.Valor6 = res[5][i].chance_of_rain.ToString();
						elemento.Valor7 = res[6][i].chance_of_rain.ToString();
						Items.Add(elemento);
					}
				}
				else if (SelPropiedad.Id.Equals("Indice UV"))
				{
					for (int i = 0; i <= 23; i++)
					{
						elemento = new clsItemsMet();
						elemento.Hora = res[0][i].Hora.ToString();
						elemento.Valor1 = res[0][i].uv.ToString();
						elemento.Valor2 = res[1][i].uv.ToString();
						elemento.Valor3 = res[2][i].uv.ToString();
						elemento.Valor4 = res[3][i].uv.ToString();
						elemento.Valor5 = res[4][i].uv.ToString();
						elemento.Valor6 = res[5][i].uv.ToString();
						elemento.Valor7 = res[6][i].uv.ToString();
						Items.Add(elemento);
					}
				}
				else if (SelPropiedad.Id.Equals("Velocidad Viento km/hra"))
				{
					for (int i = 0; i <= 23; i++)
					{
						elemento = new clsItemsMet();
						elemento.Hora = res[0][i].Hora.ToString();
						elemento.Valor1 = res[0][i].wind_kph.ToString();
						elemento.Valor2 = res[1][i].wind_kph.ToString();
						elemento.Valor3 = res[2][i].wind_kph.ToString();
						elemento.Valor4 = res[3][i].wind_kph.ToString();
						elemento.Valor5 = res[4][i].wind_kph.ToString();
						elemento.Valor6 = res[5][i].wind_kph.ToString();
						elemento.Valor7 = res[6][i].wind_kph.ToString();
						Items.Add(elemento);
					}
				}

				banderaleiado = 1;
				IsBusy=false;
			}
			catch(Exception ex)
			{
				IsBusy = false;
				ErrorPopWsMsg = ex.Message;
				ShowPopErrorWs = true;
				ThFaillog = new Thread(new ThreadStart(hidePopUp));
				ThFaillog.Start();
			}
			
		}

		async void Refresh()
		{
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
				Items.Clear();
				await Task.Delay(1000);
				IsBusy = true;
				banderaleiado = 0;
				LoadPropiedades();
			}
			
		}

		void ChangePropiedad()
		{
            Preferences.Set("Propiedad", SelPropiedad.Id.ToString());
			if (banderaleiado==1) {
				ThFaillog = new Thread(new ThreadStart(LoadPropiedades));
				ThFaillog.Start();
			}
		}

		void ChangeEstablo()
		{
			ThFaillog = new Thread(new ThreadStart(LoadPropiedades));
			ThFaillog.Start();
		}

		Boolean Pago()
		{
			
			//revisar si esta pagada la app
				return true;
			
		}

		#endregion
	}
}