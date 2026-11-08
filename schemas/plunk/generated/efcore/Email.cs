#nullable enable

using System;
using System.Collections.Generic;

namespace Plunk.Models;

public partial class Email
{
    public string Id { get; set; } = null!;

    public virtual Contact Contact { get; set; } = null!;

    public string ContactId { get; set; } = null!;

    public string? ToName { get; set; }

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string From { get; set; } = null!;

    public string? FromName { get; set; }

    public string? ReplyTo { get; set; }

    public string? Headers { get; set; }

    public string? Attachments { get; set; }

    public string? MessageId { get; set; }

    public EmailSourceType SourceType { get; set; }

    public virtual Template? Template { get; set; }

    public string? TemplateId { get; set; }

    public virtual Campaign? Campaign { get; set; }

    public string? CampaignId { get; set; }

    public virtual WorkflowExecution? WorkflowExecution { get; set; }

    public string? WorkflowExecutionId { get; set; }

    public virtual WorkflowStepExecution? WorkflowStepExecution { get; set; }

    public string? WorkflowStepExecutionId { get; set; }

    public EmailStatus Status { get; set; } = EmailStatus.Pending;

    public DateTime? ScheduledFor { get; set; }

    public DateTime? SentAt { get; set; }

    public DateTime? DeliveredAt { get; set; }

    public DateTime? OpenedAt { get; set; }

    public DateTime? ClickedAt { get; set; }

    public DateTime? BouncedAt { get; set; }

    public DateTime? ComplainedAt { get; set; }

    public DateTime? UnsubscribedAt { get; set; }

    public int Opens { get; set; } = 0;

    public int Clicks { get; set; } = 0;

    public bool Simulated { get; set; } = false;

    public string? Error { get; set; }

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
