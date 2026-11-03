#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraDatasetItems
{
    public string Id { get; set; } = null!;

    public string DatasetId { get; set; } = null!;

    public int DatasetVersion { get; set; }

    public int? ValidTo { get; set; }

    public bool IsDeleted { get; set; }

    public string Input { get; set; } = null!;

    public string? GroundTruth { get; set; }

    public string? RequestContext { get; set; }

    public string? Metadata { get; set; }

    public string? Source { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAtZ { get; set; } = DateTime.UtcNow;

    public string? ExpectedTrajectory { get; set; }

    public string? OrganizationId { get; set; }

    public string? ProjectId { get; set; }

    public string? ToolMocks { get; set; }

    public string? UnmockedToolPolicy { get; set; }

    public string? ScorerIds { get; set; }

    public string? ExternalId { get; set; }
}
