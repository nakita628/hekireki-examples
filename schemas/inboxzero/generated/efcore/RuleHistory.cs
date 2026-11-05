#nullable enable

using System;

namespace InboxZero.Models;

public partial class RuleHistory
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string RuleId { get; set; } = null!;

    public virtual Rule Rule { get; set; } = null!;

    public int Version { get; set; }

    public string TriggerType { get; set; } = null!;

    public string? PromptText { get; set; }

    public string Name { get; set; } = null!;

    public string? Instructions { get; set; }

    public bool Enabled { get; set; }

    public bool Automate { get; set; }

    public bool RunOnThreads { get; set; }

    public string ConditionalOperator { get; set; } = null!;

    public string? From { get; set; }

    public string? To { get; set; }

    public string? Subject { get; set; }

    public string? Body { get; set; }

    public string? CategoryFilterType { get; set; }

    public string? SystemType { get; set; }

    public string Actions { get; set; } = null!;

    public string? CategoryFilters { get; set; }
}
