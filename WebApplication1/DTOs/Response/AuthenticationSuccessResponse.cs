
using System;

namespace AdvertApi.DTOs.Response
{
	public class AuthenticationSuccessResponse
	{ 
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
	}
}
