#nullable enable

using System;

namespace Langfuse.Models;

public partial class DatasetItem
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public DatasetStatus? Status { get; set; } = DatasetStatus.Active;

    public string? Input { get; set; }

    public string? ExpectedOutput { get; set; }

    public string? Metadata { get; set; }

    public string? SourceTraceId { get; set; }

    public string? SourceObservationId { get; set; }

    public string DatasetId { get; set; } = null!;

    public virtual Dataset Dataset { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime? ValidTo { get; set; }

    public bool IsDeleted { get; set; } = false;
}
