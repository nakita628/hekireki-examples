#nullable enable

using System;
using System.Collections.Generic;

namespace Teable.Models;

public partial class TaskRun
{
    public string Id { get; set; } = null!;

    public string TaskId { get; set; } = null!;

    public string BaseId { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Snapshot { get; set; } = null!;

    public List<string>? DependsOnRunIds { get; set; } = new List<string>();

    public int? Spent { get; set; }

    public string? Log { get; set; }

    public string? ErrorMsg { get; set; }

    public DateTime? StartedTime { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public virtual Task Task { get; set; } = null!;
}
