#nullable enable

using System;

namespace LangWatch.Models;

public partial class ExperimentVersion
{
    public string Id { get; set; } = null!;

    public string ExperimentId { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public int Version { get; set; }

    public int CounterVersion { get; set; }

    public bool AutoSaved { get; set; } = false;

    public string? CommitMessage { get; set; }

    public string? AuthorId { get; set; }

    public string AuthorLabel { get; set; } = null!;

    public string? RunId { get; set; }

    public string State { get; set; } = null!;

    public string SchemaVersion { get; set; } = "1";

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
