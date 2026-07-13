using RealTimeChat.Domain.Common;

namespace RealTimeChat.Domain.Entities
{
    public class MessageRead : BaseEntity
    {
        public Guid MessageId { get; set; }

        public Guid UserId { get; set; }

        public DateTime ReadAt { get; set; }

        public Message Message { get; set; } = default!;

        public ApplicationUser User { get; set; } = default!;
    }
}
