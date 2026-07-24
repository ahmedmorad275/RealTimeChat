using FluentValidation;

namespace RealTimeChat.Application.Conversations.CreateGroupConversation
{
    public class CreateGroupConversationCommandValidator : AbstractValidator<CreateGroupConversationCommand>
    {
        public CreateGroupConversationCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Group Name is required.")
                .MaximumLength(200);

            RuleFor(x => x.MemberIds)
                .NotEmpty()
                .WithMessage("Group must have at least one member.");
        }
    }
}
