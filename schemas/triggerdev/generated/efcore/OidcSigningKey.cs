#nullable enable

using System;

namespace Trigger.Models;

public partial class OidcSigningKey
{
    public string Id { get; set; } = null!;

    public string Kid { get; set; } = null!;

    public OidcSigningKeyPurpose Purpose { get; set; } = OidcSigningKeyPurpose.ExternalOidc;

    public OidcSigningKeyStatus Status { get; set; }

    public string PublicJwk { get; set; } = null!;

    public DateTime ReadyAt { get; set; }

    public DateTime? PublicUntil { get; set; }

    public string GeneratedByUserId { get; set; } = null!;

    public DateTime? ActivatedAt { get; set; }

    public string? ActivatedByUserId { get; set; }

    public DateTime? RetiredAt { get; set; }

    public string? RetiredByUserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
