#nullable enable

namespace SuperProductivity.Models;

public partial class CaptureGrant
{
    public string Id { get; set; } = null!;

    public int UserId { get; set; }

    public string Kind { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public string SecretHash { get; set; } = null!;

    public long CreatedAt { get; set; }

    public long ExpiresAt { get; set; }

    public long? LastUsedAt { get; set; }

    public long? RevokedAt { get; set; }

    public string? ClientId { get; set; }

    public string? Resource { get; set; }

    public string? RefreshHash { get; set; }

    public string? PrevRefreshHash { get; set; }

    public long? RotatedAt { get; set; }

    public long? RefreshExpiresAt { get; set; }

    public virtual User User { get; set; } = null!;
}
