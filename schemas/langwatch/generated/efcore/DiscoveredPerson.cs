#nullable enable

using System;

namespace LangWatch.Models;

public partial class DiscoveredPerson
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string RawActorId { get; set; } = null!;

    public string DisplayText { get; set; } = null!;

    public string Kind { get; set; } = null!;

    public string? Department { get; set; }

    public DateTime FirstSeenAt { get; set; }

    public DateTime LastSeenAt { get; set; }

    public DateTime? ErasedAt { get; set; }

    public DateTime? MoneyRowsPendingAt { get; set; }

    public string? MoneyRebuildSince { get; set; }

    public DateTime? SuspendedAt { get; set; }

    public string? SuspendedReason { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
