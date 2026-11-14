#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class Trigger
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public TriggerAction Action { get; set; }

    public TriggerKind TriggerKind { get; set; } = TriggerKind.Automation;

    public string ActionParams { get; set; } = null!;

    public string Filters { get; set; } = null!;

    public string? FilterQuery { get; set; }

    public double LastRunAt { get; set; } = 0.0;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool Active { get; set; } = true;

    public string? PausedReason { get; set; }

    public DateTime? PausedAt { get; set; }

    public string? Message { get; set; }

    public bool Deleted { get; set; } = false;

    public AlertType? AlertType { get; set; }

    public string? SlackTemplateType { get; set; }

    public string? SlackTemplate { get; set; }

    public string? EmailSubjectTemplate { get; set; }

    public string? EmailBodyTemplate { get; set; }

    public string NotificationCadence { get; set; } = "immediate";

    public int TraceDebounceMs { get; set; } = 30000;

    public virtual ICollection<TriggerSent> TriggerSent { get; set; } = new List<TriggerSent>();

    public virtual ICollection<WebhookEndpointDelivery> WebhookDeliveries { get; set; } = new List<WebhookEndpointDelivery>();

    public virtual TriggerLatestEvaluation? LatestEvaluation { get; set; }

    public string? CustomGraphId { get; set; }

    public virtual CustomGraph? CustomGraph { get; set; }
}
