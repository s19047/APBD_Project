using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Entities
{
    public class Banner
    {
        // PK
        public int IdAdvertisment { get; set; }

        //DataBase schema Says int type , but I think it should be nvarChar
        public string Name { get; set; }
        //Precision (6,2)
        public decimal Price { get; set; }
        //Fk
        public int IdCampaign { get; set; }
        //Precision (6,2)
        public decimal Area { get; set; }

        public Campaign Campaign { get; set; }
       
    }
}
