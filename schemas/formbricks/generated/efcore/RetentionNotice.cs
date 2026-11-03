#nullable enable

using System;

namespace Formbricks.Models;

public partial class RetentionNotice
{
    public string Id { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public RetentionEntity Entity { get; set; }

    public virtual Survey? Survey { get; set; }

    public string? SurveyId { get; set; }

    public virtual User? User { get; set; }

    public string? UserId { get; set; }

    public DateTime SentAt { get; set; }

    public DateTime? DeliveredAt { get; set; }

    public bool EmailSent { get; set; } = false;

    public string? ClaimToken { get; set; }

    public DateTime? ClockAt { get; set; }
}
