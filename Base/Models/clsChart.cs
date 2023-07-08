using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
	public class clsChart {

		public PropiedadesMet LITH { get; }
		public PropiedadesMet LTEMPERATURA { get; }
		public PropiedadesMet LHUMEDAD { get; }
		public PropiedadesMet LUV { get; }

		public clsChart()
		{
			LITH = new PropiedadesMet(
				"ITH",
				new clsValores(0, 18.93),
				new clsValores(1, 20.93),
				new clsValores(2, 21.43),
				new clsValores(3, 20.58),
				new clsValores(4, 19.391),
				new clsValores(5, 18.624),
				new clsValores(6, 18.121),
				new clsValores(7, 17.428),
				new clsValores(8, 16.692),
				new clsValores(9, 16.155),
				new clsValores(10, 15.518),
				new clsValores(11, 14.964),
				new clsValores(12, 20.93),
				new clsValores(13, 20.93),
				new clsValores(14, 20.93),
				new clsValores(15, 20.93),
				new clsValores(16, 20.93),
				new clsValores(17, 20.93),
				new clsValores(18, 20.93),
				new clsValores(19, 20.93),
				new clsValores(20, 20.93),
				new clsValores(21, 20.93),
				new clsValores(22, 20.93),
				new clsValores(23, 20.93)
			);
			LTEMPERATURA = new PropiedadesMet(
				"Temperatura",
				new clsValores(0, 48.93),
				new clsValores(1, 40.93),
				new clsValores(2, 41.43),
				new clsValores(3, 40.58),
				new clsValores(4, 79.391),
				new clsValores(5, 78.624),
				new clsValores(6, 78.121),
				new clsValores(7, 77.428),
				new clsValores(8, 76.692),
				new clsValores(9, 66.155),
				new clsValores(10, 55.518),
				new clsValores(11, 54.964),
				new clsValores(12, 50.93),
				new clsValores(13, 20.93),
				new clsValores(14, 20.93),
				new clsValores(15, 20.93),
				new clsValores(16, 20.93),
				new clsValores(17, 10.93),
				new clsValores(18, 10.93),
				new clsValores(19, 10.93),
				new clsValores(20, 10.93),
				new clsValores(21, 10.93),
				new clsValores(22, 10.93),
				new clsValores(23, 10.93)
			);
			LHUMEDAD = new PropiedadesMet(
				"Humedad",
				new clsValores(0, 18.93),
				new clsValores(1, 10.93),
				new clsValores(2, 11.43),
				new clsValores(3, 10.58),
				new clsValores(4, 19.391),
				new clsValores(5, 18.624),
				new clsValores(6, 18.121),
				new clsValores(7, 17.428),
				new clsValores(8, 16.692),
				new clsValores(9, 16.155),
				new clsValores(10, 45.518),
				new clsValores(11, 44.964),
				new clsValores(12, 50.93),
				new clsValores(13, 50.93),
				new clsValores(14, 60.93),
				new clsValores(15, 60.93),
				new clsValores(16, 70.93),
				new clsValores(17, 70.93),
				new clsValores(18, 40.93),
				new clsValores(19, 40.93),
				new clsValores(20, 20.93),
				new clsValores(21, 20.93),
				new clsValores(22, 20.93),
				new clsValores(23, 20.93)
			);
			LUV = new PropiedadesMet(
				"UV",
				new clsValores(0, 8.93),
				new clsValores(1, 10.93),
				new clsValores(2, 11.43),
				new clsValores(3, 10.58),
				new clsValores(4, 49.391),
				new clsValores(5, 48.624),
				new clsValores(6, 98.121),
				new clsValores(7, 97.428),
				new clsValores(8, 96.692),
				new clsValores(9, 76.155),
				new clsValores(10, 75.518),
				new clsValores(11, 64.964),
				new clsValores(12, 60.93),
				new clsValores(13, 40.93),
				new clsValores(14, 40.93),
				new clsValores(15, 20.93),
				new clsValores(16, 20.93),
				new clsValores(17, 20.93),
				new clsValores(18, 10.93),
				new clsValores(19, 10.93),
				new clsValores(20, 10.93),
				new clsValores(21, 20.93),
				new clsValores(22, 20.93),
				new clsValores(23, 20.93)
			);

		}


		public class PropiedadesMet
		{
			public string Propiedad { get; set; }
			public IList<clsValores> Values { get; set; }

			public PropiedadesMet(string propiedad, params clsValores[] values)
			{
				this.Propiedad = propiedad;
				this.Values = new List<clsValores>(values);
			}
		}

		public class clsValores
			{
			public int Year { get; set; }
			public double Value { get; set; }

			public clsValores(int year, double value)
			{
				this.Year = year;
				this.Value = value;
			}
		}

	}

}