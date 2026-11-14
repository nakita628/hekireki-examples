#nullable enable

namespace LangWatch.Models;

public partial class SsoProvider
{
    public string Id { get; set; } = null!;

    public string Issuer { get; set; } = null!;

    public string? OidcConfig { get; set; }

    public string? SamlConfig { get; set; }

    public string? UserId { get; set; }

    public string ProviderId { get; set; } = null!;

    public string? OrganizationId { get; set; }

    public string Domain { get; set; } = null!;
}
