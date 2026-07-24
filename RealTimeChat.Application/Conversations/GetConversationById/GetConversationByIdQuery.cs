using MediatR;

namespace RealTimeChat.Application.Conversations.GetConversationById
{
    public class GetConversationByIdQuery : IRequest<ConversationResponse>
    {
        public Guid ConversationId { get; set; }
    }
}
