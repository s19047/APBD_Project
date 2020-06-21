using AdvertApi.DTOs.Requests;
using AdvertApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    //All exceptions are handled by exception Middleware

    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IDbService _service;
        
        public ClientsController(IDbService service)
        {
            _service = service;
        }

       
        [HttpPost]
        public IActionResult RegisterClient(RegisterUserRequest request)
        {
            var res = _service.RegisterClient(request);
            return Ok(res);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUserRequest request)
        {
            return Ok(_service.LoginUser(request));
        }

        [Authorize(Roles = "admin")]
        [HttpPost("admin")]
        public IActionResult RegisterEmployee(RegisterUserRequest request)
        {
            //TODO - Create Employee Model + Table + RegisterEmplyee Method 
            throw new NotImplementedException();
        }

        [HttpPost("refreshToken")]
        public IActionResult RefreshTokens(RefreshTokenRequest request)
        {
            return Ok(_service.RefreshTokens(request));
        }

        //List campaigns for the logged-in user
        [Authorize]
        [HttpGet("campaigns")]
        public IActionResult GetCampaigns()
        {
            var userId = int.Parse(this.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(_service.GetCampaigns(userId));
        }

        [Authorize]
        [HttpPost("campaigns")]
        public IActionResult CreateCampaign(CreateCampaignRequest request)
        {
            return Ok(_service.CreateCampaign(request));
        }
    }
}