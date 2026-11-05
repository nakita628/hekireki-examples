#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class Rule
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Action> Actions { get; set; } = new List<Action>();

    public virtual ICollection<AttachmentSource> AttachmentSources { get; set; } = new List<AttachmentSource>();

    public bool Enabled { get; set; } = true;

    public bool Automate { get; set; } = true;

    public bool RunOnThreads { get; set; } = false;

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;

    public string? OrganizationRuleId { get; set; }

    public virtual OrganizationRule? OrganizationRule { get; set; }

    public bool? OrganizationRuleMemberEnabled { get; set; }

    public virtual ICollection<ExecutedRule> ExecutedRules { get; set; } = new List<ExecutedRule>();

    public virtual ICollection<ClassificationFeedback> ClassificationFeedback { get; set; } = new List<ClassificationFeedback>();

    public LogicalOperator ConditionalOperator { get; set; } = LogicalOperator.And;

    public string? Instructions { get; set; }

    public string? GroupId { get; set; }

    public virtual Group? Group { get; set; }

    public string? From { get; set; }

    public string? To { get; set; }

    public string? Subject { get; set; }

    public string? Body { get; set; }

    public CategoryFilterType? CategoryFilterType { get; set; }

    public virtual ICollection<Category> CategoryFilters { get; set; } = new List<Category>();

    public SystemType? SystemType { get; set; }

    public string? PromptText { get; set; }

    public virtual ICollection<RuleHistory> History { get; set; } = new List<RuleHistory>();
}
