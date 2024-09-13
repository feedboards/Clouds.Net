using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
using Clouds.Net.AWS;
using Clouds.Net.AWS.Interfaces;
using Clouds.Net.AWS.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace Clouds.Net.AWS.Helpers
{
    public class CognitoHelper : ICognitoHelper
    {
        private readonly AmazonCognitoIdentityProviderClient _providerClient;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _userPoolId;

        public CognitoHelper(
            string clientId,
            string clientSecret,
            string userPoolId,
            string DECognitoUserAccessKey,
            string DECognitoUserSecretAccessKey)
            : this(
                  clientId,
                  clientSecret,
                  userPoolId,
                  DECognitoUserAccessKey,
                  DECognitoUserSecretAccessKey,
                  SD.DefaultRegion)
        {
        }

        public CognitoHelper(
            string clientId,
            string clientSecret,
            string userPoolId,
            string DECognitoUserAccessKey,
            string DECognitoUserSecretAccessKey,
            string region)
        {
            _clientId = clientId ?? throw new InvalidOperationException("Client ID must be provided.");

            _clientSecret = clientSecret ?? throw new InvalidOperationException("Client Secret must be provided.");

            _userPoolId = userPoolId ?? throw new InvalidOperationException("User Pool ID must be provided.");

            _providerClient =
                new AmazonCognitoIdentityProviderClient(
                    new BasicAWSCredentials(DECognitoUserAccessKey, DECognitoUserSecretAccessKey),
                    AWSUtils.GetRegionFromString(region));
        }

        public async Task<AdminDeleteUserResponse> DeleteUserAsync(string email)
        {
            return await _providerClient.AdminDeleteUserAsync(new AdminDeleteUserRequest
            {
                Username = email,
                UserPoolId = _userPoolId
            });
        }

        public async Task<GetUserResponse> GetUserAsync(string accessToken)
        {
            return await _providerClient.GetUserAsync(new GetUserRequest
            {
                AccessToken = accessToken,
            });
        }

        public async Task<InitiateAuthResponse> InitiateAuthAsync(string email, string password)
        {
            return await _providerClient.InitiateAuthAsync(new InitiateAuthRequest
            {
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                ClientId = _clientId,
                AuthParameters = new Dictionary<string, string>
                {
                    { "USERNAME", email },
                    { "PASSWORD", password },
                    { "SECRET_HASH", GenerateSecretHash(email) }
                }
            });
        }

        public async Task<SignUpResponse> SignUpAsync(string email, string password, string role)
        {
            return await _providerClient.SignUpAsync(new SignUpRequest
            {
                ClientId = _clientId,
                SecretHash = GenerateSecretHash(email),
                Username = email,
                Password = password,
                UserAttributes = new List<AttributeType>
                {
                    new AttributeType { Name = "email", Value = email },
                    new AttributeType { Name = "custom:Role", Value = role },
                }
            });
        }

        public string GetSubIdByAccessToken(string accessToken)
        {
            var jsonToken = new JwtSecurityTokenHandler()
                .ReadToken(accessToken) as JwtSecurityToken;
            return jsonToken?.Claims
                .FirstOrDefault(claim => claim.Type == "sub")?.Value ?? string.Empty;
        }

        public async Task<string> GetUserRoleAsync(string accessToken)
        {
            try
            {
                var responseString = string.Empty;

                foreach (var attribute in
                    (await GetUserAsync(accessToken)).UserAttributes)
                {
                    _ = attribute.Name == "custom:Role" ?
                        responseString = attribute.Value : string.Empty;
                }

                return responseString;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public void Dispose()
        {
            _providerClient.Dispose();
            GC.SuppressFinalize(this);
        }

        private string GenerateSecretHash(string username)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_clientSecret));
            return Convert.ToBase64String(
                hmac.ComputeHash(
                    Encoding.UTF8.GetBytes(username + _clientId)));
        }
    }
}
