using Clouds.Net.AWS.Interfaces;

namespace API
{
    public class S3
    {
        public readonly IS3Helper _s3Helper;

        public S3(IS3Helper s3Helper)
        {
            _s3Helper = s3Helper;
        }

        public async void Run()
        {
            var yourStream = new MemoryStream();
            var yourExpires = TimeSpan.FromDays(1);

            await _s3Helper.Exists("your-bucket-name", "your-s3-key");

            await _s3Helper.DownloadAsync("your-bucket-name", "your-path", "your-s3-key");

            await _s3Helper.DeleteAsync("your-bucket-name", "s3Key");

            await _s3Helper.UploadOrUpdateAsPublicToReadAsync("your-bucket-name", "your-path", "your-s3-key");

            await _s3Helper.UploadOrUpdateAsPublicToReadAsync("your-bucket-name", yourStream, "your-s3-key");

            await _s3Helper.UploadOrUpdateAsync("your-bucket-name", "your-path", "your-s3-key");

            await _s3Helper.UploadOrUpdateAsync("your-bucket-name", yourStream, "your-s3-key");

            await _s3Helper.GetObjectsV2("your-bucket-name");

            await _s3Helper.GetObject("your-bucket-name", "your-s3-key");

            _s3Helper.GetObjectUrlPublicFile("your-bucket-name", "your-region", "your-s3-key");

            _s3Helper.GetObjectUrlByDefaultUTC("your-bucket-name", "your-s3-key");

            _s3Helper.GetObjectUrlByDefaultNow("your-bucket-name", "your-s3-key");

            _s3Helper.GetObjectUrlByUTC("your-bucket-name", "your-s3-key", yourExpires);

            _s3Helper.GetObjectUrlByNow("your-bucket-name", "your-s3-key", yourExpires);

            _s3Helper.GetS3Key("your-file-name", new List<string> { "your-folder" });

            _s3Helper.GetS3KeyFromUrl("your-url");
        }
    }
}
