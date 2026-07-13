using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Infrastructure.Data.Configurations
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable("Attachments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FileName)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.Property(x => x.FileUrl)
                   .HasMaxLength(1000)
                   .IsRequired();

            builder.Property(x => x.ContentType)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.HasIndex(x => x.MessageId);

            builder.HasOne(x => x.Message)
               .WithMany()
               .HasForeignKey(x => x.MessageId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
