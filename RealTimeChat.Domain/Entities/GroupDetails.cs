using RealTimeChat.Domain.Common;

namespace RealTimeChat.Domain.Entities
{
    public class GroupDetails : BaseEntity
    {
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public Guid ConversationId { get; set; }

        public Guid CreatedByUserId { get; set; }

        public Conversation Conversation { get; set; } = default!;

        public ApplicationUser CreatedByUser { get; set; } = default!;

    }
}
