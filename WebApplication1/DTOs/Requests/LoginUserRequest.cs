using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.DTOs.Requests
{
	public class LoginUserRequest
	{
		public string Login { get; set; }
		public string Password { get; set; }
	}
}
