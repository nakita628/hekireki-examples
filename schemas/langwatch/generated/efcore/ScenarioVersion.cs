#nullable enable

using System;

namespace LangWatch.Models;

public partial class ScenarioVersion
{
    public string Id { get; set; } = null!;

    public string ScenarioId { get; set; } = null!;

    public virtual Scenario Scenario { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public int Version { get; set; }

    public string? AuthorId { get; set; }

    public string AuthorLabel { get; set; } = null!;

    public string? ChangeDescription { get; set; }

    public string Snapshot { get; set; } = null!;

    public int SchemaVersion { get; set; } = 1;

    public DateTime CreatedAt { get; set; }
}
