using Amazon.SQS.Model;

namespace Clouds.Net.AWS.Interfaces
{
    public interface ISQSHelper
    {
        Task<List<T?>> WaitForNewMessages<T>();
        Task<List<T?>> WaitForNewMessages<T>(string queueUrl);
        Task<List<T?>> WaitForNewMessages<T>(int waitTimeSeconds);
        Task<List<T?>> WaitForNewMessages<T>(int waitTimeSeconds, string queueUrl);
        Task<T?> WaitForNewMessage<T>();
        Task<T?> WaitForNewMessage<T>(string queueUrl);
        Task<T?> WaitForNewMessage<T>(int waitTimeSeconds);
        Task<T?> WaitForNewMessage<T>(int waitTimeSeconds, string queueUrl);
        Task DeleteMessages(List<Message> messages);
        Task DeleteMessages(List<Message> messages, string queueUrl);
        Task DeleteMessage(Message message);
        Task DeleteMessage(Message message, string queueUrl);
        Task<List<T>> AddNewMessages<T>(List<T> messages) where T : notnull;
        Task<List<T>> AddNewMessages<T>(List<T> messages, string queueUrl) where T : notnull;
        Task<T> AddNewMessage<T>(T message) where T : notnull;
        Task<T> AddNewMessage<T>(T message, string queueUrl) where T : notnull;
    }
}
