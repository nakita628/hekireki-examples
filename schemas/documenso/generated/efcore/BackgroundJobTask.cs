#nullable enable

using System;

namespace Documenso.Models;

public partial class BackgroundJobTask
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public BackgroundJobTaskStatus Status { get; set; } = BackgroundJobTaskStatus.Pending;

    public string? Result { get; set; }

    public int Retried { get; set; } = 0;

    public int MaxRetries { get; set; } = 3;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string JobId { get; set; } = null!;

    public virtual BackgroundJob BackgroundJob { get; set; } = null!;
}
