using System;

namespace Base.Models
{
	public class Item
	{
		public string Id { get; set; }
		public string Text { get; set; }
		public string Description { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public double Value { get; set; }
		public double Value2 { get; set; }
		public int Hora { get; set; }
	}
}