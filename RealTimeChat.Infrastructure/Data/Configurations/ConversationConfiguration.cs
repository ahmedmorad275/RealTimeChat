using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Infrastructure.Data.Configurations
{
    public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.ToTable("Conversations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type).HasConversion<string>();

            builder.Property(x => x.Type).IsRequired();
        }
    }
}
