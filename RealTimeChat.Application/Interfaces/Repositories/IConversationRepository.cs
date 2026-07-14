using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Application.Interfaces.Repositories
{
  public interface IConversationRepository : IGenericRepository<Conversation>
  {
    Task<List<Conversation>> GetUserConversationsAsync(Guid userId);

    Task<Conversation?> GetConversationWithMembersAsync(Guid conversationId);

    Task<Conversation?> GetPrivateConversationAsync(Guid firstUserId, Guid secondUserId);

    Task<bool> ExistsPrivateConversationAsync(Guid firstUserId, Guid secondUserId);
  }
}