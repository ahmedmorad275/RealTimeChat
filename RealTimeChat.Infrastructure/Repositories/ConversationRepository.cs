using Microsoft.EntityFrameworkCore;
using RealTimeChat.Application.Interfaces.Repositories;
using RealTimeChat.Domain.Entities;
using RealTimeChat.Domain.Enums;
using RealTimeChat.Infrastructure.Data;

namespace RealTimeChat.Infrastructure.Repositories
{
  public class ConversationRepository : GenericRepository<Conversation>, IConversationRepository
  {
    public ConversationRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsPrivateConversationAsync(Guid firstUserId, Guid secondUserId)
    {
      return await _set.AnyAsync(
        x => x.Type == ConversationType.Private &&
        x.Members.Any(x => x.UserId == firstUserId) &&
        x.Members.Any(x => x.UserId == secondUserId));
    }

    public async Task<Conversation?> GetConversationWithMembersAsync(Guid conversationId)
    {
      return await _set.Include(x => x.Members)
        .FirstOrDefaultAsync(x => x.Id == conversationId);
    }

    public async Task<Conversation?> GetPrivateConversationAsync(Guid firstUserId, Guid secondUserId)
    {
      return await _set.Include(x => x.Members).
        FirstOrDefaultAsync(x =>
        x.Type == ConversationType.Private &&
        x.Members.Any(x => x.UserId == firstUserId) &&
        x.Members.Any(x => x.UserId == secondUserId));
    }

    public async Task<List<Conversation>> GetUserConversationsAsync(Guid userId)
    {
      return await _set.Include(x => x.Members)
        .Where(x => x.Members.Any(m => m.UserId == userId))
        .ToListAsync();
    }
  }
}