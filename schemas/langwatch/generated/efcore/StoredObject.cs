#nullable enable

using System;

namespace LangWatch.Models;

public partial class StoredObject
{
    public string TenantId { get; set; } = null!;

    public string Id { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Purpose { get; set; } = null!;

    public string OwnerKind { get; set; } = null!;

    public string OwnerId { get; set; } = null!;

    public string Filename { get; set; } = null!;

    public string Sha256 { get; set; } = null!;

    public long SizeBytes { get; set; }

    public string MediaType { get; set; } = null!;

    public bool MediaTypeVerified { get; set; } = false;

    public string? StorageProvider { get; set; }

    public string? StorageDestinationId { get; set; }

    public string? StorageProviderRelativeId { get; set; }

    public int Generation { get; set; } = 0;

    public string Audiences { get; set; } = null!;

    public DateTime? ExpiresAt { get; set; }

    public DateTime? AvailableAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string Source { get; set; } = null!;

    public string? LegacyFingerprint { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
