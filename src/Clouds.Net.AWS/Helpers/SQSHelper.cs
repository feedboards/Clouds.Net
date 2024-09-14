using Amazon.SQS;
using Amazon.SQS.Model;
using Clouds.Net.AWS.Interfaces;
using Clouds.Net.AWS.Utils;
using Newtonsoft.Json;

namespace Clouds.Net.AWS.Helpers
{
    public class SQSHelper : ISQSHelper
    {
        private readonly AmazonSQSClient _client;
        private readonly string _queueUrl;

        public SQSHelper(
            string accessKey,
            string secretKey,
            string region,
            string queueUrl)
        {
            _client = new AmazonSQSClient(accessKey, secretKey, new AmazonSQSConfig
            {
                RegionEndpoint = AWSUtils.GetRegionFromString(region),
            });

            _queueUrl = queueUrl;
        }

        public SQSHelper(
            string accessKey,
            string secretKey,
            string queueUrl)
            : this(
                  accessKey,
                  secretKey,
                  SD.DefaultRegion,
                  queueUrl)
        {
        }

        public async Task<List<T?>> WaitForNewMessages<T>()
        {
            return await WaitForNewMessages<T>(_queueUrl);
        }

        public async Task<List<T?>> WaitForNewMessages<T>(string queueUrl)
        {
            var messages = await _client.ReceiveMessageAsync(new ReceiveMessageRequest
            {
                QueueUrl = queueUrl,
                WaitTimeSeconds = 20
            });

            return messages.Messages.ConvertAll(message => JsonConvert.DeserializeObject<T>(message.Body));
        }

        public async Task<List<T?>> WaitForNewMessages<T>(int waitTimeSeconds)
        {
            return await WaitForNewMessages<T>(waitTimeSeconds, _queueUrl);
        }

        public async Task<List<T?>> WaitForNewMessages<T>(int waitTimeSeconds, string queueUrl)
        {
            var messages = await _client.ReceiveMessageAsync(new ReceiveMessageRequest
            {
                QueueUrl = queueUrl,
                WaitTimeSeconds = waitTimeSeconds
            });

            return messages.Messages.ConvertAll(message => JsonConvert.DeserializeObject<T>(message.Body));
        }

        public async Task<T?> WaitForNewMessage<T>()
        {
            return await WaitForNewMessage<T>(_queueUrl);
        }

        public async Task<T?> WaitForNewMessage<T>(string queueUrl)
        {
            var response = await _client.ReceiveMessageAsync(new ReceiveMessageRequest
            {
                QueueUrl = queueUrl,
                MaxNumberOfMessages = 1,
                WaitTimeSeconds = 20
            });

            var message = response.Messages.Count > 0 ? JsonConvert.DeserializeObject<T>(response.Messages[0].Body) : default;

            return message;
        }

        public async Task<T?> WaitForNewMessage<T>(int waitTimeSeconds)
        {
            return await WaitForNewMessage<T>(waitTimeSeconds, _queueUrl);
        }

        public async Task<T?> WaitForNewMessage<T>(int waitTimeSeconds, string queueUrl)
        {
            var response = await _client.ReceiveMessageAsync(new ReceiveMessageRequest
            {
                QueueUrl = queueUrl,
                MaxNumberOfMessages = 1,
                WaitTimeSeconds = waitTimeSeconds
            });

            var message = response.Messages.Count > 0 ? JsonConvert.DeserializeObject<T>(response.Messages[0].Body) : default;

            return message;
        }

        public async Task DeleteMessages(List<Message> messages)
        {
            await DeleteMessages(messages, _queueUrl);
        }

        public async Task DeleteMessages(List<Message> messages, string queueUrl)
        {
            var deleteTasks = new List<Task>();

            foreach (var message in messages)
            {
                deleteTasks.Add(_client.DeleteMessageAsync(new()
                {
                    QueueUrl = queueUrl,
                    ReceiptHandle = message.ReceiptHandle
                }));
            }

            await Task.WhenAll(deleteTasks);
        }

        public async Task DeleteMessage(Message message)
        {
            await DeleteMessage(message, _queueUrl);
        }

        public async Task DeleteMessage(Message message, string queueUrl)
        {
            await _client.DeleteMessageAsync(new DeleteMessageRequest
            {
                QueueUrl = queueUrl,
                ReceiptHandle = message.ReceiptHandle
            });
        }

        public async Task<List<T>> AddNewMessages<T>(List<T> messages) where T : notnull
        {
            return await AddNewMessages(messages, _queueUrl);
        }

        public async Task<List<T>> AddNewMessages<T>(List<T> messages, string queueUrl) where T : notnull
        {
            var tasks = new List<Task>();

            foreach (var message in messages)
            {
                tasks.Add(AddNewMessage(message, queueUrl));
            }

            await Task.WhenAll(tasks);
            return messages;
        }

        public async Task<T> AddNewMessage<T>(T message) where T : notnull
        {
            return await AddNewMessage(message, _queueUrl);
        }

        public async Task<T> AddNewMessage<T>(T message, string queueUrl) where T : notnull
        {
            var serializeMessage = JsonConvert.SerializeObject(message);

            await _client.SendMessageAsync(new SendMessageRequest
            {
                QueueUrl = queueUrl,
                MessageBody = serializeMessage
            });

            return message;
        }
    }
}
