using Microsoft.EntityFrameworkCore;
using RealTimeChat.Application.Interfaces.Repositories;
using RealTimeChat.Domain.Entities;
using RealTimeChat.Infrastructure.Data;

namespace RealTimeChat.Infrastructure.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Message>> GetConversationMessagesAsync(Guid conversationId, int pageNumber, int pageSize)
        {
            return await _set.Where(x => x.ConversationId == conversationId)
              .OrderByDescending(x => x.CreatedAt)
              .Skip((pageNumber - 1) * pageSize)
              .Take(pageSize)
              .ToListAsync();
        }

        public async Task<Message?> GetLastMessageAsync(Guid conversationId)
        {
            return await _set.Where(x => x.ConversationId == conversationId)
              .OrderByDescending(x => x.CreatedAt)
              .FirstOrDefaultAsync();
        }

        public async Task<Message?> GetMessageWithReplyAsync(Guid messageId)
        {
            return await _set.Include(x => x.ReplyToMessage)
              .FirstOrDefaultAsync(x => x.Id == messageId);
        }

        public async Task<int> GetUnreadMessagesCountAsync(Guid conversationId, Guid userId)
        {
            var lastReadMessageId = await _context.ConversationMembers
              .Where(x => x.ConversationId == conversationId && x.UserId == userId)
              .Select(x => x.LastReadMessageId)
              .FirstOrDefaultAsync();

            if (lastReadMessageId == null)
            {
                return await _set.CountAsync(x => x.ConversationId == conversationId);
            }

            var lastReadMessage = await _set.FirstAsync(x => x.Id == lastReadMessageId);

            return await _set.CountAsync(x => x.ConversationId == conversationId && x.CreatedAt > lastReadMessage.CreatedAt);
        }
    }
}