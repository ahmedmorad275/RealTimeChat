using AutoMapper;
using MediatR;
using RealTimeChat.Application.Interfaces;
using RealTimeChat.Application.Interfaces.Repositories;
using RealTimeChat.Application.Interfaces.Services;
using RealTimeChat.Domain.Entities;
using RealTimeChat.Domain.Exceptions;

namespace RealTimeChat.Application.Conversations.CreatePrivateConversation
{
    public class CreatePrivateConversationCommandHandler : IRequestHandler<CreatePrivateConversationCommand, ConversationResponse>
    {
        private readonly IConversationRepository _repository;
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;

        public CreatePrivateConversationCommandHandler(IConversationRepository repository, IApplicationDbContext context, ICurrentUser currentUser, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<ConversationResponse> Handle(CreatePrivateConversationCommand request, CancellationToken cancellationToken)
        {
            var currnetUserId = _currentUser.UserId;

            if (request.SecondUserId == currnetUserId)
                throw new BadRequestException("You can't start conversation with yourself.");

            var existing = await _repository.GetPrivateConversationAsync(currnetUserId, request.SecondUserId);

            if (existing is not null)
                return _mapper.Map<ConversationResponse>(existing);

            var conversation = Conversation.CreatePrivate(currnetUserId, request.SecondUserId);

            await _repository.AddAsync(conversation);
            await _context.SaveChangesAsync(cancellationToken);

            var created = await _repository.GetConversationWithMembersAsync(conversation.Id);

            return _mapper.Map<ConversationResponse>(created);
        }
    }
}
