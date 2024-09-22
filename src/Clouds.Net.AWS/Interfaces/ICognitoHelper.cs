using Amazon.CognitoIdentityProvider.Model;

namespace Clouds.Net.AWS.Interfaces
{
    public interface ICognitoHelper : IDisposable
    {
        Task<GetUserResponse> GetUserAsync(string accessToken);
        Task<AdminDeleteUserResponse> DeleteUserAsync(string email);
        Task<InitiateAuthResponse> InitiateAuthAsync(string email, string password);
        Task<SignUpResponse> SignUpAsync(string username, string password, List<AttributeType> attributes);
        Task<string> GetUserRoleAsync(string accessToken);
        string GetSubIdByAccessToken(string accessToken);
    }
}
