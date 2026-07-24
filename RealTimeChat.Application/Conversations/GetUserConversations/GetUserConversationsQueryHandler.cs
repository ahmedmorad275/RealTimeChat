using AutoMapper;
using MediatR;
using RealTimeChat.Application.Interfaces.Repositories;
using RealTimeChat.Application.Interfaces.Services;

namespace RealTimeChat.Application.Conversations.GetUserConversations
{
    public class GetUserConversationsQueryHandler : IRequestHandler<GetUserConversationsQuery, List<ConversationResponse>>
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;

        public GetUserConversationsQueryHandler(ICurrentUser currentUser, IMapper mapper, IConversationRepository conversationRepository)
        {
            _currentUser = currentUser;
            _mapper = mapper;
            _conversationRepository = conversationRepository;
        }

        public async Task<List<ConversationResponse>> Handle(GetUserConversationsQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUser.UserId;

            var conversations = await _conversationRepository.GetUserConversationsAsync(currentUserId);

            return _mapper.Map<List<ConversationResponse>>(conversations);
        }
    }
}
