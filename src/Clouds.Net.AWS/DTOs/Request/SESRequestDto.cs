namespace Clouds.Net.AWS.DTOs.Request
{
    public class SESRequestDto
    {
        public required string ReceiverAddress { get; set; }
        public required string Subject { get; set; }
        public required string TextBody { get; set; }
        public string? HtmlBody { get; set; } = null;
    }
}
