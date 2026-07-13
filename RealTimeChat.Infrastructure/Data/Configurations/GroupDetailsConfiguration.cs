using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Infrastructure.Data.Configurations
{
    public class GroupDetailsConfiguration : IEntityTypeConfiguration<GroupDetails>
    {
        public void Configure(EntityTypeBuilder<GroupDetails> builder)
        {
            builder.ToTable("GroupDetails");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.ImageUrl)
               .HasMaxLength(1000);


            builder.HasOne(x => x.CreatedByUser)
                .WithMany()
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Conversation)
                .WithOne()
                .HasForeignKey<GroupDetails>(x => x.ConversationId);
        }
    }
}
