using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Base.ViewModels
{
	public class PagoViewModel:BaseViewModel, IQueryAttributable
	{
		public PagoViewModel() {
			Title = "Pagos";
			nombre = "RUBEN ISAI NAVARRETE DIAZ";
			msg = "No hay pagos disponibles";
			RevisarPago();

			CommandPagar = new Command(OnClickPago, BlockButton);
			ShowPopUpCommand = new Command(ShowPopUpPagar, BlockButton);

		}

		string nombre = string.Empty;
		string msg = string.Empty;
		string estatus = string.Empty;
		string monto = string.Empty;
		Color color = Colors.Black;
		public ObservableCollection<string> DATA { get; }

		public string ID { get; set; }

		public Command CommandPagar { get; }

		public Command ShowPopUpCommand { get; }

		public Color Color
		{
			get => this.color;
			set => SetProperty(ref this.color, value);
		}

		public string Nombre {
			get => this.nombre;
			set => SetProperty(ref this.nombre, value);
		}

		public string Msg {
			get => this.msg;
			set => SetProperty(ref this.msg, value);
		}

		public string Monto
		{
			get => this.monto;
			set => SetProperty(ref this.monto, value);
		}

		public string Estatus {
			get => this.estatus;
			set => SetProperty(ref this.estatus, value);
		}

		public void ApplyQueryAttributes(IDictionary<string, object> query)
		{
			//ID = HttpUtility.UrlDecode(query["ID"] as string);
			
		}
		void ShowPopUpPagar()
		{
			if (ShowPopUp)
			{
				ShowPopUp = false;
			}
			else
			{
				ShowPopUp = true;
			}
		}
		void RevisarPago()
		{
			estatus = "Pendiente de pago";
			Monto = "$ 120,000.00";
			if (estatus.Equals("Pagado"))
			{
				Color = Colors.Green;
				IsOne = false;
			}
			else
			{
				IsOne = true;
				Color = Colors.Red;
			}
			
		}

		async void OnClickPago()
		{
			if (true) {

				ShowPopUp = false;
				ExitoPopWsMsg = "Pago realizado con exito";
				ShowPopExitoWs = true;
				await Task.Delay(1000);
				ShowPopExitoWs = false;
			}
			else
			{
				ShowPopUp = false;
				ErrorPopWsMsg = "Problemas para generar el pago";
				ShowPopErrorWs = true;
				await Task.Delay(1000);
				ShowPopErrorWs = false;
			}
		}


	}
}
