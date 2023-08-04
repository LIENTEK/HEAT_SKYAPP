
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
	public class clsUriWs
	{
		public static string sample { get => ""; }

		#region ConfiguracionApp
		public static string Name { get => "Xcode DevTool"; }
		public static string Ambiente { get => "PRD"; }
		public static string StoreAppAndroid { get => "https://play.google.com/store/apps/details?id=com.companyname.promvtadiscalse"; }
		public static string StoreAppiOS { get => "https://apps.apple.com/mx/app/bancomer-m%C3%B3vil/id374824226"; }
		public static string Android { get => "Android"; }
		public static string iOS { get => "iOS"; }
		public static string Win { get => "Win"; }

		public static int LongUser = 50;

		public static int ShortUser = 4;

		public static int LongPassword = 50;

		public static int ShortPassword = 4;

		public static int ShortPhone = 10;

		public static int LongEdad = 90;

		public static int ShortEdad = 10;

		public static string HelperPassword = "*La contraseña debe ser de entre " + ShortPassword + " y " + LongPassword + " caracteres";

		#endregion

		#region RutasApi
		public static string ApiVersion { get => "Versiones/PostVersion"; }
		public static string ApiLogIn { get => "/Login"; }
		public static string ApiNuevoRegistro { get => "Usuarios/PostNuevo"; }
		public static string GetTokenPassword { get => "Usuarios/PostTokenPassword"; }
		public static string UpdatePassword { get => "Usuarios/PostUpdatePassword"; }

		public static string ApiListaEstablos { get => "/Establos_Ususarios"; }
		public static string ApiListaPropiedades { get => "/ParametrosClima"; }
		public static string ApiChart24 { get => "/GraficaClima24"; }
		public static string ApiReporte { get => "/ReporteMeteoeologico"; }
		#endregion

		#region LientekInfo
		public static string Company { get => "Lientek Solutios"; }
		public static string BodyContactoLientek { get => "Estimado " + ManagerLientek; }
		public static string CorreoLientek { get => "rubendiaznt@live.com.mx"; }
		public static string ManagerLientek { get => "ING.REY OMAR NAVARRETE MARTINEZ"; }
		public static string NumeroLientek { get => "871-480-2130"; }
		public static string FbLientek { get => "https://www.facebook.com/people/Lien-Tek/100076257905455/?locale=en_GB"; }
		public static string GoogleLientek { get => "https://www.google.com/search?q=lien+technologique&rlz=1C1CHBF_esMX1017MX1017&oq=lien+t&aqs=chrome.0.69i59l3j0i512l2j69i57j0i512l4.1162819j1j15&sourceid=chrome&ie=UTF-8"; }

		public static string Cliente { get => "http://digithpro.com/"; }

		public static string CatalogoLientek
		{
			get => "Empresa dedicada al desarrollo de soluciones tecnologicas." + Environment.NewLine + Environment.NewLine
			+ "- Escritorio" + Environment.NewLine + Environment.NewLine
			+ "- Web" + Environment.NewLine + Environment.NewLine
			+ "- Movil (iOS Android)" + Environment.NewLine + Environment.NewLine
			+ "- Interfaces entre sistemas" + Environment.NewLine + Environment.NewLine
			+ "- Rfid, Impresoras, Escaner, Etiquetadoras, Monitoreo" + Environment.NewLine + Environment.NewLine
			+ "- Servidores y Redes" + Environment.NewLine + Environment.NewLine
			+ "- Reportes y Analisis de datos" + Environment.NewLine; 
		}

		#endregion

		#region MensajeErrores
		public static string ErrorHttp { get => "ERROR: "; }
		public static string ErrorHttpLientek { get => "ERRORLIENTEK: "; }
		public static string ErrorRed { get => "Existen problemas para acceder a internet."; }
		#endregion
	}
}
