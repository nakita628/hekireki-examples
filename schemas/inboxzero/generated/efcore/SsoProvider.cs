#nullable enable

namespace InboxZero.Models;

public partial class SsoProvider
{
    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public string Id { get; set; } = null!;

    public string Issuer { get; set; } = null!;

    public string? OidcConfig { get; set; }

    public string? SamlConfig { get; set; }

    public string? EmailAccountId { get; set; }

    public virtual EmailAccount? EmailAccount { get; set; }

    public string ProviderId { get; set; } = null!;

    public string? OrganizationId { get; set; }

    public virtual Organization? Organization { get; set; }

    public string Domain { get; set; } = null!;
}
