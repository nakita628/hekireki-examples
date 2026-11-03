#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraScorerDefinitionVersions
{
    public string Id { get; set; } = null!;

    public string ScorerDefinitionId { get; set; } = null!;

    public int VersionNumber { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Type { get; set; } = null!;

    public string? Model { get; set; }

    public string? Instructions { get; set; }

    public string? ScoreRange { get; set; }

    public string? PresetConfig { get; set; }

    public string? DefaultSampling { get; set; }

    public string? ChangedFields { get; set; }

    public string? ChangeMessage { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;
}
