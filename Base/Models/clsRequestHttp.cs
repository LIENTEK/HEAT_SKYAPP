using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
	public class clsRequestHttp
	{

		public string URI { get; set; }
		public string JsonData { get; set; }

		string MainURI = "http://www.modernagroup.com.mx:8099/ApiAppVentasDiscalse/api/";  // INTERNET ISS


		public clsRequestHttp()
		{

		}

		async public Task<string> RequestJSON()
		{
			HttpClient client = new HttpClient();
			string result;
			MainURI = MainURI + URI;
			try
			{
				var content = new StringContent(JsonData, Encoding.UTF8, "application/json");
				HttpResponseMessage response = client.PostAsync(MainURI, content).Result;
				result = await response.Content.ReadAsStringAsync();

				return result;
			}
			catch (Exception ex)
			{
				return "ERROR: " + ex.Message;
			}
		}

		async public Task<string> RequestJSONtoken()
		{
			HttpClient client = new HttpClient();
			string result;
			MainURI = MainURI + URI;
			try
			{
				var content = new StringContent(JsonData, Encoding.UTF8, "application/json");
				HttpResponseMessage response = client.PostAsync(MainURI + URI, content).Result;
				result = await response.Content.ReadAsStringAsync();

				return result;
			}
			catch (Exception ex)
			{
				return "ERROR: " + ex.Message;
			}
		}

	}
}

