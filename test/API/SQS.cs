using Amazon.SQS.Model;
using Clouds.Net.AWS.Interfaces;

namespace API
{
    public class SQS
    {
        private readonly ISQSHelper _SQSHelper;

        public SQS(ISQSHelper SQSHelper)
        {
            _SQSHelper = SQSHelper;
        }

        public async Task Run()
        {
            var waitTimeSeconds = 5;

            await _SQSHelper.WaitForNewMessages<string>(waitTimeSeconds, "your-queue-url");

            var yourMessages = new List<Message>();

            await _SQSHelper.DeleteMessages(yourMessages, "your-queue-url");

            var yourMessage = new Message();

            await _SQSHelper.DeleteMessage(yourMessage, "your-queue-url");

            var yourMessagesWithOwnType = new List<string>() { "your-message" };

            await _SQSHelper.AddNewMessages<string>(yourMessagesWithOwnType, "your-queue-url");

            var yourMessageWithOwnType = "your-message";

            await _SQSHelper.AddNewMessage<string>(yourMessageWithOwnType, "your-queue-url");
        }
    }
}
