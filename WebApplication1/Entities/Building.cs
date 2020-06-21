using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Entities
{
    public class Building
    {
        public int IdBuilding{ get; set; }
        public string Street { get; set; }
        public int StreetNumber{ get; set; }
        public string City { get; set; }

        // Precision(6,2)
        public decimal Height { get; set; }

        //Defines collection of campaigns where the building was a 'from building'
        public ICollection<Campaign> StartCampaigns { get; set; }
        //Defines collection of campaigns where the building was a 'to building'
        public ICollection<Campaign> EndCampaigns { get; set; }
    }
}
