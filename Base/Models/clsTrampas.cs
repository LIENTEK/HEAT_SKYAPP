﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
    public class clsTrampas
    {
		public string? CORRAL { get; set; }
		public string? TRAMPA { get; set; }
		public string? ESTATUS { get; set; }
		public string? DURACION { get; set; }

		public Color? AlertaEstatus { get; set; }

		public Color? AlertaHeader { get; set; }
		public Color? AlertaOffline { get; set; }
	}
}
