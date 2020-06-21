using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace HospitalDB.Entities
{
	public static class ModelBuilderExtensions
	{
		public static void Seed(this ModelBuilder modelBuilder)
        {
            //Clients can be registered throught the RegisterClient Endpoint
            //Campaigns and banners can be created through the CreateCampaign EndPoint

            var mockBuilding = new List<Building>();
            mockBuilding.Add(new Building { IdBuilding = 1, Street = "Marii grzegowskiej", StreetNumber = 1, City = "Warsaw", Height = 10 });
            mockBuilding.Add(new Building { IdBuilding = 2, Street = "Marii grzegowskiej", StreetNumber = 2, City = "Warsaw", Height = 3 });
            mockBuilding.Add(new Building { IdBuilding = 3, Street = "Marii grzegowskiej", StreetNumber = 3, City = "Warsaw", Height = 19 });
            mockBuilding.Add(new Building { IdBuilding = 4, Street = "Mokotow", StreetNumber = 4, City = "Warsaw", Height = 9 });
            modelBuilder.Entity<Building>().HasData(mockBuilding);
        }
	}
}
