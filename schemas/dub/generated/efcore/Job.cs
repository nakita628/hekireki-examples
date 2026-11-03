#nullable enable

using System;

namespace Dub.Models;

public partial class Job
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Payload { get; set; } = null!;

    public string? Options { get; set; }

    public DateTime ScheduledAt { get; set; }

    public int Attempts { get; set; } = 0;

    public string? LastError { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
