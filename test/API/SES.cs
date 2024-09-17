using Clouds.Net.AWS.DTOs.Request;
using Clouds.Net.AWS.Interfaces;

namespace API
{
    public class SES
    {
        private readonly ISESHelper _SESHelper;

        public SES(ISESHelper SESHelper)
        {
            _SESHelper = SESHelper;
        }

        public async Task Run()
        {
            var message = new SESRequestDto()
            {
                ReceiverAddress = "user-email",
                Subject = "subject",
                TextBody = "your-message",
                HtmlBody = "your-html-code"
            };

            await _SESHelper.SendMail(message, "your-email");
        }
    }
}
