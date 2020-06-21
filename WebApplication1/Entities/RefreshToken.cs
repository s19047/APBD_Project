using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace AdvertApi.Models
{

    public class RefreshToken 
    {
        [Key]
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public int UserId { get; set; }

        //Incase we want to invalidate the refresh Token ( ex: user changes password or thinks someone else has access to his/her account)
        public bool Invalidated { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

    }
}
