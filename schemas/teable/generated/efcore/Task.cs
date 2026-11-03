#nullable enable

using System;
using System.Collections.Generic;

namespace Teable.Models;

public partial class Task
{
    public string Id { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? Snapshot { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string? LastModifiedBy { get; set; }

    public virtual ICollection<TaskRun> Runs { get; set; } = new List<TaskRun>();
}
