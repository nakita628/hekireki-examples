#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class Dataset
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Metadata { get; set; }

    public string? RemoteExperimentUrl { get; set; }

    public string? RemoteExperimentPayload { get; set; }

    public bool RemoteExperimentEnabled { get; set; } = true;

    public string? RemoteExperimentSecretKey { get; set; }

    public string? RemoteExperimentDisplaySecretKey { get; set; }

    public string? RemoteExperimentRequestHeaders { get; set; }

    public string? RemoteExperimentDisplayHeaders { get; set; }

    public string? InputSchema { get; set; }

    public string? ExpectedOutputSchema { get; set; }

    public virtual Project Project { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<DatasetItem> DatasetItems { get; set; } = new List<DatasetItem>();

    public virtual ICollection<DatasetRuns> DatasetRuns { get; set; } = new List<DatasetRuns>();
}
