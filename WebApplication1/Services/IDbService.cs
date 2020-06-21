using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Response;

namespace AdvertApi.Services
{
	public interface IDbService
	{
		public string RegisterClient(RegisterUserRequest request);
		public AuthenticationSuccessResponse LoginUser(LoginUserRequest request);
		public AuthenticationSuccessResponse RefreshTokens(RefreshTokenRequest request);
		public GetCampaignsResponse GetCampaigns(int userId);
		public string CreateCampaign(CreateCampaignRequest request);
	}
}
