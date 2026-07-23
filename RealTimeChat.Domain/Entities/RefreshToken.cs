using RealTimeChat.Domain.Common;

namespace RealTimeChat.Domain.Entities
{
  public class RefreshToken : BaseEntity
  {
    public string Token { get; set; } = string.Empty;
    public string? ReplacedByToken { get; set; }

    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = default!;

    public DateTime ExpiresAt { get; set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

    public DateTime? RevokedAt { get; set; }
    public bool IsRevoked => RevokedAt != null;

    public bool IsActive => !IsExpired && !IsRevoked;
  }
}