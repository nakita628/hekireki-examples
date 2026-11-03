#nullable enable

using System;

namespace Papermark.Models;

public partial class TaskActivity
{
    public string Id { get; set; } = null!;

    public string TaskId { get; set; } = null!;

    public virtual Task Task { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Comment { get; set; }

    public string? FromStatus { get; set; }

    public string? ToStatus { get; set; }

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public string? ViewerId { get; set; }

    public virtual Viewer? Viewer { get; set; }

    public string? ViewId { get; set; }

    public virtual View? View { get; set; }

    public DateTime CreatedAt { get; set; }
}
