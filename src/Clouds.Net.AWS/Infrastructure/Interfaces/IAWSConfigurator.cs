namespace Clouds.Net.AWS.Infrastructure.Interfaces
{
    public interface IAWSConfigurator
    {
        IAWSConfigurator SetDefaultRegion(string region);
        IAWSConfigurator SetDefaultCredentials(string accessKey, string secretKey);
        IAWSConfigurator UseLocalStack(string? url = null);

        #region S3
        IAWSConfigurator AddS3(string bucketName, string region);
        IAWSConfigurator AddS3(string bucketName, string region, string accessKey, string secretKey);
        IAWSConfigurator AddS3(string bucketName);
        IAWSConfigurator AddS3(string bucketName, string accessKey, string secretKey);
        #endregion

        #region Congnito
        IAWSConfigurator AddCognito(string clientId, string clientSecret, string userPoolId);
        IAWSConfigurator AddCognito(
            string clientId,
            string clientSecret,
            string userPoolId,
            string accessKey,
            string secretKey);
        IAWSConfigurator AddCognito(
            string clientId,
            string clientSecret,
            string userPoolId,
            string region);
        IAWSConfigurator AddCognito(
            string clientId,
            string clientSecret,
            string userPoolId,
            string accessKey,
            string secretKey,
            string region);
        #endregion

        #region SES
        IAWSConfigurator AddSES(string sourceMail);
        IAWSConfigurator AddSES(string sourceMail, string region);
        IAWSConfigurator AddSES(string accessKey, string secretKey, string sourceMail);
        IAWSConfigurator AddSES(string accessKey, string secretKey, string sourceMail, string region);
        #endregion

        #region SQS
        IAWSConfigurator AddSQS(string queueUrl);
        IAWSConfigurator AddSQS(string queueUrl, string region);
        IAWSConfigurator AddSQS(string accessKey, string secretKey, string queueUrl);
        IAWSConfigurator AddSQS(string accessKey, string secretKey, string queueUrl, string region);
        #endregion
    }
}
