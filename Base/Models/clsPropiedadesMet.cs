using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
	public class clsPropiedadesMet
	{
        public int time_epoch { get; set; }
        public DateTime time { get; set; }
        public double temp_c { get; set; }
        public double temp_f { get; set; }
        public double condition_text { get; set; }
        public object condition_icon { get; set; }
        public int condition_code { get; set; }
        public double wind_mph { get; set; }
        public double wind_kph { get; set; }
        public int wind_degree { get; set; }
        public string wind_dir { get; set; }
        public double pressure_mb { get; set; }
        public double pressure_in { get; set; }
        public double precip_mm { get; set; }
        public double precip_in { get; set; }
        public int humidity { get; set; }
        public int cloud { get; set; }
        public double feelslike_c { get; set; }
        public double feelslike_f { get; set; }
        public double windchill_c { get; set; }
        public double windchill_f { get; set; }
        public double heatindex_c { get; set; }
        public double heatindex_f { get; set; }
        public double dewpoint_c { get; set; }
        public double dewpoint_f { get; set; }
        public int will_it_rain { get; set; }
        public int will_it_snow { get; set; }
        public int is_day { get; set; }
        public double vis_km { get; set; }
        public double vis_miles { get; set; }
        public int chance_of_rain { get; set; }
        public int chance_of_snow { get; set; }
        public double gust_mph { get; set; }
        public double gust_kph { get; set; }
        public double uv { get; set; }
        public double ITH { get; set; }
        public string Hora { get; set; }
        public int HoraInt { get; set; }
        public DateTime Fecha { get; set; }
        public string Dia { get; set; }
        public object Shifthora { get; set; }
        public string FechaHora { get; set; }

    }
}
