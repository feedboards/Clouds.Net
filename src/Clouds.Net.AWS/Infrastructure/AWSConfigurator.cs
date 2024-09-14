using Clouds.Net.AWS.Helpers;
using Clouds.Net.AWS.Infrastructure.Interfaces;
using Clouds.Net.AWS.Infrastructure.Options;
using Clouds.Net.AWS.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Clouds.Net.AWS.Infrastructure
{
    public class AWSConfigurator : IAWSConfigurator
    {
        private readonly IServiceCollection _services;
        private readonly AWSOptions _awsOptions;
        private readonly LocalStackOptions _localStackOptions;

        internal IServiceCollection Services
        {
            get { return _services; }
        }

        public AWSConfigurator(IServiceCollection services)
        {
            _services = services;
            _awsOptions = new AWSOptions();
            _localStackOptions = new LocalStackOptions();

            _localStackOptions.UseLocalStack = false;
        }

        // TODO add LocalStack support

        #region Cognito
        public IAWSConfigurator AddCognito(string clientId, string clientSecret, string userPoolId)
        {
            ValidateAWSCredentials();
            ValidateAWSDefaultRegion();

            return AddCognito(
                clientId,
                clientSecret,
                userPoolId,
                _awsOptions.AccessKey,
                _awsOptions.SecretKey,
                SD.DefaultRegion);
        }

        public IAWSConfigurator AddCognito(string clientId, string clientSecret, string userPoolId, string accessKey, string secretKey)
        {
            ValidateAWSDefaultRegion();

            return AddCognito(
                clientId,
                clientSecret,
                userPoolId,
                accessKey,
                secretKey,
                SD.DefaultRegion);
        }

        public IAWSConfigurator AddCognito(string clientId, string clientSecret, string userPoolId, string region)
        {
            ValidateAWSCredentials();

            return AddCognito(
                clientId,
                clientSecret,
                userPoolId,
                _awsOptions.AccessKey,
                _awsOptions.SecretKey,
                region);
        }

        public IAWSConfigurator AddCognito(string clientId, string clientSecret, string userPoolId, string accessKey, string secretKey, string region)
        {
            _services.AddSingleton<ICognitoHelper, CognitoHelper>(provider =>
                new CognitoHelper(
                    clientId,
                    clientSecret,
                    userPoolId,
                    accessKey,
                    secretKey,
                    region));

            return this;
        }
        #endregion

        #region S3
        public IAWSConfigurator AddS3(string bucketName, string region)
        {
            ValidateAWSCredentials();

            return AddS3(
                bucketName,
                region,
                _awsOptions.AccessKey,
                _awsOptions.SecretKey);
        }

        public IAWSConfigurator AddS3(string bucketName, string region, string accessKey, string secretKey)
        {
            if (_localStackOptions.UseLocalStack)
            {
                _services.AddSingleton<IS3Helper, S3Helper>(provider =>
                    new S3Helper(bucketName, accessKey, secretKey, region, true, _localStackOptions.URL));
            }
            else
            {
                _services.AddSingleton<IS3Helper, S3Helper>(provider =>
                    new S3Helper(bucketName, accessKey, secretKey, region));
            }

            return this;
        }

        public IAWSConfigurator AddS3(string bucketName)
        {
            ValidateAWSCredentials();
            ValidateAWSDefaultRegion();

            return AddS3(
                bucketName,
                SD.DefaultRegion,
                _awsOptions.AccessKey,
                _awsOptions.SecretKey);
        }

        public IAWSConfigurator AddS3(string bucketName, string accessKey, string secretKey)
        {
            ValidateAWSDefaultRegion();

            return AddS3(
                bucketName,
                SD.DefaultRegion,
                accessKey,
                secretKey);
        }
        #endregion

        #region SES
        public IAWSConfigurator AddSES(string sourceMail)
        {
            ValidateAWSCredentials();
            ValidateAWSDefaultRegion();

            return AddSES(
                _awsOptions.AccessKey,
                _awsOptions.SecretKey,
                sourceMail,
                SD.DefaultRegion);
        }

        public IAWSConfigurator AddSES(string sourceMail, string region)
        {
            ValidateAWSCredentials();

            return AddSES(
                _awsOptions.AccessKey,
                _awsOptions.SecretKey,
                sourceMail,
                region);
        }

        public IAWSConfigurator AddSES(string accessKey, string secretKey, string sourceMail)
        {
            ValidateAWSDefaultRegion();

            return AddSES(
                accessKey,
                secretKey,
                sourceMail,
                SD.DefaultRegion);
        }

        public IAWSConfigurator AddSES(string accessKey, string secretKey, string sourceMail, string region)
        {
            _services.AddSingleton<SESHelper, SESHelper>(provider =>
                new SESHelper(accessKey, secretKey, sourceMail, region));

            return this;
        }
        #endregion

        #region SQS
        public IAWSConfigurator AddSQS(string queueUrl)
        {
            ValidateAWSCredentials();
            ValidateAWSDefaultRegion();

            return AddSQS(
                _awsOptions.AccessKey,
                _awsOptions.SecretKey,
                queueUrl,
                SD.DefaultRegion);
        }

        public IAWSConfigurator AddSQS(string queueUrl, string region)
        {
            ValidateAWSCredentials();

            return AddSQS(
                _awsOptions.AccessKey,
                _awsOptions.SecretKey,
                queueUrl,
                region);
        }

        public IAWSConfigurator AddSQS(string accessKey, string secretKey, string queueUrl)
        {
            ValidateAWSDefaultRegion();

            return AddSQS(
                accessKey,
                secretKey,
                queueUrl,
                SD.DefaultRegion);
        }

        public IAWSConfigurator AddSQS(string accessKey, string secretKey, string queueUrl, string region)
        {
            _services.AddSingleton<ISQSHelper, SQSHelper>(provider =>
                new SQSHelper(accessKey, secretKey, region, queueUrl));

            return this;
        }
        #endregion

        public IAWSConfigurator SetCredentials(string accessKey, string secretKey)
        {
            _awsOptions.AccessKey = accessKey;
            _awsOptions.SecretKey = secretKey;

            return this;
        }

        public IAWSConfigurator SetDefaultRegion(string region)
        {
            SD.DefaultRegion = region;

            return this;
        }

        public IAWSConfigurator UseLocalStack(string? url = null)
        {
            _localStackOptions.UseLocalStack = true;

            if (url == null)
            {
                _localStackOptions.URL = "http://localhost:4572";

                return this;
            }

            _localStackOptions.URL = url;

            return this;
        }


        #region Validations
        private void ValidateAWSCredentials()
        {
            if (string.IsNullOrEmpty(_awsOptions.SecretKey) && string.IsNullOrEmpty(_awsOptions.AccessKey))
            {
                throw new ArgumentException("Hasn't been set up AWS Credentials");
            }
        }

        private void ValidateAWSDefaultRegion()
        {
            if (string.IsNullOrEmpty(SD.DefaultRegion))
            {
                throw new ArgumentException("Hasn't been set up default aws region");
            }
        }
        #endregion
    }
}