using RealTimeChat.Domain.Common;
using RealTimeChat.Domain.Enums;

namespace RealTimeChat.Domain.Entities
{
    public class Conversation : AuditableEntity
    {
        public ConversationType Type { get; private set; }

        public Guid? LastMessageId { get; set; }

        public DateTime? LastMessageAt { get; set; }

        public ICollection<ConversationMember> Members { get; set; } = [];
    }
}
