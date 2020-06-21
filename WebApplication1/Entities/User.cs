using AdvertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Entities
{
	/*Please note that I have decided to change table to client to user , so that I can also 
		store Information about Employees (in this case Admin only) in the same table.
	    Since admins will also be able to create campaigns , this approach should work  */
	public class User
	{
		//PK
		public int IdUser{ get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }

		// is user a client or admin
		public string Role { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public string PasswordSalt { get; set; }
		public RefreshToken RefreshToken { get; set; }
		public ICollection<Campaign> Campaigns { get; set; }
	}
}
