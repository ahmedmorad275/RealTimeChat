using MediatR;

namespace RealTimeChat.Application.Conversations.GetUserConversations
{
    public class GetUserConversationsQuery : IRequest<List<ConversationResponse>>
    {

    }
}
