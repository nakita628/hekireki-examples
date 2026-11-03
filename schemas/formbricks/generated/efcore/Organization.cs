#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class Organization
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();

    public virtual ICollection<Workspace> Workspaces { get; set; } = new List<Workspace>();

    public virtual OrganizationBilling? Billing { get; set; }

    public string Whitelabel { get; set; } = "{}";

    public virtual ICollection<Invite> Invites { get; set; } = new List<Invite>();

    public bool IsAISmartToolsEnabled { get; set; } = false;

    public string? DisplayTimeZone { get; set; }

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();

    public virtual ICollection<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();

    public virtual ICollection<FeedbackDirectory> FeedbackDirectories { get; set; } = new List<FeedbackDirectory>();

    public virtual ICollection<RetentionPolicy> RetentionPolicies { get; set; } = new List<RetentionPolicy>();

    public virtual ICollection<RetentionExemption> RetentionExemptions { get; set; } = new List<RetentionExemption>();

    public virtual ICollection<RetentionRun> RetentionRuns { get; set; } = new List<RetentionRun>();

    public virtual ICollection<RetentionNotice> RetentionNotices { get; set; } = new List<RetentionNotice>();
}
