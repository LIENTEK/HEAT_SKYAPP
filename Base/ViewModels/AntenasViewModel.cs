using Base.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Base.ViewModels
{
    public class AntenasViewModel : BaseViewModel
	{
		public AntenasViewModel()
		{
			Items = new ObservableCollection<clsAntenas>();
			Establos = new ObservableCollection<clsEstablo>();
			CommandConsultar = new Command(LoadPropiedades);
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


		public ObservableCollection<clsAntenas> Items { get; set; }
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

				var strrq = new clsConsultas().ObtenerAntenas(SelEstablo.ESTABLO_ID.ToString());
				var res = JsonConvert.DeserializeObject<List<clsAntenas>>(strrq);

				foreach (var item in res)
				{
					Items.Add(item);
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

		async void Refresh()
		{
			Items.Clear();
			await Task.Delay(1000);
			IsBusy = true;
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