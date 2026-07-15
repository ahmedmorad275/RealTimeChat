using Microsoft.EntityFrameworkCore;
using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Application.Interfaces
{
  public interface IApplicationDbContext : IDisposable
  {
    DbSet<Message> Messages { get; }
    DbSet<ApplicationUser> Users { get; }
    DbSet<Attachment> Attachments { get; }
    DbSet<MessageRead> MessageReads { get; }
    DbSet<GroupDetails> GroupDetails { get; }
    DbSet<Conversation> Conversations { get; }
    DbSet<MessageReaction> MessageReactions { get; }
    DbSet<ConversationMember> ConversationMembers { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
  }
}