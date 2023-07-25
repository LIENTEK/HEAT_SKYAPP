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

                try
				{
					clsUsuario  objuser = new clsUsuario();
					string obj = Preferences.Get("objuser", "");
					objuser = Newtonsoft.Json.JsonConvert.DeserializeObject<clsUsuario>(obj);

                    var elemento = new clsPropiedadesMet();
                    var establo = new clsEstablo();
                    var propiedad = new clsPropiedades();

					//obtener establos
                    var rqestablo = new clsEstablo();
                    var strrq = rqestablo.ObtenerEstablos(objuser.USUARIO_ID.ToString());

                    var responseestablos = JsonConvert.DeserializeObject<List<clsEstablo>>(strrq);

                    if (responseestablos.Count >1)
                    {
						foreach (var item in responseestablos)
						{
							establo = new clsEstablo();
							establo.ESTABLO_ID = item.ESTABLO_ID;
							establo.NOMBRE =item.NOMBRE;
                            Establos.Add(establo);
                        }

					}
					else
					{
						if (responseestablos[0].ESTABLO_ID==-1)
						{
							ErrorPopWsMsg= rqestablo.NOMBRE;
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
                            propiedad.Propiedad =item;
                            Propiedades.Add(propiedad);
                        }
                    }
					catch
					{
                        throw new InvalidOperationException("No se descargaron las propiedades meteorologicas");
                    }

                    SelEstablo = Establos.Where(x => x.ESTABLO_ID.Equals(Preferences.Get("IdEstablo", 0))).FirstOrDefault();
                    SelPropiedad = Propiedades.Where(x => x.Id.Equals(Preferences.Get("Propiedad", ""))).FirstOrDefault();
					LoadPropiedades();

                }
				catch(Exception ex)
				{
					
				}
				

			}
			else
			{
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
			Preferences.Set("IdEstablo", SelEstablo.ESTABLO_ID);
			Pressed = true;
            var strrq = new clsConsultas().ObtenerAllData(SelEstablo.LATITUD.ToString(), SelEstablo.LONGITUD.ToString());
			strrq = strrq.Substring(1, strrq.Length-2);
            var arrofarr = strrq.TrimStart('[').TrimEnd(']').Split(',');
            var rqalldata = JsonConvert.DeserializeObject<List<List<clsPropiedadesMet>>>(strrq);


            Pressed = false;
		}

		void ChangePropiedad()
		{
            Preferences.Set("Propiedad", SelPropiedad.Id.ToString());
            Pressed = true;
			// cambiar el Items
			Pressed = false;
		}

		Boolean Pago()
		{
			
			//revisar si esta pagada la app
				return true;
			
		}

		#endregion
	}
}