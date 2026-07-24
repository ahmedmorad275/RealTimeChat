namespace RealTimeChat.Application.Conversations
{
    public class ConversationResponse
    {
        public string Type { get; set; } = string.Empty;

        public Guid Id { get; set; }

        public DateTime? LastMessageAt { get; set; }

        public ICollection<ConversationMemberResponse> Members { get; set; } = [];

    }
}
