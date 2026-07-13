using RealTimeChat.Domain.Common;

namespace RealTimeChat.Domain.Entities
{
    public class Attachment : BaseEntity
    {
        public Guid MessageId { get; set; }

        public string FileName { get; set; } = default!;

        public string FileUrl { get; set; } = default!;

        public string ContentType { get; set; } = default!;

        public Message Message { get; set; } = default!;
    }
}
