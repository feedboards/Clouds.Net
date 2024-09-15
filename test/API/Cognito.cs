using Amazon.CognitoIdentityProvider.Model;
using Clouds.Net.AWS.Interfaces;

namespace API
{
    public class Cognito
    {
        public readonly ICognitoHelper _cognitoHelper;

        public Cognito(ICognitoHelper cognitoHelper)
        {
            _cognitoHelper = cognitoHelper;
        }

        public async Task Run()
        {
            await _cognitoHelper.GetUserAsync("user-access-token");

            await _cognitoHelper.DeleteUserAsync("user-email");

            await _cognitoHelper.InitiateAuthAsync("user-email", "user-password");

            var attributes = new List<AttributeType>(); // list of your attributes

            await _cognitoHelper.SignUpAsync("user-username", "user-password", attributes);

            await _cognitoHelper.GetUserRoleAsync("user-access-token");

            _cognitoHelper.GetSubIdByAccessToken("user-access-key");
        }
    }
}
