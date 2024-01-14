using System;
namespace ExtraVert
{
	public class Plant
	{
		public string Species { get; set; }
		public int LightNeeds { get; set; }
		public decimal AskingPrice { get; set; }
		public string City { get; set; }
		public int ZipCode { get; set; }
		public bool Sold { get; set; }
		public DateTime AvailableUntil { get; set; }
	}
}

