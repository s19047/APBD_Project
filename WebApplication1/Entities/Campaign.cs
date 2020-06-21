using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Entities
{
	public class Campaign
	{
		// PK
		public int IdCampaign { get; set; }
		public int IdUser { get; set; }
		public DateTime StartDate { get; set; }
		public  DateTime EndDate { get; set; }
		// Precision(6,2)
		public decimal PricePerSquareMeter { get; set; }
		//FK
		public int FromIdBuilding { get; set; }
		//FK
		public int ToIdBuilding { get; set; }
		public Building FromBuilding { get; set; }
		public Building ToBuilding { get; set; }
		public User User{ get; set; }

		public ICollection<Banner> Banners { get; set; }
	}
}
