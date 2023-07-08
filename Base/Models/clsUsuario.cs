using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
	public class clsUsuario
	{
		public string Amb { get; set; }
		public string Id { get; set; }
		public string User { get; set; }
		public string Password { get; set; }
		public string Activo { get; set; }

		public clsUsuario() { 
		
		}

		public string LogInWs()
		{
			clsRequestHttp req = new clsRequestHttp();
			req.URI = clsUriWs.ApiLogIn;
			req.JsonData = Newtonsoft.Json.JsonConvert.SerializeObject(this);
			string result = req.RequestJSON().Result;
			return result;
		}

		public string LogInWsJson()
		{
			string jsnversion =
				"{" +
					"user:'" + User + "'," +
					"pwd:'" + Password + "'," +
					"ambiente:'" + Amb + "'" +
				"}";

			clsRequestHttp req = new clsRequestHttp();
			req.URI = clsUriWs.ApiLogIn;
			req.JsonData = jsnversion;
			string result = req.RequestJSON().Result;
			return result;
		}

	}
}
