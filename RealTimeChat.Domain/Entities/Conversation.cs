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

        private Conversation() { }

        public static Conversation CreatePrivate(Guid firstUserId, Guid secondUserId)
        {
            var conversation = new Conversation() { Type = ConversationType.Private };

            conversation.Members.Add(new ConversationMember() { UserId = firstUserId, JoinedAt = DateTime.UtcNow });
            conversation.Members.Add(new ConversationMember() { UserId = secondUserId, JoinedAt = DateTime.UtcNow });

            return conversation;
        }

        public static Conversation CreateGroup(Guid creatorId, string name, string? description, IEnumerable<Guid> memberIds)
        {
            var conversation = new Conversation() { Type = ConversationType.Group };
            conversation.Members.Add(new ConversationMember() { UserId = creatorId, JoinedAt = DateTime.UtcNow, IsAdmin = true });

            foreach (var memberId in memberIds.Where(id => id != creatorId).Distinct())
            {
                conversation.Members.Add(new ConversationMember() { UserId = memberId, JoinedAt = DateTime.UtcNow });
            }

            return conversation;
        }
    }
}
