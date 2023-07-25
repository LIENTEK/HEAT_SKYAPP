using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
	public class clsVersionApp
	{
		public string Amb { get;set; }
		public string Id { get; set; }
		public string Plataforma { get; set; }
		public string Version { get; set; }
		public string Compilacion { get; set; }

		public string VersionActual()
		{
			clsRequestHttp req = new clsRequestHttp();
			req.URI = clsUriWs.ApiVersion;
			req.JsonData = Newtonsoft.Json.JsonConvert.SerializeObject(this);
			string result = req.Requestform().Result;
			return result;
		}

		public string VersionActualJson()
		{
			string jsnversion =
				"{" +
					"plataforma:'" + Plataforma + "'," +
					"version:'" + Version + "'," +
					"compilacion:'" + Compilacion + "'," +
					"ambiente:'" + Amb + "'" +
				"}";

			clsRequestHttp req = new clsRequestHttp();
			req.URI = clsUriWs.ApiVersion;
			req.JsonData = jsnversion;
			string result = req.Requestform().Result;
			return result;
		}
	}
}
