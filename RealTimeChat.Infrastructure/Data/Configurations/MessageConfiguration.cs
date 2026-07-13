using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Infrastructure.Data.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Content)
                .HasMaxLength(4000);

            builder.Property(x => x.Type)
                .HasConversion<string>();

            builder.Property(x => x.Type)
                .IsRequired();

            builder.HasIndex(x => x.SenderId);

            builder.HasIndex(x => x.ConversationId);

            builder.HasIndex(x => x.CreatedAt);

            builder.HasOne(x => x.Conversation)
                .WithMany()
                .HasForeignKey(x => x.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Sender)
                .WithMany()
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ReplyToMessage)
               .WithMany()
               .HasForeignKey(x => x.ReplyToMessageId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
