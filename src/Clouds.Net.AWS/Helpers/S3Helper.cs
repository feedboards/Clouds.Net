using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Clouds.Net.AWS.Interfaces;
using Clouds.Net.AWS.Utils;
using System.Net;
using System.Text;

namespace Clouds.Net.AWS.Helpers
{
    public class S3Helper : IS3Helper
    {
        private readonly AmazonS3Client _client;
        private readonly TransferUtility _transferUtility;
        private readonly string _bucketName;

        public S3Helper(
            string bucketName,
            string accessKey,
            string secretKey,
            string region,
            bool useLocalStack = false,
            string? localStackUrl = null)
        {
            var config = new AmazonS3Config();

            _bucketName = bucketName;

            config.RegionEndpoint = AWSUtils.GetRegionFromString(region);

            if (useLocalStack)
            {
                config.ServiceURL = localStackUrl;
                config.UseHttp = true;
                config.ForcePathStyle = true;
            }

            _client = new AmazonS3Client(accessKey, secretKey, config);
            _transferUtility = new TransferUtility(_client);
        }

        public S3Helper(
            string bucketName,
            string accessKey,
            string secretKey,
            bool useLocalStack = false)
            : this(
                  bucketName,
                  accessKey,
                  secretKey,
                  SD.DefaultRegion,
                  useLocalStack)
        {
        }

        public async Task<bool> Exists(string s3Key)
        {
            return await Exists(_bucketName, s3Key);
        }

        public async Task<bool> Exists(string bucketName, string s3Key)
        {
            try
            {
                var metadate = await _client.GetObjectMetadataAsync(bucketName, s3Key);

                return true;
            }
            catch (AmazonS3Exception ex)
            {
                if (ex.StatusCode != HttpStatusCode.NotFound)
                {
                    throw;
                }

                return false;
            }
        }

        public async Task DownloadAsync(string path, string s3Key)
        {
            await DownloadAsync(path, _bucketName, s3Key);
        }

        public async Task DownloadAsync(string bucketName, string path, string s3Key)
        {
            await _transferUtility.DownloadAsync(path, bucketName, s3Key);
        }

        public async Task DeleteAsync(string s3Key)
        {
            await DeleteAsync(_bucketName, s3Key);
        }

        public async Task DeleteAsync(string bucketName, string s3Key)
        {
            await _client.DeleteObjectAsync(bucketName, s3Key);
        }

        public async Task UploadOrUpdateAsPublicToReadAsync(string path, string s3Key)
        {
            await UploadOrUpdateAsPublicToReadAsync(_bucketName, path, s3Key);
        }

        public async Task UploadOrUpdateAsPublicToReadAsync(string bucketName, string path, string s3Key)
        {
            await _transferUtility.UploadAsync(new TransferUtilityUploadRequest()
            {
                BucketName = bucketName,
                FilePath = path,
                Key = s3Key,
                CannedACL = S3CannedACL.PublicRead
            });
        }

        public async Task UploadOrUpdateAsPublicToReadAsync(Stream stream, string s3Key)
        {
            await UploadOrUpdateAsPublicToReadAsync(_bucketName, stream, s3Key);
        }

        public async Task UploadOrUpdateAsPublicToReadAsync(string bucketName, Stream stream, string s3Key)
        {
            await _transferUtility.UploadAsync(new TransferUtilityUploadRequest()
            {
                BucketName = bucketName,
                InputStream = stream,
                Key = s3Key,
                CannedACL = S3CannedACL.PublicRead
            });
        }

        public async Task UploadOrUpdateAsync(string path, string s3Key)
        {
            await UploadOrUpdateAsync(_bucketName, path, s3Key);
        }

        public async Task UploadOrUpdateAsync(string bucketName, string path, string s3Key)
        {
            await _transferUtility.UploadAsync(path, bucketName, s3Key);
        }

        public async Task UploadOrUpdateAsync(Stream stream, string s3Key)
        {
            await UploadOrUpdateAsync(_bucketName, stream, s3Key);
        }

        public async Task UploadOrUpdateAsync(string bucketName, Stream stream, string s3Key)
        {
            await _transferUtility.UploadAsync(stream, bucketName, s3Key);
        }

        public async Task<ListObjectsV2Response> GetObjectsV2()
        {
            return await GetObjectsV2(_bucketName);
        }

        public async Task<ListObjectsV2Response> GetObjectsV2(string bucketName)
        {
            return await _client.ListObjectsV2Async(new()
            {
                BucketName = bucketName,
            });
        }

        public async Task<GetObjectResponse> GetObject(string s3Key)
        {
            return await GetObject(_bucketName, s3Key);
        }

        public async Task<GetObjectResponse> GetObject(string bucketName, string s3Key)
        {
            return await _client.GetObjectAsync(bucketName, s3Key);
        }

        public string GetObjectUrlPublicFile(string region, string s3Key)
        {
            return GetObjectUrlPublicFile(_bucketName, region, s3Key);
        }

        public string GetObjectUrlPublicFile(string bucketName, string region, string s3Key)
        {
            return $"https://{bucketName}.s3.{region}.amazonaws.com/{s3Key}";
        }

        public string GetObjectUrlByDefaultUTC(string s3Key)
        {
            return GetObjectUrlByDefaultUTC(_bucketName, s3Key);
        }

        public string GetObjectUrlByDefaultUTC(string bucketName, string s3Key)
        {
            return _client.GetPreSignedURL(new()
            {
                BucketName = bucketName,
                Key = s3Key,
                Expires = DateTime.UtcNow.Add(TimeSpan.FromHours(3)),
            });
        }

        public string GetObjectUrlByDefaultNow(string s3Key)
        {
            return GetObjectUrlByDefaultNow(_bucketName, s3Key);
        }

        public string GetObjectUrlByDefaultNow(string bucketName, string s3Key)
        {
            return _client.GetPreSignedURL(new()
            {
                BucketName = bucketName,
                Key = s3Key,
                Expires = DateTime.Now.Add(TimeSpan.FromHours(3)),
            });
        }

        public string GetObjectUrlByUTC(string s3Key, TimeSpan expires)
        {
            return GetObjectUrlByUTC(_bucketName, s3Key, expires);
        }

        public string GetObjectUrlByUTC(string bucketName, string s3Key, TimeSpan expires)
        {
            return _client.GetPreSignedURL(new()
            {
                BucketName = bucketName,
                Key = s3Key,
                Expires = DateTime.UtcNow.Add(expires),
            });
        }

        public string GetObjectUrlByNow(string s3Key, TimeSpan expires)
        {
            return GetObjectUrlByNow(_bucketName, s3Key, expires);
        }

        public string GetObjectUrlByNow(string bucketName, string s3Key, TimeSpan expires)
        {
            return _client.GetPreSignedURL(new()
            {
                BucketName = bucketName,
                Key = s3Key,
                Expires = DateTime.UtcNow.Add(expires),
            });
        }

        public string GetS3Key(string file, List<string> folders)
        {
            var s3KeyBuilder = new StringBuilder();

            foreach (var folder in folders)
            {
                s3KeyBuilder.AppendLine(folder);
            }

            return $"{s3KeyBuilder}/{file}";
        }

        public string GetS3Key(string file)
        {
            return file;
        }

        public string GetS3KeyFromUrl(string url)
        {
            return Uri.UnescapeDataString(new Uri(url).AbsolutePath[1..]);
        }

        public void Dispose()
        {
            _client.Dispose();
            _transferUtility.Dispose();
        }
    }
}
