namespace RealTimeChat.Domain.Common
{
    public class SoftDeleteEntity : AuditableEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
