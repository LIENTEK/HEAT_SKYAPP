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

		//string MainURI = "http://www.modernagroup.com.mx:8099/ApiAppVentasDiscalse/api/";  // INTERNET ISS

		string MainURIHeat = "http://50.21.190.132/HEATSKY/wsHeatsky.asmx";

        string MainURI = "http://50.21.190.132/DIGISKY/wsDigisky.asmx"; // INTERNET ISS

		string MainURICOW = "http://50.21.190.132/COWLOOK/COWLOOK.ASMX";

		public clsRequestHttp()
		{

		}

		async public Task<string> Requestform()
		{
			HttpClient client = new HttpClient();
			string result;
			MainURI = MainURI + URI;
			//client.Timeout = System.TimeSpan.FromMilliseconds(1000);
			try
			{
				var content = new StringContent(JsonData, Encoding.UTF8, "application/x-www-form-urlencoded");
				HttpResponseMessage response = client.PostAsync(MainURI, content).Result;
				result = await response.Content.ReadAsStringAsync();				

				return result;
			}
			catch (Exception ex)
			{
				return "ERROR: " + ex.Message;
			}
		}

        async public Task<string> RequestformHeatsky()
        {
            HttpClient client = new HttpClient();
            string result;
            MainURIHeat = MainURIHeat + URI;
			//client.Timeout = System.TimeSpan.FromMilliseconds(1000);
			try
            {
                var content = new StringContent(JsonData, Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = client.PostAsync(MainURIHeat, content).Result;
                result = await response.Content.ReadAsStringAsync();

                return result;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }
        }

		async public Task<string> RequestformCOW()
		{
			HttpClient client = new HttpClient();
			string result;
			MainURICOW = MainURICOW + URI;
			//client.Timeout = System.TimeSpan.FromMilliseconds(1000);
			try
			{
				var content = new StringContent(JsonData, Encoding.UTF8, "application/x-www-form-urlencoded");
				HttpResponseMessage response = client.PostAsync(MainURICOW, content).Result;
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

