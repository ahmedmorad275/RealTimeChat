using MediatR;

namespace RealTimeChat.Application.Conversations.CreateGroupConversation
{
    public class CreateGroupConversationCommand : IRequest<ConversationResponse>
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public List<Guid> MemberIds { get; set; } = [];
    }
}
