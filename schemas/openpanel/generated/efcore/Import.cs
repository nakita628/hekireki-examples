#nullable enable

using System;

namespace OpenPanel.Models;

public partial class Import
{
    public Guid Id { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string? JobId { get; set; }

    public ImportStatus Status { get; set; }

    public string? StatusMessage { get; set; }

    public string? ErrorMessage { get; set; }

    public string Config { get; set; } = null!;

    public int TotalEvents { get; set; } = 0;

    public int ProcessedEvents { get; set; } = 0;

    public string? CurrentStep { get; set; }

    public string? CurrentBatch { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
