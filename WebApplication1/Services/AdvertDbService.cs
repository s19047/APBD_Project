using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Response;
using AdvertApi.ExceptionHandling;
using AdvertApi.Exceptions;
using AdvertApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebApplication1.Entities;

namespace AdvertApi.Services
{

    public class AdvertDbService : IDbService
	{
		private readonly AdvertismentDbContext _context;
        private IConfiguration _configuration { get; set; }
        private IPasswordHandler _passwordHandler { get; set; }
        
        public AdvertDbService(AdvertismentDbContext context, IConfiguration configuration, IPasswordHandler passwordHandler)
        {
			_context = context;
            _configuration = configuration;
            _passwordHandler = passwordHandler;
		}


		public string RegisterClient(RegisterUserRequest request)
		{
            //Check if User Already Exists
            if (_context.Users.Where(u => u.Login.Equals(request.Login)).Any())
                throw new UserAlreadyExistsException("UserName Already exists");

            //generate salt and encrypt pass
            var salt = _passwordHandler.GenerateRandomSalt();
            var encryptedPass = _passwordHandler.EncryptPassword(request.Password, salt);


            var client = new User
            {
               FirstName = request.FirstName,
               LastName = request.LastName,
               Email = request.Email,
               Phone = request.Phone,
               Login = request.Login,
               Role = "user",
               Password = encryptedPass,
               PasswordSalt = salt,

            };

            _context.Users.Add(client);
            _context.SaveChanges();

            return "Client was added Successfully :)";
        
         }


        public AuthenticationSuccessResponse LoginUser(LoginUserRequest request)
        {
            //Check if username exists
            var user = _context.Users.Where(u => u.Login.Equals(request.Login)).FirstOrDefault();

            if (user is null)
                throw new UserDoesNotExistException("The following username does not exist");

            //Verify Password
            var encryptedPass = user.Password;
            var unEncryptedPass = request.Password;
            var salt = user.PasswordSalt;

            if (!_passwordHandler.ConfirmPassword(encryptedPass,unEncryptedPass,salt))
                throw new IncorrectPasswordException("This Password is Incorrect");

            //Generate New Access and RefreshToken
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName+user.LastName ),
                new Claim(ClaimTypes.Role, "user")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtOptions:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken
            (
                issuer: _configuration["JwtOptions:Issuer"],
                audience: _configuration["JwtOptions:Audience"],
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );



            //if user never had a refresh token , then generate one , else replace old with new
            var oldRefreshToken = _context.RefreshTokens.Where(r => r.UserId.Equals(user.IdUser)).FirstOrDefault();
            var newRefreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expires = DateTime.Now.AddDays(7),
                UserId = user.IdUser,
            };

            if(oldRefreshToken is null)
            {
                _context.RefreshTokens.Add(newRefreshToken);
            }
            else
            {

                _context.RefreshTokens.Remove(oldRefreshToken);
                _context.SaveChanges();
                _context.RefreshTokens.Add(newRefreshToken);
            }

            _context.SaveChanges();
        
            return new AuthenticationSuccessResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                RefreshToken = newRefreshToken.Token
            };

        }

        public AuthenticationSuccessResponse RefreshTokens(RefreshTokenRequest request)
        {

            var validatedToken = GetPrincipalFromToken(request.AccessToken);

            var storedRefreshToken = _context.RefreshTokens.SingleOrDefault(r => r.Token == request.RefreshToken);

            if ((validatedToken is null) || (storedRefreshToken is null) || ((storedRefreshToken.Invalidated)))
                throw new InvalidTokenException("This token is invalid");

            //Update Refresh 
            var user = _context.Users.Where(u => u.IdUser.Equals(storedRefreshToken.UserId)).FirstOrDefault();

            _context.RefreshTokens.Remove(storedRefreshToken);
            _context.SaveChanges();
            var newRefreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expires = DateTime.Now.AddDays(7),
                UserId = user.IdUser
            };
            _context.RefreshTokens.Add(newRefreshToken);

            _context.SaveChanges();

            //generate new access token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName+user.LastName ),
                new Claim(ClaimTypes.Role, "user")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtOptions:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken
            (
                issuer: _configuration["JwtOptions:Issuer"],
                audience: _configuration["JwtOptions:Audience"],
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );


            
            return new AuthenticationSuccessResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                RefreshToken = newRefreshToken.Token
            };
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JwtOptions:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = _configuration["JwtOptions:Audience"],

                    ValidateLifetime = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtOptions:SecretKey"]))
                };

                tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }

        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }


        public GetCampaignsResponse GetCampaigns(int userId)
        {
            var user = _context.Users.Where(u => u.IdUser.Equals(userId)).FirstOrDefault();
            var campaigns = _context.Campaigns.Where(c => c.IdUser.Equals(userId)).ToList();

            List<CampaignModel> campaignModels = new List<CampaignModel>();
            foreach(var campaign in campaigns)
            {
                var FromStreetNumber = _context.Buildings.Where(b => b.IdBuilding.Equals(campaign.FromIdBuilding))
                                                                    .Select(b => b.StreetNumber).FirstOrDefault();

                var ToStreetNumber = _context.Buildings.Where(b => b.IdBuilding.Equals(campaign.ToIdBuilding))
                                                                    .Select(b => b.StreetNumber).FirstOrDefault();

                var ads = _context.Banners.Where(b => b.IdCampaign.Equals(campaign.IdCampaign)).ToList();
                List<BannerModel> bannerModels = new List<BannerModel>();
                foreach(var ad in ads)
                {
                    var bannerModel = new BannerModel
                    {
                        IdAdvertisment = ad.IdAdvertisment,
                        Name = ad.Name,
                        Price = ad.Price
                    };

                    bannerModels.Add(bannerModel);

                }
                var campaignModel = new CampaignModel
                {
                    IdCampaign = campaign.IdCampaign,
                    StartDate = campaign.StartDate,
                    EndDate = campaign.EndDate,
                    FromBuildingStreetNumber = FromStreetNumber,
                    ToBuildingStreetNumber = ToStreetNumber,
                    Ads = bannerModels
                };

                campaignModels.Add(campaignModel);
            }

            return new GetCampaignsResponse
            {
                CustomerName = user.FirstName+"_"+user.LastName,
                Campaigns = campaignModels
            };
        }

        public string CreateCampaign(CreateCampaignRequest request)
        {
            //Check if buildings are on the same street
            var firstBuilding = _context.Buildings.Where(b => b.IdBuilding.Equals(request.FromIdBuilding)).FirstOrDefault();
            var secondBuilding = _context.Buildings.Where(b => b.IdBuilding.Equals(request.ToIdBuilding)).FirstOrDefault();

            if (!firstBuilding.Street.Equals(secondBuilding.Street))
                throw new BuildingsOnDifferentStreetException("The buildings provided are on two different streets!");

            //Create Campaign , then banners
            var campaign = new Campaign
            {
                IdUser = request.IdClient,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                PricePerSquareMeter = request.PricePerSquareMeter,
                FromIdBuilding = request.FromIdBuilding,
                ToIdBuilding = request.ToIdBuilding
            };
            _context.Campaigns.Add(campaign);
            _context.SaveChanges();

            
            //we assume width of all buildings is 1, the larger building will have 1 banner covering it only
            //, and another banner will extend from the smaller building to the larger building 

            decimal banner1Area, banner2Area;
            if(firstBuilding.Height > secondBuilding.Height)
            {
                banner1Area = firstBuilding.Height;
                var buildingDifference = Math.Abs(firstBuilding.StreetNumber - secondBuilding.StreetNumber);
                banner2Area = secondBuilding.Height * buildingDifference;
            }
            else
            {
                banner1Area = secondBuilding.Height;
                var buildingDifference = Math.Abs(firstBuilding.StreetNumber - secondBuilding.StreetNumber);
                banner2Area = firstBuilding.Height * buildingDifference;
            }

            //Create banners and add them to DB
            var banner1 = new Banner
            {
                // I would prefer if user inputted the name with the request , but that wasn't in the requirement
                Name = "Banner1",
                Area = banner1Area,
                Price = banner1Area*request.PricePerSquareMeter,
                IdCampaign = campaign.IdCampaign
            };
            _context.Banners.Add(banner1);
            _context.SaveChanges();
            var banner2 = new Banner
            {
                // I would prefer if user inputted the name with the request , but that wasn't in the requirement
                Name = "Banner2",
                Area = banner2Area,
                Price = banner2Area * request.PricePerSquareMeter,
                IdCampaign = campaign.IdCampaign
            };
            _context.Banners.Add(banner2);
            _context.SaveChanges();

            return "Campaigns and Banners Were Created Successfully";
        }
    }
}
