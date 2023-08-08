using DevExpress.Maui.DataForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
	public class clsEstablo
	{
        public int? ESTABLO_ID { get; set; }
        public int? CLIENTE_ID { get; set; }
        public object? CLIENTE { get; set; }
        public string? NOMBRE { get; set; }
        public double? LATITUD { get; set; }
        public double? LONGITUD { get; set; }
        public object? CONTACTO { get; set; }
        public object? EMAIL { get; set; }
        public object? TEL { get; set; }
        public bool? ACTIVO { get; set; }
        public DateTime FECHA_REG { get; set; }
        public object? USR_REG { get; set; }
        public DateTime FECHA_CAN { get; set; }
        public object? USR_CAN { get; set; }
        public int? MOTIVO_CAN_ID { get; set; }
        public object? MOTIVO_CAN { get; set; }

        
    }
}
