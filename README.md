> [!WARNING]
> I wanted to clarify that our libraries do not include all AWS and Azure functionalities. It has been developed over time based on our experiences using AWS and Azure in .NET applications. If you're interested in adding new functions, we highly encourage contributions from the community. Your feedback and involvement are greatly appreciated.

# Documentation

> [!WARNING]
> documentation is currently being written.

## Installetion

```
dotnet add package Clouds.Net.AWS
```

## How to add `Clouds.Net.AWS` to my project

Here’s a step-by-step guide on how to add `Clouds.Net.AWS`

### Configuration Methods

#### Basic Configuration

- `SetDefaultRegion(string region)`: Sets the default AWS region for all services unless overridden.
- `SetDefaultCredentials(string accessKey, string secretKey)`: Sets the default AWS credentials for all services unless overridden.
- `UseLocalStack(string? url = null)`: Configures the application to interact with LocalStack, ideal for local development and testing.

> [!NOTE]
> default AWS region is already set to `us-east-1`

> [!WARNING]
> Not all services have a `LocalStack` implementation.
>
> `LocalStack` implementation currently only supports `Amazon S3`.

#### Amazon S3

- `AddS3(string bucketName)`: Adds an Amazon S3 service configuration using the default credentials and region.
- `AddS3(string bucketName, string region)`: Specifies the region for S3 configuration.
- `AddS3(string bucketName, string accessKey, string secretKey)`: Adds S3 with specific credentials.
- `AddS3(string bucketName, string region, string accessKey, string secretKey)`: Fully specifies S3 configuration with designated credentials and region.

#### Amazon Cognito

- `AddCognito(string clientId, string clientSecret, string userPoolId)`: Sets up Amazon Cognito with the default settings.
- `AddCognito(string clientId, string clientSecret, string userPoolId, string region)`: Specifies the region for Cognito configuration.
- `AddCognito(string clientId, string clientSecret, string userPoolId, string accessKey, string secretKey)`: Configures Cognito with specific credentials.
- `AddCognito(string clientId, string clientSecret, string userPoolId, string accessKey, string secretKey, string region)`: Fully configures Cognito with specified credentials and region.

#### Amazon SES

- `AddSES(string sourceMail)`: Configures Amazon SES with default settings.
- `AddSES(string sourceMail, string region)`: Specifies the region for SES configuration.
- `AddSES(string accessKey, string secretKey, string sourceMail)`: Adds SES with specific credentials.
- `AddSES(string accessKey, string secretKey, string sourceMail, string region)`: Fully specifies SES configuration with designated credentials and region.

#### Amazon SQS

- `AddSQS(string queueUrl)`: Sets up Amazon SQS using the default settings.
- `AddSQS(string queueUrl, string region)`: Specifies the region for SQS configuration.
- `AddSQS(string accessKey, string secretKey, string queueUrl)`: Configures SQS with specific credentials.
- `AddSQS(string accessKey, string secretKey, string queueUrl, string region)`: Fully specifies SQS configuration with designated credentials and region.

### Example Usage

```c#
builder.Services.AddAWSServices(options =>
{
    options.SetDefaultRegion("us-east-1");
    options.SetDefaultCredentials("your-access-key", "your-secret-key");

    options.AddS3("your-bucket-name");

    options.AddCognito("client-id", "client-secret", "user-pool-id");

    options.AddSES("your-email@example.com");

    options.AddSQS("your-queue-url");
});
```
