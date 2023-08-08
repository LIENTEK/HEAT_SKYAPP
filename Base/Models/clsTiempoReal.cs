using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
	public class clsTiempoReal
	{
		public string? NAVE { get; set; }
		public string? NOMBRE { get; set; }
		public double? TEMPERATURA { get; set; }
		public int? HUMEDAD { get; set; }
		public string? ESTATUS { get; set; }
		public double? DURACION { get; set; }
		public int? ITH { get; set; }

		public Color? AlertaTemp { get; set; }
		public Color? AlertaHumedad { get; set; }
		public Color? AlertaITH { get; set; }
	}
}
