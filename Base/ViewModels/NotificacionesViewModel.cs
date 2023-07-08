using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.ViewModels
{
	public class NotificacionesViewModel : BaseViewModel
	{
		public NotificacionesViewModel() {
			Title = "Notificaciones";
			IsReady = true;
			IsOne = false;
			ShowNotificaciones();
		}

		async void ShowNotificaciones()
		{
			await Task.Delay(1000);
			IsReady=false;
			IsOne=true;
		}
	}
}
