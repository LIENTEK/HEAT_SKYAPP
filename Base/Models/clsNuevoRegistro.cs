using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
	public class clsNuevoRegistro
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string ApeidoPaterno { get; set; }
		public string ApeidoMaterno { get; set; }
		public string Usuario { get; set; }
		public string Password { get; set; }
		public string FechaNacimiento { get; set; }
		public string Telefono { get; set; }

		public clsNuevoRegistro() { 
		
		}

		public string NuevoRegistroWs()
		{
			clsRequestHttp req = new clsRequestHttp();
			req.URI = clsUriWs.ApiNuevoRegistro;
			req.JsonData = Newtonsoft.Json.JsonConvert.SerializeObject(this);
			string result = req.RequestJSON().Result;
			return result;
		}
	}
}
