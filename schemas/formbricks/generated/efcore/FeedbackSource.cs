#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class FeedbackSource
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public FeedbackSourceType Type { get; set; }

    public FeedbackSourceStatus Status { get; set; } = FeedbackSourceStatus.Active;

    public FeedbackSourceImportMode ImportMode { get; set; } = FeedbackSourceImportMode.CompletedOnly;

    public FeedbackSourceElementScope ElementScope { get; set; } = FeedbackSourceElementScope.Specific;

    public string WorkspaceId { get; set; } = null!;

    public virtual Workspace Workspace { get; set; } = null!;

    public string FeedbackDirectoryId { get; set; } = null!;

    public virtual FeedbackDirectory FeedbackDirectory { get; set; } = null!;

    public virtual ICollection<FeedbackSourceFormbricksMapping> FormbricksMappings { get; set; } = new List<FeedbackSourceFormbricksMapping>();

    public virtual ICollection<FeedbackSourceFieldMapping> FieldMappings { get; set; } = new List<FeedbackSourceFieldMapping>();

    public DateTime? LastSyncAt { get; set; }

    public string? CreatedBy { get; set; }

    public virtual User? Creator { get; set; }

    public virtual FeedbackDirectoryWorkspace FeedbackDirectoryWorkspace { get; set; } = null!;
}
