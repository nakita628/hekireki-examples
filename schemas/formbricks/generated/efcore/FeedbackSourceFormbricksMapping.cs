#nullable enable

using System;

namespace Formbricks.Models;

public partial class FeedbackSourceFormbricksMapping
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string FeedbackSourceId { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public virtual FeedbackSource FeedbackSource { get; set; } = null!;

    public string SurveyId { get; set; } = null!;

    public virtual Survey Survey { get; set; } = null!;

    public string ElementId { get; set; } = null!;

    public HubFieldType HubFieldType { get; set; }

    public string? CustomFieldLabel { get; set; }
}
