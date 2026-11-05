#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class ExecutedRule
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ThreadId { get; set; } = null!;

    public string MessageId { get; set; } = null!;

    public ExecutedRuleStatus Status { get; set; }

    public bool Automated { get; set; }

    public string? Reason { get; set; }

    public string? MatchMetadata { get; set; }

    public string? RuleId { get; set; }

    public virtual Rule? Rule { get; set; }

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;

    public virtual ICollection<ExecutedAction> ActionItems { get; set; } = new List<ExecutedAction>();

    public virtual ICollection<ScheduledAction> ScheduledActions { get; set; } = new List<ScheduledAction>();
}
