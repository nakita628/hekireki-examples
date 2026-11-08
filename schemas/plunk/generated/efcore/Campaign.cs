#nullable enable

using System;
using System.Collections.Generic;

namespace Plunk.Models;

public partial class Campaign
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public CampaignStatus Status { get; set; } = CampaignStatus.Draft;

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string From { get; set; } = null!;

    public string? FromName { get; set; }

    public string? ReplyTo { get; set; }

    public TemplateType Type { get; set; } = TemplateType.Marketing;

    public CampaignAudienceType AudienceType { get; set; } = CampaignAudienceType.All;

    public string? AudienceCondition { get; set; }

    public virtual Segment? Segment { get; set; }

    public string? SegmentId { get; set; }

    public DateTime? ScheduledFor { get; set; }

    public int TotalRecipients { get; set; } = 0;

    public int SentCount { get; set; } = 0;

    public int DeliveredCount { get; set; } = 0;

    public int OpenedCount { get; set; } = 0;

    public int ClickedCount { get; set; } = 0;

    public int BouncedCount { get; set; } = 0;

    public int ComplainedCount { get; set; } = 0;

    public int UnsubscribedCount { get; set; } = 0;

    public bool Archived { get; set; } = false;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual ICollection<Email> Emails { get; set; } = new List<Email>();

    public DateTime? SentAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
