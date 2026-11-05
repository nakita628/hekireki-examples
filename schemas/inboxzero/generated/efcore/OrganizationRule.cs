#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class OrganizationRule
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public bool Enabled { get; set; } = true;

    public string Name { get; set; } = null!;

    public string? Instructions { get; set; }

    public bool RunOnThreads { get; set; } = false;

    public LogicalOperator ConditionalOperator { get; set; } = LogicalOperator.And;

    public string? From { get; set; }

    public string? To { get; set; }

    public string? Subject { get; set; }

    public string? Body { get; set; }

    public virtual ICollection<OrganizationRuleAction> Actions { get; set; } = new List<OrganizationRuleAction>();

    public virtual ICollection<Rule> MemberRules { get; set; } = new List<Rule>();
}
