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
	
	public class RegisterUserTest
	{
		//Note : make sure you run tests inorder because later tests might depend on earlier ones
		// Also note that running a test twice might result in an error , for instance if you register the same user twice

		[Fact]
		//register user test
		public void Test1()
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

			var registerClientRequest = new RegisterUserRequest
			{
				FirstName = "John",
				LastName = "Kowalski",
				Email = "kowalski@wp.pl",
				Phone = "454-232-222",
				Login = "ahmadAlaziz123",
				Password = "Ahmad321"
			};
			var result = controller.RegisterClient(registerClientRequest);
			var okResult = result as OkObjectResult;

			//Test 
			Assert.NotNull(okResult);
			Assert.IsType<string>(okResult.Value);
			Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

		}

	
	
	}
}
