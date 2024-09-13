using Clouds.Net.AWS.DTOs.Request;

namespace Clouds.Net.AWS.Interfaces
{
    public interface ISESHelper : IDisposable
    {
        Task<bool> SendMail(SESRequestDto obj);
        Task<bool> SendMail(SESRequestDto obj, string sourceMail);
    }
}
