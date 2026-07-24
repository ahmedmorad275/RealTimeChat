using AutoMapper;
using MediatR;
using RealTimeChat.Application.Interfaces.Repositories;
using RealTimeChat.Application.Interfaces.Services;
using RealTimeChat.Domain.Entities;
using RealTimeChat.Domain.Exceptions;

namespace RealTimeChat.Application.Conversations.GetConversationById
{
    public class GetConversationByIdQueryHandler : IRequestHandler<GetConversationByIdQuery, ConversationResponse>
    {
        private readonly IMapper _mapper;
        private readonly IConversationRepository _conversationRepository;
        private readonly ICurrentUser _currentUser;

        public GetConversationByIdQueryHandler(IMapper mapper, IConversationRepository conversationRepository, ICurrentUser currentUser)
        {
            _mapper = mapper;
            _conversationRepository = conversationRepository;
            _currentUser = currentUser;
        }

        public async Task<ConversationResponse> Handle(GetConversationByIdQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUser.UserId;

            var conversation = await _conversationRepository.GetConversationWithMembersAsync(request.ConversationId);

            if (conversation is null)
                throw new NotFoundException(nameof(Conversation), request.ConversationId);

            if (!conversation.Members.Any(x => x.UserId == currentUserId))
                throw new ForbiddenException("You are not a member of this conversation.");

            return _mapper.Map<ConversationResponse>(conversation);
        }
    }
}
