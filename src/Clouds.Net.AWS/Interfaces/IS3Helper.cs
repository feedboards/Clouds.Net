using Amazon.S3.Model;

namespace Clouds.Net.AWS.Interfaces
{
    public interface IS3Helper : IDisposable
    {
        Task<bool> Exists(string s3Key);
        Task<bool> Exists(string bucketName, string s3Key);
        Task DownloadAsync(string localFile, string s3Key);
        Task DownloadAsync(string bucketName, string localFile, string s3Key);
        Task DeleteAsync(string s3Key);
        Task DeleteAsync(string bucketName, string s3Key);
        Task UploadOrUpdateAsPublicToReadAsync(string localFile, string s3Key);
        Task UploadOrUpdateAsPublicToReadAsync(string bucketName, string localFile, string s3Key);
        Task UploadOrUpdateAsPublicToReadAsync(Stream stream, string s3Key);
        Task UploadOrUpdateAsPublicToReadAsync(string bucketName, Stream stream, string s3Key);
        Task UploadOrUpdateAsync(string localFile, string s3Key);
        Task UploadOrUpdateAsync(string bucketName, string localFile, string s3Key);
        Task UploadOrUpdateAsync(Stream stream, string s3Key);
        Task UploadOrUpdateAsync(string bucketName, Stream stream, string s3Key);
        Task<ListObjectsV2Response> GetObjectsV2();
        Task<ListObjectsV2Response> GetObjectsV2(string bucketName);
        Task<GetObjectResponse> GetObject(string s3Key);
        Task<GetObjectResponse> GetObject(string bucketName, string s3Key);
        string GetObjectUrlPublicFile(string region, string s3Key);
        string GetObjectUrlPublicFile(string bucketName, string region, string s3Key);
        string GetObjectUrlByDefaultUTC(string s3Key);
        string GetObjectUrlByDefaultUTC(string bucketName, string s3Key);
        string GetObjectUrlByDefaultNow(string s3Key);
        string GetObjectUrlByDefaultNow(string bucketName, string s3Key);
        string GetObjectUrlByUTC(string s3Key, TimeSpan expires);
        string GetObjectUrlByUTC(string bucketName, string s3Key, TimeSpan expires);
        string GetObjectUrlByNow(string s3Key, TimeSpan expires);
        string GetObjectUrlByNow(string bucketName, string s3Key, TimeSpan expires);
        string GetS3Key(string file, List<string> folders);
        string GetS3Key(string file);
        string GetS3KeyFromUrl(string url);
    }
}
