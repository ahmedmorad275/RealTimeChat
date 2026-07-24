namespace RealTimeChat.Application.Conversations
{
    public class ConversationMemberResponse
    {
        public Guid UserId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string? ProfilePictureUrl { get; set; }

        public bool IsOnline { get; set; }
    }
}
