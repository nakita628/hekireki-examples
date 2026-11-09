#nullable enable

using System;

namespace Jitsu.Models;

public partial class TaskLog
{
    public Guid Id { get; set; }

    public string Level { get; set; } = null!;

    public string Logger { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string SyncId { get; set; } = null!;

    public string TaskId { get; set; } = null!;

    public DateTime Timestamp { get; set; }
}
