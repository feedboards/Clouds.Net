> [!WARNING]
> I wanted to clarify that our libraries do not include all AWS functionalities. It has been developed over time based on our experiences using AWS in .NET applications. If you're interested in adding new functions, we highly encourage contributions from the community. Your feedback and involvement are greatly appreciated.

# Documentation

> [!WARNING]
> documentation is currently being written.

# Overview

## Amazon S3

- `Exists(string s3Key)`: Checks if an S3 object exists using its key.
- `Exists(string bucketName, string s3Key`: Checks if an S3 object exists in a specified bucket using its key.
- `DownloadAsync(string localFile, string s3Key)`: Asynchronously downloads an S3 object to a local file using its key.
- `DownloadAsync(string bucketName, string localFile, string s3Key)`: Asynchronously downloads an S3 object from a specified bucket to a local file.
- `DeleteAsync(string s3Key)`: Asynchronously deletes an S3 object using its key.
- `DeleteAsync(string bucketName, string s3Key)`: Asynchronously deletes an S3 object from a specified bucket.
- `UploadOrUpdateAsPublicToReadAsync(string localFile, string s3Key)`: Asynchronously uploads or updates a local file to S3, setting it as public-read.
- `UploadOrUpdateAsPublicToReadAsync(string bucketName, string localFile, string s3Key)`: Asynchronously uploads or updates a local file to a specified bucket on S3, setting it as public-read.
- `UploadOrUpdateAsPublicToReadAsync(Stream stream, string s3Key)`: Asynchronously uploads or updates a stream to S3, setting it as public-read.
- `UploadOrUpdateAsPublicToReadAsync(string bucketName, Stream stream, string s3Key)`: Asynchronously uploads or updates a stream to a specified bucket on S3, setting it as public-read.
- `UploadOrUpdateAsync(string localFile, string s3Key)`: Asynchronously uploads or updates a local file to S3.
- `UploadOrUpdateAsync(string bucketName, string localFile, string s3Key)`: Asynchronously uploads or updates a local file to a specified bucket on S3.
- `UploadOrUpdateAsync(Stream stream, string s3Key)`: Asynchronously uploads or updates a stream to S3.
- `UploadOrUpdateAsync(string bucketName, Stream stream, string s3Key)`: Asynchronously uploads or updates a stream to a specified bucket on S3.
- `GetObjectsV2()`: Asynchronously retrieves a list of all S3 objects.
- `GetObjectsV2(string bucketName)`: Asynchronously retrieves a list of all S3 objects in a specified bucket.
- `GetObject(string s3Key)`: Asynchronously retrieves an S3 object using its key.
- `GetObject(string bucketName, string s3Key)`: Asynchronously retrieves an S3 object from a specified bucket.
- `GetObjectUrlPublicFile(string region, string s3Key)`: Retrieves the public URL of an S3 object in a specified region.
- `GetObjectUrlPublicFile(string bucketName, string region, string s3Key)`: Retrieves the public URL of an S3 object from a specified bucket in a region.
- `GetObjectUrlByDefaultUTC(string s3Key)`: Retrieves the URL of an S3 object with a default UTC timestamp.
- `GetObjectUrlByDefaultUTC(string bucketName, string s3Key)`: Retrieves the URL of an S3 object from a specified bucket with a default UTC timestamp.
- `GetObjectUrlByDefaultNow(string s3Key)`: Retrieves the URL of an S3 object with the current time as default.
- `GetObjectUrlByDefaultNow(string bucketName, string s3Key)`: Retrieves the URL of an S3 object from a specified bucket with the current time as default.
- `GetObjectUrlByUTC(string s3Key, TimeSpan expires)`: Retrieves the URL of an S3 object that expires after a specified duration from UTC now.
- `GetObjectUrlByUTC(string bucketName, string s3Key, TimeSpan expires)`: Retrieves the URL of an S3 object from a specified bucket that expires after a specified duration from UTC now.
- `GetObjectUrlByNow(string s3Key, TimeSpan expires)`: Retrieves the URL of an S3 object that expires after a specified duration from now.
- `GetObjectUrlByNow(string bucketName, string s3Key, TimeSpan expires)`: Retrieves the URL of an S3 object from a specified bucket that expires after a specified duration from now.
- `GetS3Key(string file, List<string> folders)`: Generates an S3 key for a file located within specified folders.
- `GetS3KeyFromUrl(string url)`: Extracts the S3 key from a given URL.

## Amazon Cognito

- `GetUserAsync(string accessToken)`: Asynchronously retrieves a user's information using their access token.
- `DeleteUserAsync(string email)`: Asynchronously deletes a user based on their email address.
- `InitiateAuthAsync(string email, string password)`: Asynchronously initiates an authentication session using an email and password.
- `SignUpAsync(string username, string password, List<AttributeType> attributes)`: Asynchronously registers a new user with a username, password, and a list of additional attributes.
- `GetUserRoleAsync(string accessToken)`: Asynchronously retrieves the role of a user using their access token.
- `GetSubIdByAccessToken(string accessToken)`: Retrieves the subject identifier (Sub ID) associated with a given access token.

## Amazon SES

- `SendMail(SESRequestDto obj)`: Asynchronously sends an email using the details specified in the SESRequestDto object.
- `SendMail(SESRequestDto obj, string sourceMail)`: Asynchronously sends an email using the details specified in the SESRequestDto object and allows specifying a source email address.

## Amazon SQS

- `WaitForNewMessages<T>()`: Asynchronously waits for and retrieves new messages of type T from the default queue.
- `WaitForNewMessages<T>(string queueUrl)`: Asynchronously waits for and retrieves new messages of type T from a specified queue URL.
- `WaitForNewMessages<T>(int waitTimeSeconds)`

# How to add and configure `Clouds.Net.AWS` to the project

Here’s a step-by-step guide on how to add `Clouds.Net.AWS`

## installation

```
dotnet add package Clouds.Net.AWS
```

configure dependency injection

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

### Amazon S3

- `AddS3(string bucketName)`: Adds an Amazon S3 service configuration using the default credentials and region.
- `AddS3(string bucketName, string region)`: Specifies the region for S3 configuration.
- `AddS3(string bucketName, string accessKey, string secretKey)`: Adds S3 with specific credentials.
- `AddS3(string bucketName, string region, string accessKey, string secretKey)`: Fully specifies S3 configuration with designated credentials and region.

### Amazon Cognito

- `AddCognito(string clientId, string clientSecret, string userPoolId)`: Sets up Amazon Cognito with the default settings.
- `AddCognito(string clientId, string clientSecret, string userPoolId, string region)`: Specifies the region for Cognito configuration.
- `AddCognito(string clientId, string clientSecret, string userPoolId, string accessKey, string secretKey)`: Configures Cognito with specific credentials.
- `AddCognito(string clientId, string clientSecret, string userPoolId, string accessKey, string secretKey, string region)`: Fully configures Cognito with specified credentials and region.

### Amazon SES

- `AddSES(string sourceMail)`: Configures Amazon SES with default settings.
- `AddSES(string sourceMail, string region)`: Specifies the region for SES configuration.
- `AddSES(string accessKey, string secretKey, string sourceMail)`: Adds SES with specific credentials.
- `AddSES(string accessKey, string secretKey, string sourceMail, string region)`: Fully specifies SES configuration with designated credentials and region.

### Amazon SQS

- `AddSQS(string queueUrl)`: Sets up Amazon SQS using the default settings.
- `AddSQS(string queueUrl, string region)`: Specifies the region for SQS configuration.
- `AddSQS(string accessKey, string secretKey, string queueUrl)`: Configures SQS with specific credentials.
- `AddSQS(string accessKey, string secretKey, string queueUrl, string region)`: Fully specifies SQS configuration with designated credentials and region.

> [!NOTE]
> default AWS region is already set to `us-east-1`

> [!WARNING]
> Not all services have a `LocalStack` implementation.
>
> `LocalStack` implementation currently only supports `Amazon S3`.

# Example Usage

## Amazon S3

```c#

```
