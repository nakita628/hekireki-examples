#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class RetentionPolicy
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public RetentionEntity Entity { get; set; }

    public bool Enabled { get; set; } = false;

    public DateTime? EnabledAt { get; set; }

    public int WarnDays { get; set; } = 60;

    public int PeriodDays { get; set; }

    public List<RetentionSurveyCondition>? Conditions { get; set; } = new List<RetentionSurveyCondition>();

    public DateTime? DeferredAt { get; set; }

    public virtual User? UpdatedBy { get; set; }

    public string? UpdatedById { get; set; }
}
