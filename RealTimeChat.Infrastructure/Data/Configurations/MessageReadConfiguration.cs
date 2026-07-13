using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Infrastructure.Data.Configurations
{
    public class MessageReadConfiguration : IEntityTypeConfiguration<MessageRead>
    {
        public void Configure(EntityTypeBuilder<MessageRead> builder)
        {
            builder.ToTable("MessageReads");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ReadAt)
                   .IsRequired();

            builder.HasIndex(x =>
            new
            {
                x.UserId,
                x.MessageId
            }).IsUnique();

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
