using RealTimeChat.Domain.Common;

namespace RealTimeChat.Domain.Entities
{
    public class ConversationMember : BaseEntity
    {
        public Guid ConversationId { get; set; }

        public Guid UserId { get; set; }

        public DateTime JoinedAt { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsMuted { get; set; }

        public bool IsPinned { get; set; }

        public bool IsArchived { get; set; }

        public Guid? LastReadMessageId { get; set; }

        public Conversation Conversation { get; private set; } = default!;
        public ApplicationUser User { get; private set; } = default!;
    }
}
