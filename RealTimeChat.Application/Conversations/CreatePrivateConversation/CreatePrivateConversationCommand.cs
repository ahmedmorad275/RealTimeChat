using MediatR;

namespace RealTimeChat.Application.Conversations.CreatePrivateConversation
{
    public class CreatePrivateConversationCommand : IRequest<ConversationResponse>
    {
        public Guid SecondUserId { get; set; }
    }
}
