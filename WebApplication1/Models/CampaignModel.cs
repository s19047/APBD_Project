using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Models
{
	public class CampaignModel
	{
			
			public int IdCampaign { get; set; }
			public DateTime StartDate { get; set; }
			public DateTime EndDate { get; set; }
			public decimal PricePerSquareMeter { get; set; }
			public int FromBuildingStreetNumber { get; set; }
			public int ToBuildingStreetNumber { get; set; }

			public ICollection<BannerModel> Ads { get; set; }

	}
}
