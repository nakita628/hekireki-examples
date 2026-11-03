#nullable enable

using System;
using System.Collections.Generic;

namespace Documenso.Models;

public partial class BackgroundJob
{
    public string Id { get; set; } = null!;

    public BackgroundJobStatus Status { get; set; } = BackgroundJobStatus.Pending;

    public string? Payload { get; set; }

    public int Retried { get; set; } = 0;

    public int MaxRetries { get; set; } = 3;

    public string JobId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Version { get; set; } = null!;

    public DateTime SubmittedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime? LastRetriedAt { get; set; }

    public virtual ICollection<BackgroundJobTask> Tasks { get; set; } = new List<BackgroundJobTask>();
}
