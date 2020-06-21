using AdvertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.DTOs.Response
{
	public class GetCampaignsResponse
	{
		public string CustomerName { get; set; }
		public ICollection<CampaignModel> Campaigns { get; set;}
	}
}
