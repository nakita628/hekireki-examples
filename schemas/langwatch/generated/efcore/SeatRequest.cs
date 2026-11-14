#nullable enable

using System;

namespace LangWatch.Models;

public partial class SeatRequest
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? UserId { get; set; }

    public string? Name { get; set; }

    public string Status { get; set; } = null!;

    public string Source { get; set; } = null!;

    public bool IdpRefused { get; set; } = false;

    public string? IdpKind { get; set; }

    public DateTime? SalesNotifiedAt { get; set; }

    public DateTime OpenedAt { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public string? ResolvedByUserId { get; set; }

    public DateTime? SeatPendingSince { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
