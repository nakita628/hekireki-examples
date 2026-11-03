#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class Webhook
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Url { get; set; } = null!;

    public WebhookSource Source { get; set; } = WebhookSource.User;

    public virtual Workspace Workspace { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public List<PipelineTriggers>? Triggers { get; set; }

    public List<string>? SurveyIds { get; set; }

    public string? Secret { get; set; }
}
