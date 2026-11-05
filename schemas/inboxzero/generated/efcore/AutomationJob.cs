#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class AutomationJob
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public bool Enabled { get; set; } = true;

    public string? Prompt { get; set; }

    public string CronExpression { get; set; } = null!;

    public DateTime NextRunAt { get; set; }

    public string MessagingChannelId { get; set; } = null!;

    public virtual MessagingChannel MessagingChannel { get; set; } = null!;

    public string EmailAccountId { get; set; } = null!;

    public virtual ICollection<AutomationJobRun> Runs { get; set; } = new List<AutomationJobRun>();
}
