#nullable enable

using System;

namespace Papermark.Models;

public partial class TaskAssignment
{
    public string Id { get; set; } = null!;

    public string TaskId { get; set; } = null!;

    public virtual Task Task { get; set; } = null!;

    public string? ViewerId { get; set; }

    public virtual Viewer? Viewer { get; set; }

    public string? GroupId { get; set; }

    public virtual ViewerGroup? Group { get; set; }

    public string? LinkId { get; set; }

    public virtual Link? Link { get; set; }

    public string? Email { get; set; }

    public DateTime? NotifiedAt { get; set; }

    public DateTime? LastReminderAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
