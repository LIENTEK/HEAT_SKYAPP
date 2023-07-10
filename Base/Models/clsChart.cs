using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
	public class clsChart {

        public int time_epoch { get; set; }
        public DateTime time { get; set; }
        public decimal temp_c { get; set; }
        public decimal temp_f { get; set; }
        public decimal condition_text { get; set; }
        public string condition_icon { get; set; }
        public int condition_code { get; set; }
        public decimal wind_mph { get; set; }
        public decimal wind_kph { get; set; }
        public int wind_degree { get; set; }
        public string wind_dir { get; set; }
        public decimal pressure_mb { get; set; }
        public decimal pressure_in { get; set; }
        public decimal precip_mm { get; set; }
        public decimal precip_in { get; set; }
        public int humidity { get; set; }
        public int cloud { get; set; }
        public decimal feelslike_c { get; set; }
        public decimal feelslike_f { get; set; }
        public decimal windchill_c { get; set; }
        public decimal windchill_f { get; set; }
        public decimal heatindex_c { get; set; }
        public decimal heatindex_f { get; set; }
        public decimal dewpoint_c { get; set; }
        public decimal dewpoint_f { get; set; }
        public int will_it_rain { get; set; }
        public int will_it_snow { get; set; }
        public int is_day { get; set; }
        public decimal vis_km { get; set; }
        public decimal vis_miles { get; set; }
        public int chance_of_rain { get; set; }
        public int chance_of_snow { get; set; }
        public decimal gust_mph { get; set; }
        public decimal gust_kph { get; set; }
        public decimal uv { get; set; }

        public decimal ITH
        {
            get;
            //{
                //return Math.Truncate((1.8 * temp_c + 32) - (0.55 - 0.55 * humidity / 100) * ((1.8) * temp_c - 26));
            //}
        }
        public string Hora
        {
            get
            {
                return time.ToString("HH:00"); // (CDate("1.1.1970 00:00:00").AddSeconds(time_epoch)).ToLocalTime().ToString("HH:00")
            }
        }
        public int HoraInt
        {
            get
            {
                return System.Convert.ToInt32(time.ToString("HH")); // CInt((CDate("1.1.1970 00:00:00").AddSeconds(time_epoch)).ToLocalTime().ToString("HH"))
            }
        }
        public DateTime Fecha
        {
            get
            {
                return (DateTime) time; // CDate((CDate("1.1.1970 00:00:00").AddSeconds(time_epoch)).ToLocalTime().ToString("dd/MM/yyyy"))
            }
        }
        public string Dia
        {
            get
            {
                return time.ToString("MM-dd"); // (CDate("1.1.1970 00:00:00").AddSeconds(time_epoch)).ToLocalTime().ToString("MM-dd")
            }
        }
        public string Shifthora { get; set; }
        public string FechaHora
        {
            get
            {
                return Dia + " " + Shifthora;
            }
        }
    }

}