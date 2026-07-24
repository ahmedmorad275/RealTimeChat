using AutoMapper;
using MediatR;
using RealTimeChat.Application.Interfaces;
using RealTimeChat.Application.Interfaces.Repositories;
using RealTimeChat.Application.Interfaces.Services;
using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Application.Conversations.CreateGroupConversation
{
    public class CreateGroupConversationCommandHandler : IRequestHandler<CreateGroupConversationCommand, ConversationResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;
        private readonly IConversationRepository _conversationRepository;
        private readonly IApplicationDbContext _context;

        public CreateGroupConversationCommandHandler(IMapper mapper, ICurrentUser currentUser, IConversationRepository conversationRepository, IApplicationDbContext context)
        {
            _mapper = mapper;
            _currentUser = currentUser;
            _conversationRepository = conversationRepository;
            _context = context;
        }

        public async Task<ConversationResponse> Handle(CreateGroupConversationCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUser.UserId;

            var conversation = Conversation.CreateGroup(currentUserId, request.Name, request.Description, request.MemberIds);

            await _conversationRepository.AddAsync(conversation);

            var groupDetails = new GroupDetails()
            {
                ConversationId = conversation.Id,
                CreatedByUserId = currentUserId,
                Description = request.Description,
                Name = request.Name
            };

            await _context.GroupDetails.AddAsync(groupDetails, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var created = await _conversationRepository.GetConversationWithMembersAsync(conversation.Id);

            return _mapper.Map<ConversationResponse>(created);
        }
    }
}
