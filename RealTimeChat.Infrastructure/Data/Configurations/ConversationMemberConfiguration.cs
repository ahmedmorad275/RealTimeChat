using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Infrastructure.Data.Configurations
{
    public class ConversationMemberConfiguration : IEntityTypeConfiguration<ConversationMember>
    {
        public void Configure(EntityTypeBuilder<ConversationMember> builder)
        {
            builder.ToTable("ConversationMembers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.JoinedAt).IsRequired();

            builder.HasIndex(x => new
            {
                x.UserId,
                x.ConversationId
            }).IsUnique();

            builder.HasOne(x => x.Conversation)
                   .WithMany(c => c.Members)
                   .HasForeignKey(x => x.ConversationId);


            builder.HasOne(x => x.User)
                   .WithMany()
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
