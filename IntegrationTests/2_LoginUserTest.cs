using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Response;
using AdvertApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Controllers;
using WebApplication1.Entities;
using Xunit;

namespace IntegrationTests
{

	// Make sure you already ran RegisterUserTest!
	public class LoginUserTest
	{


		[Fact]
		public void Test2()
		{
			//Arrange
			var advertDbContext = new AdvertismentDbContext();
			var passHandler = new PasswordHandler();
			var myConfiguration = new Dictionary<string, string>
			{
				{"JwtOptions:Issuer", "AdvertApi"},
				{"JwtOptions:Audience", "http://localhost:5000/"},
				{"JwtOptions:SecretKey", "ThisIsAhmadsSecretKey"}
			};

			var configuration = new ConfigurationBuilder()
					.AddInMemoryCollection(myConfiguration)
					.Build();

			var dbService = new AdvertDbService(advertDbContext, configuration, passHandler);
			var controller = new ClientsController(dbService);

			//Act

			var loginRequest = new LoginUserRequest
			{
				Login = "ahmad19047",
				Password = "Ahmad321"
			};
			var result = controller.Login(loginRequest);
			var okResult = result as OkObjectResult;

			//Test 2
			Assert.NotNull(okResult);
			Assert.IsType<AuthenticationSuccessResponse>(okResult.Value);
			Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
		}

	}
}
