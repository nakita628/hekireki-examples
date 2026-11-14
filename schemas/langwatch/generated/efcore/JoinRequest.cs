#nullable enable

using System;

namespace LangWatch.Models;

public partial class JoinRequest
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string Domain { get; set; } = null!;

    public string State { get; set; } = null!;

    public string MatchedVia { get; set; } = null!;

    public string Origin { get; set; } = "web";

    public DateTime? ExpiresAt { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public string? ResolvedByType { get; set; }

    public string? ResolvedById { get; set; }

    public string? WithdrawalCause { get; set; }

    public string? ConnectionId { get; set; }

    public DateTime OccurredAt { get; set; }

    public string LastEventId { get; set; } = null!;

    public DateTime AcceptedAt { get; set; }

    public string ProjectionVersion { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
