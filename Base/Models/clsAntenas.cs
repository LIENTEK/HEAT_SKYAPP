using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
    public class clsAntenas
    {
		public int? GATEWAY_ID { get; set; }
		public string? MAC { get; set; }
		public int? ESTABLO_ID { get; set; }
		public double? LATITUD { get; set; }
		public double? LONGITUD { get; set; }
		public string? NOMBRE { get; set; }
		public string? UBICACION { get; set; }
		public DateTime FECHA_ULT_LECTURA { get; set; }
		public string? ULTIMO_ESTATUS { get; set; }
		public double? HORAS_OFFLINE { get; set; }
		public Color? AlertaEstatus { get; set; }
		public Color? AlertaOffline { get; set; }
	}
}
