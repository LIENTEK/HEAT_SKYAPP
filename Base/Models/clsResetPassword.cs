using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
    public class clsResetPassword
    {
		public string Amb { get; set; }
		public string Id { get; set; }
		public string User { get; set; }
        public string Codigo { get; set; }
		public string Password { get; set; }
		public string Cpassword { get; set; }

		clsResetPassword() { 
        
        }

		public string GetToken()
		{
			clsRequestHttp req = new clsRequestHttp();
			req.URI = clsUriWs.GetTokenPassword;
			req.JsonData = Newtonsoft.Json.JsonConvert.SerializeObject(this);
			string result = req.Requestform().Result;
			return result;
		}

		public string UpdatePassword()
		{
			clsRequestHttp req = new clsRequestHttp();
			req.URI = clsUriWs.UpdatePassword;
			req.JsonData = Newtonsoft.Json.JsonConvert.SerializeObject(this);
			string result = req.Requestform().Result;
			return result;
		}
	}
}
