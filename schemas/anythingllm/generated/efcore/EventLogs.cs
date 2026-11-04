#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class EventLogs
{
    public int Id { get; set; }

    public string Event { get; set; } = null!;

    public string? Metadata { get; set; }

    public int? UserId { get; set; }

    public DateTime OccurredAt { get; set; }
}
