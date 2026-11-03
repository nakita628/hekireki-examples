#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraDatasets
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Metadata { get; set; }

    public string? InputSchema { get; set; }

    public string? GroundTruthSchema { get; set; }

    public string? RequestContextSchema { get; set; }

    public string? Tags { get; set; }

    public string? TargetType { get; set; }

    public string? TargetIds { get; set; }

    public string? ScorerIds { get; set; }

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAtZ { get; set; } = DateTime.UtcNow;

    public string? OrganizationId { get; set; }

    public string? ProjectId { get; set; }

    public string? CandidateKey { get; set; }

    public string? CandidateId { get; set; }
}
