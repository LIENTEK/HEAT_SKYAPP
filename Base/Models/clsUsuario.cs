using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
	public class clsUsuario
	{
    
            public int USUARIO_ID { get; set; }
            public int CLIENTE_ID { get; set; }
            public object CLIENTE { get; set; }
            public string NOMBRE { get; set; }
            public object USUARIO { get; set; }
            public object PWD { get; set; }
            public object ACTIVO { get; set; }
            public DateTime FECHA_REG { get; set; }
            public object USR_REG { get; set; }
            public DateTime FECHA_MOD { get; set; }
            public object USR_MOD { get; set; }
            public object MOTIVO_MOD { get; set; }
            public object ESTABLOS { get; set; }
        


        public clsUsuario() { 
		
		}

		public string LogInWsMe()
		{
			clsRequestHttp req = new clsRequestHttp();
			req.URI = clsUriWs.ApiLogIn;
			req.JsonData = Newtonsoft.Json.JsonConvert.SerializeObject(this);
			string result = req.Requestform().Result;
			return result;
		}

		public string LogInWs()
		{

			string jsnversion = "usr=" + USUARIO + "&pwd=" + PWD;
			clsRequestHttp req = new clsRequestHttp();
			req.URI = clsUriWs.ApiLogIn;
			req.JsonData = jsnversion;
			string result = req.Requestform().Result;
			return result;
		}

	}
}
