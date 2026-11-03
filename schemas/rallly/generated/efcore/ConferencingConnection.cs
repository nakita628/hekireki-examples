#nullable enable

using System;

namespace Rallly.Models;

public partial class ConferencingConnection
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string IntegrationId { get; set; } = null!;

    public string ProviderAccountId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? DisplayName { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Credential Credential { get; set; } = null!;

    public string CredentialId { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
