using Microsoft.AspNetCore.Identity;

namespace RealTimeChat.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = default!;

        public string? ProfilePictureUrl { get; set; }

        public DateTime? LastSeenAt { get; set; }

        public bool IsOnline { get; set; }
    }
}
