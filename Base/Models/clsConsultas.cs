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
			string result = req.RequestJSON().Result;
			return result;
		}
		public string ListaClientesUsuario(string usuario, string amb)
		{
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/VisitaCliente/POSTLSTA_CLIENTES";
			string jsnversion =
			   "{" +
				   "usuario:'" + usuario + "'," +
				   "ambiente:'" + amb + "'" +
			   "}";
			req.JsonData = jsnversion;
			string result = req.RequestJSON().Result;
			return result;
		}
		public string ListaTipoVisita(string amb)
		{
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/VisitaCliente/POSTLSTA_TIPOVISITA";
			string jsnversion =
			   "{" +
				   "ambiente:'" + amb + "'" +
			   "}";
			req.JsonData = jsnversion;
			string result = req.RequestJSON().Result;
			return result;
		}
		public string ListaCatResultados(string amb)
		{
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/VisitaCliente/POSTLSTA_CATRESULTADOS";
			string jsnversion =
			   "{" +
				   "ambiente:'" + amb + "'" +
			   "}";
			req.JsonData = jsnversion;
			string result = req.RequestJSON().Result;
			return result;
		}
		public string RegistrarVisita(string amb, string location, string usuario, string idcliente, string idtipo, string descotro)
		{
			string jsnfolio =
				"{" +
					"location:'" + location + "'," +
					"usuario:'" + usuario + "'," +
					"cliente:'" + idcliente + "'," +
					"otro:'" + descotro + "'," +
					"tipo:'" + idtipo + "'," +
					"ambiente:'" + amb + "'" +
				"}";
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/VisitaCliente/POSTREGISTRAR_VISITA";
			req.JsonData = jsnfolio;
			string result = req.RequestJSON().Result;
			return result;
		}
		public string FinalizarVisita(string amb, string location, string usuario, string idvisita, string idresultado, string comentario, string atendio, string date)
		{
			string jsnfolio =
				"{" +
					"location:'" + location + "'," +
					"usuario:'" + usuario + "'," +
					"visita:'" + idvisita + "'," +
					"atendio:'" + atendio + "'," +
					"comentario:'" + comentario + "'," +
					"resultado:'" + idresultado + "'," +
					"date:'" + date + "'," +
					"ambiente:'" + amb + "'" +
				"}";
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/VisitaCliente/POSTFINALIZAR_VISITA";
			req.JsonData = jsnfolio;
			string result = req.RequestJSON().Result;
			return result;
		}
		public string ListaMarcas(string amb)
		{
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/Inventario/POSTINVENTARIOS_MARCAS";
			string jsnversion =
			   "{" +
				   "ambiente:'" + amb + "'" +
			   "}";
			req.JsonData = jsnversion;
			string result = req.RequestJSON().Result;
			return result;
		}
		public string ListaEstilos(string amb, string marca)
		{
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/Inventario/POSTINVENTARIOS_ESTILOS";
			string jsnversion =
			   "{" +
				   "marca:'" + marca + "'," +
				   "ambiente:'" + amb + "'" +
			   "}";
			req.JsonData = jsnversion;
			string result = req.RequestJSON().Result;
			return result;
		}

		public string ListaEstilosAll(string amb)
		{
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/Inventario/POSTINVENTARIOS_ESTILOSALL";
			string jsnversion =
			   "{" +
				   "ambiente:'" + amb + "'" +
			   "}";
			req.JsonData = jsnversion;
			string result = req.RequestJSON().Result;
			return result;
		}

		public string Estilo(string amb, string marca, string color, string estilo, string desc)
		{
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/Inventario/POSTINVENTARIOS_ESTILO";
			string jsnversion =
			   "{" +
				   "marca:'" + marca + "'," +
				   "ambiente:'" + amb + "'," +
				   "desc:'" + desc + "'," +
				   "estilo:'" + estilo + "'," +
				   "color:'" + color + "'" +
			   "}";
			req.JsonData = jsnversion;
			string result = req.RequestJSON().Result;
			return result;
		}

		public string EnviarPreseleccion(string amb, string location, string usuario, string idcliente, string idtipo, string lista)
		{
			string jsnfolio =
				"{" +
					"location:'" + location + "'," +
					"usuario:'" + usuario + "'," +
					"cliente:'" + idcliente + "'," +
					"lista:'" + lista + "'," +
					"tipo:'" + idtipo + "'," +
					"ambiente:'" + amb + "'" +
				"}";
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/Inventario/POSTENVIAR_PRESELECCION";
			req.JsonData = jsnfolio;
			string result = req.RequestJSON().Result;
			return result;
		}

		public string GenVales(string amb, string location, string usuario, string idcliente, string idtipo, string lista, string almacen, string zona, string pares, string vendedor)
		{
			string jsnfolio =
				"{" +
					"location:'" + location + "'," +
					"usuario:'" + usuario + "'," +
					"cliente:'" + idcliente + "'," +
					"lista:'" + lista + "'," +
					"tipo:'" + idtipo + "'," +
					"almacen:'" + almacen + "'," +
					"zona:'" + zona + "'," +
					"vendedor:'" + vendedor + "'," +
					"pares:'" + pares + "'," +
					"ambiente:'" + amb + "'" +
				"}";
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/ValesDiscalse/POST_GENERARVALESDISCALSE";
			req.JsonData = jsnfolio;
			string result = req.RequestJSON().Result;
			return result;
		}

		public string ReenviarVales(string amb, string location, string usuario, string idcliente, string idvale, string lista, string almacen, string zona, string pares, string vendedor)
		{
			string jsnfolio =
				"{" +
					"location:'" + location + "'," +
					"usuario:'" + usuario + "'," +
					"cliente:'" + idcliente + "'," +
					"lista:'" + lista + "'," +
					"vale:'" + idvale + "'," +
					"almacen:'" + almacen + "'," +
					"zona:'" + zona + "'," +
					"vendedor:'" + vendedor + "'," +
					"pares:'" + pares + "'," +
					"ambiente:'" + amb + "'" +
				"}";
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/ValesDiscalse/POST_ENVIARVALESDISCALSE";
			req.JsonData = jsnfolio;
			string result = req.RequestJSON().Result;
			return result;
		}

		public string InfoCotizacion(string amb, string usuario, string cliente, string almacen)
		{
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/Cotizacion/POST_COTIZACIONCLIENTES";
			string jsnversion =
			   "{" +
					"usuario:'" + usuario + "'," +
					"cliente:'" + cliente + "'," +
					"almacen:'" + almacen + "'," +
					"ambiente:'" + amb + "'" +
			   "}";
			req.JsonData = jsnversion;
			string result = req.RequestJSON().Result;
			return result;
		}

		public string GenCotizacion(string amb, string location, string usuario, string idcliente, string idtipo, string lista, string almacen, string zona, string data)
		{
			string jsnfolio =
				"{" +
					"location:'" + location + "'," +
					"usuario:'" + usuario + "'," +
					"cliente:'" + idcliente + "'," +
					"lista:'" + lista + "'," +
					"tipo:'" + idtipo + "'," +
					"data:'" + data + "'," +
					"almacen:'" + almacen + "'," +
					"zona:'" + zona + "'," +
					"ambiente:'" + amb + "'" +
				"}";
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/Cotizacion/POST_GENCOTIZACION";
			req.JsonData = jsnfolio;
			string result = req.RequestJSON().Result;
			return result;
		}

		public string ReenviarCotizacion(string amb, string location, string usuario, string idcoti, string almacen, string correo)
		{
			string jsnfolio =
				"{" +
					"location:'" + location + "'," +
					"usuario:'" + usuario + "'," +
					"cotizacion:'" + idcoti + "'," +
					"almacen:'" + almacen + "'," +
					"correo:'" + correo + "'," +
					"ambiente:'" + amb + "'" +
				"}";
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/Cotizacion/POST_ENVCOTIZACION";
			req.JsonData = jsnfolio;
			string result = req.RequestJSON().Result;
			return result;
		}

		public string ListaCotizaciones(string amb, string usuario, string almacen, string estatus)
		{
			string jsnfolio =
				"{" +
					"usuario:'" + usuario + "'," +
					"almacen:'" + almacen + "'," +
					"estatus:'" + estatus + "'," +
					"ambiente:'" + amb + "'" +
				"}";
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/Cotizacion/POST_LISTACOTIZACIONES";
			req.JsonData = jsnfolio;
			string result = req.RequestJSON().Result;
			return result;
		}

		public string InfoPedidos(string amb, string usuario, string cliente, string almacen)
		{
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/Pedido/POST_PEDIDOCLIENTES";
			string jsnversion =
			   "{" +
					"usuario:'" + usuario + "'," +
					"cliente:'" + cliente + "'," +
					"almacen:'" + almacen + "'," +
					"ambiente:'" + amb + "'" +
			   "}";
			req.JsonData = jsnversion;
			string result = req.RequestJSON().Result;
			return result;
		}

		public string GenPedido(string amb, string location, string usuario, string idcliente, string idtipo, string lista, string almacen, string zona, string data)
		{
			string jsnfolio =
				"{" +
					"location:'" + location + "'," +
					"usuario:'" + usuario + "'," +
					"cliente:'" + idcliente + "'," +
					"lista:'" + lista + "'," +
					"tipo:'" + idtipo + "'," +
					"data:'" + data + "'," +
					"almacen:'" + almacen + "'," +
					"zona:'" + zona + "'," +
					"ambiente:'" + amb + "'" +
				"}";
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/Pedido/POST_GENERARPEDIDO";
			req.JsonData = jsnfolio;
			string result = req.RequestJSON().Result;
			return result;
		}

		public string ReenviarPedido(string amb, string location, string usuario, string idcoti, string almacen, string correo)
		{
			string jsnfolio =
				"{" +
					"location:'" + location + "'," +
					"usuario:'" + usuario + "'," +
					"cotizacion:'" + idcoti + "'," +
					"almacen:'" + almacen + "'," +
					"correo:'" + correo + "'," +
					"ambiente:'" + amb + "'" +
				"}";
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/Pedido/POST_ENVPEDIDO";
			req.JsonData = jsnfolio;
			string result = req.RequestJSON().Result;
			return result;
		}

		public string ListaPedidos(string amb, string usuario, string almacen, string estatus)
		{
			string jsnfolio =
				"{" +
					"usuario:'" + usuario + "'," +
					"almacen:'" + almacen + "'," +
					"estatus:'" + estatus + "'," +
					"ambiente:'" + amb + "'" +
				"}";
			clsRequestHttp req = new clsRequestHttp();
			req.URI = "/Pedido/POST_LISTAPEDIDOS";
			req.JsonData = jsnfolio;
			string result = req.RequestJSON().Result;
			return result;
		}
	}
}
