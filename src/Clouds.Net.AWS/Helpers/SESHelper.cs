using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Clouds.Net.AWS.DTOs.Request;
using Clouds.Net.AWS.Interfaces;
using Clouds.Net.AWS.Utils;

namespace Clouds.Net.AWS.Helpers
{
    public class SESHelper : ISESHelper
    {
        private readonly AmazonSimpleEmailServiceClient _client;
        private readonly string _sourceMail;

        public SESHelper(
            string accessKey,
            string secretKey,
            string sourceMail)
            : this(
                accessKey,
                  secretKey,
                  sourceMail,
                  SD.DefaultRegion)
        {
        }

        public SESHelper(
            string accessKey,
            string secretKey,
            string sourceMail,
            string region)
        {
            _client = new AmazonSimpleEmailServiceClient(
                accessKey,
                secretKey,
                AWSUtils.GetRegionFromString(region));

            _sourceMail = sourceMail;
        }

        public async Task<bool> SendMail(SESRequestDto obj)
        {
            return await SendMail(obj, _sourceMail);
        }

        public async Task<bool> SendMail(SESRequestDto obj, string sourceMail)
        {
            try
            {
                var emailRequest = new SendEmailRequest()
                {
                    Source = sourceMail,
                    Destination = new Destination
                    {
                        ToAddresses = new List<string> { obj.ReceiverAddress }
                    },
                    Message = new Message
                    {
                        Subject = new Content
                        {
                            Charset = "UTF-8",
                            Data = obj.Subject,
                        },
                        Body = new Body
                        {
                            Text = new Content
                            {
                                Charset = "UTF-8",
                                Data = obj.TextBody
                            }
                        }
                    }
                };

                if (obj.HtmlBody != null)
                {
                    emailRequest.Message.Body.Html = new Content
                    {
                        Charset = "UTF-8",
                        Data = obj.HtmlBody
                    };
                }

                var response = await _client.SendEmailAsync(emailRequest);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
