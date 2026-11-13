#nullable enable

namespace SuperProductivity.Models;

public partial class CaptureAuth
{
    public string Id { get; set; } = null!;

    public int? UserId { get; set; }

    public string ClientId { get; set; } = null!;

    public string ClientName { get; set; } = null!;

    public string RedirectUri { get; set; } = null!;

    public string CodeChallenge { get; set; } = null!;

    public string Resource { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Scope { get; set; } = null!;

    public string? CodeHash { get; set; }

    public int? TokenVersion { get; set; }

    public long ExpiresAt { get; set; }

    public virtual User? User { get; set; }
}
