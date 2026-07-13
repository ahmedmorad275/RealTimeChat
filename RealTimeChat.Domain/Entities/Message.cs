using RealTimeChat.Domain.Common;
using RealTimeChat.Domain.Enums;

namespace RealTimeChat.Domain.Entities
{
    public class Message : AuditableEntity
    {
        public Guid ConversationId { get; set; }

        public Guid SenderId { get; set; }

        public string? Content { get; set; }

        public MessageType Type { get; set; }

        public DateTime? EditedAt { get; set; }

        public Guid? ReplyToMessageId { get; set; }

        public Conversation Conversation { get; set; } = default!;

        public ApplicationUser Sender { get; set; } = default!;

        public Message? ReplyToMessage { get; set; }
    }
}
