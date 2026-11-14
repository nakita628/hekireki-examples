#nullable enable

using System;

namespace LangWatch.Models;

public partial class DatasetVersionRun
{
    public string Id { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string DatasetId { get; set; } = null!;

    public string VersionId { get; set; } = null!;

    public string RunId { get; set; } = null!;

    public string? ExperimentId { get; set; }

    public DateTime CreatedAt { get; set; }
}
