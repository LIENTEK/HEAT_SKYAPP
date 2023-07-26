using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
	public class clsConsultas
	{
		public clsConsultas()
		{

		}
		public string VersionActual(string plataforma, string version, string numberCode, string amb)
		{
			string jsnversion =
				"{" +
					"plataforma:'" + plataforma + "'," +
					"version:'" + version + "'," +
					"compilacion:'" + numberCode + "'," +
					"ambiente:'" + amb + "'" +
				"}";

			clsRequestHttp req = new clsRequestHttp();
			req.URI = "Versiones/PostVersion";
			req.JsonData = jsnversion;
			string result = req.Requestform().Result;
			return result;
		}

		public string ObtenerParametros()
		{
			string jsnversion = " ";
            clsRequestHttp req = new clsRequestHttp();
            req.JsonData = jsnversion;
            req.URI = "/ParametrosClima";
            string result = req.RequestformHeatsky().Result;
            return result;
        }

        public string ObtenerAllData(string lat, string lon)
        {
            string jsnversion = "lat="+lat+"&lon="+lon;
            clsRequestHttp req = new clsRequestHttp();
            req.JsonData = jsnversion;
            req.URI = "/ReporteMeteorologico";
            string result = req.RequestformHeatsky().Result;
            return result;
        }

		public string DatosChart(string lat, string lon)
		{
			string jsnversion = "lat=" + lat + "&lon=" + lon;
			clsRequestHttp req = new clsRequestHttp();
			req.JsonData = jsnversion;
			req.URI = "/GraficaClima24";
			string result = req.RequestformHeatsky().Result;
			return result;
		}

	}
}
