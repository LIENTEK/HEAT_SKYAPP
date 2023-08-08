using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
	public class clsChart {
		public DateTime hora { get; set; }
		public double? temp { get; set; }
		public int? humedad { get; set; }
		public int? ITH { get; set; }
	}

}