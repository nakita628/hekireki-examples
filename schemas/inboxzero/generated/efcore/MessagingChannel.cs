#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class MessagingChannel
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public MessagingProvider Provider { get; set; }

    public bool IsConnected { get; set; } = true;

    public string TeamId { get; set; } = null!;

    public string? TeamName { get; set; }

    public string? ProviderUserId { get; set; }

    public string? BotUserId { get; set; }

    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;

    public virtual ICollection<MessagingRoute> Routes { get; set; } = new List<MessagingRoute>();

    public virtual ICollection<AutomationJob> AutomationJobs { get; set; } = new List<AutomationJob>();

    public virtual ICollection<Action> Actions { get; set; } = new List<Action>();

    public virtual ICollection<ExecutedAction> ExecutedActions { get; set; } = new List<ExecutedAction>();
}
