#nullable enable

using System;

namespace LangWatch.Models;

public partial class IdentityProjectionCursor
{
    public string UserId { get; set; } = null!;

    public string LastEventId { get; set; } = null!;

    public DateTime AcceptedAt { get; set; }

    public DateTime OccurredAt { get; set; }

    public string ProjectionVersion { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
