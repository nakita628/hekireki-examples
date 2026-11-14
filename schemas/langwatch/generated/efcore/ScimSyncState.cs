#nullable enable

using System;

namespace LangWatch.Models;

public partial class ScimSyncState
{
    public string Id { get; set; } = null!;

    public string ConnectionId { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string State { get; set; } = null!;

    public DateTime? LastPushedAt { get; set; }

    public string? LastFailure { get; set; }

    public string DeadLetters { get; set; } = "[]";

    public string? RevokedCause { get; set; }

    public DateTime OccurredAt { get; set; }

    public string LastEventId { get; set; } = null!;

    public DateTime AcceptedAt { get; set; }

    public string ProjectionVersion { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
