using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Infrastructure.Data.Configurations
{
    public class MessageReactionConfiguration : IEntityTypeConfiguration<MessageReaction>
    {
        public void Configure(EntityTypeBuilder<MessageReaction> builder)
        {
            builder.ToTable("MessageReactions");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new
            {
                x.MessageId,
                x.UserId
            }).IsUnique();

            builder.Property(x => x.Reaction)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasOne(x => x.Message)
                .WithMany()
                .HasForeignKey(x => x.MessageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
