#nullable enable

using System;

namespace Formbricks.Models;

public partial class FeedbackSourceFieldMapping
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string FeedbackSourceId { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public virtual FeedbackSource FeedbackSource { get; set; } = null!;

    public string SourceFieldId { get; set; } = null!;

    public string TargetFieldId { get; set; } = null!;

    public string? StaticValue { get; set; }
}
