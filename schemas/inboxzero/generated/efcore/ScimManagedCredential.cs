#nullable enable

using System;

namespace InboxZero.Models;

public partial class ScimManagedCredential
{
    public string Id { get; set; } = null!;

    public string ConnectionRecordId { get; set; } = null!;

    public virtual ScimManagedConnection Scimmanagedconnection { get; set; } = null!;

    public string CredentialId { get; set; } = null!;

    public string TokenDigest { get; set; } = null!;

    public string HashVersion { get; set; } = null!;

    public string ActiveSlotKey { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string SerializedScopes { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastUsedAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public string? RevokedBy { get; set; }

    public DateTime? DecommissionedAt { get; set; }
}
