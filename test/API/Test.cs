using Clouds.Net.AWS.Helpers;

namespace API
{
    public class Test
    {
        public readonly S3Helper _s3Helper;

        public Test(S3Helper s3Helper)
        {
            _s3Helper = s3Helper;
        }

        public async void Run()
        {
            var result = await _s3Helper.Exists("your-s3-key");
        }
    }
}
