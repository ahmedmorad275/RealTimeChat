namespace RealTimeChat.Domain.Common
{
    public class AuditableEntity : BaseEntity
    {
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
