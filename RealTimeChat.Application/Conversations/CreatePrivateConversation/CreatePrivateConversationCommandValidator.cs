using FluentValidation;

namespace RealTimeChat.Application.Conversations.CreatePrivateConversation
{
    public class CreatePrivateConversationCommandValidator : AbstractValidator<CreatePrivateConversationCommand>
    {
        public CreatePrivateConversationCommandValidator()
        {
            RuleFor(x => x.SecondUserId).NotEmpty();
        }
    }
}
