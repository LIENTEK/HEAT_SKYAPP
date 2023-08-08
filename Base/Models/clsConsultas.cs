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
		public string VersionActual(string plataforma, string amb)
		{
			string jsnversion = "SO=" + plataforma;
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/Version";
			req.JsonData = jsnversion;
			string result = req.Requestform().Result;
			return result;
		}

		public string OlvidoContraseña(string user)
		{
			string jsnversion = "usr=" + user;
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/ContraseñaOlvidada";
			req.JsonData = jsnversion;
			string result = req.Requestform().Result;
			return result;
		}

		public string CambiarContraseña(string user, string pwd)
		{
			string jsnversion = "usr=" + user+"&newpwd="+pwd;
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/CambiarContraseña";
			req.JsonData = jsnversion;
			string result = req.Requestform().Result;
			return result;
		}

		public string GetToken()
		{
			clsRequestHttp req = new clsRequestHttp();
			req.URI = clsUriWs.GetTokenPassword;
			req.JsonData = "";
			string result = req.Requestform().Result;
			return result;
		}

		public string UpdatePassword()
		{
			clsRequestHttp req = new clsRequestHttp();
			req.URI = clsUriWs.UpdatePassword;
			req.JsonData = "";
			string result = req.Requestform().Result;
			return result;
		}

		public string LogInWs(string user, string pwd)
		{

			string jsnversion = "usr=" + user + "&pwd=" + pwd;
			clsRequestHttp req = new clsRequestHttp();
			req.URI = clsUriWs.ApiLogIn;
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
			string jsnversion = "lat=" + lat + "&lon=" + lon;
			clsRequestHttp req = new clsRequestHttp();
			req.JsonData = jsnversion;
			req.URI = "/ReporteMeteorologico";
			string result = req.RequestformHeatsky().Result;
			return result;
		}

		public string DatosChart(string lat, string lon,string date)
		{
			string jsnversion = "lat=" + lat + "&lon=" + lon + "&fecha=" + date;
			clsRequestHttp req = new clsRequestHttp();
			req.JsonData = jsnversion;
			req.URI = "/GraficaClima24";
			string result = req.RequestformHeatsky().Result;
			return result;
		}

		public string ResumenEstres(string lat, string lon)
		{
			string jsnversion = "lat=" + lat + "&lon=" + lon;
			clsRequestHttp req = new clsRequestHttp();
			req.JsonData = jsnversion;
			req.URI = "/ResumenEstres";
			string result = req.RequestformHeatsky().Result;
			return result;
		}

		public string ObtenerEstablos(string id)
		{

			string jsnversion = "usuario_id=" + id;
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/EstablosUsuario";
			req.JsonData = jsnversion;
			string result = req.Requestform().Result;
			return result;
		}

		public string ObtenerAntenas(string id)
		{

			string jsnversion = "estid=" + id;
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/EstatusAntenas";
			req.JsonData = jsnversion;
			string result = req.RequestformCOW().Result;
			return result;
		}

		public string ObtenerTrampas(string id)
		{

			string jsnversion = "estid=" + id;
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/EstatusTrampas";
			req.JsonData = jsnversion;
			string result = req.RequestformCOW().Result;
			return result;
		}

		public string ObtenerCorrales(string id)
		{

			string jsnversion = "estid=" + id;
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/MonitoreoTemp";
			req.JsonData = jsnversion;
			string result = req.RequestformCOW().Result;
			return result;
		}

		public string ObtenerReporte(string id)
		{

			string jsnversion = "idest=" + id;
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/ReporteSemanal";
			req.JsonData = jsnversion;
			string result = req.RequestformHeatsky().Result;
			return result;
		}
	}
}
