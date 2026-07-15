using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Application.Interfaces.Repositories
{
  public interface IMessageRepository
  {
    Task<List<Message>> GetConversationMessagesAsync(Guid conversationId, int pageNumber, int pageSize);

    Task<Message?> GetMessageWithReplyAsync(Guid messageId);

    Task<Message?> GetLastMessageAsync(Guid conversationId);

    Task<int> GetUnreadMessagesCountAsync(Guid conversationId, Guid userId);
  }
}