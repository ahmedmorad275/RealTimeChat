using RealTimeChat.Domain.Common;

namespace RealTimeChat.Domain.Entities
{
    public class MessageReaction : BaseEntity
    {
        public Guid MessageId { get; set; }

        public Guid UserId { get; set; }

        public string Reaction { get; set; } = default!;

        public Message Message { get; set; } = default!;

        public ApplicationUser User { get; set; } = default!;
    }
}
