#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Email { get; set; }

    public DateTime? EmailVerified { get; set; }

    public string? Password { get; set; }

    public DateTime? SessionsExpiredAt { get; set; }

    public string? Image { get; set; }

    public bool Admin { get; set; } = false;

    public bool V4BetaEnabled { get; set; } = false;

    public string? SupportAccess { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<OrganizationMembership> OrganizationMemberships { get; set; } = new List<OrganizationMembership>();

    public virtual ICollection<ProjectMembership> ProjectMemberships { get; set; } = new List<ProjectMembership>();

    public virtual ICollection<MembershipInvitation> Invitations { get; set; } = new List<MembershipInvitation>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public List<string>? FeatureFlags { get; set; } = new List<string>();

    public virtual ICollection<AnnotationQueueItem> AnnotatedLockedItem { get; set; } = new List<AnnotationQueueItem>();

    public virtual ICollection<AnnotationQueueItem> AnnotatedCompletedItem { get; set; } = new List<AnnotationQueueItem>();

    public virtual ICollection<DashboardWidget> DashboardWidgetsCreated { get; set; } = new List<DashboardWidget>();

    public virtual ICollection<DashboardWidget> DashboardWidgetsUpdated { get; set; } = new List<DashboardWidget>();

    public virtual ICollection<Dashboard> DashboardCreated { get; set; } = new List<Dashboard>();

    public virtual ICollection<Dashboard> DashboardUpdated { get; set; } = new List<Dashboard>();

    public virtual ICollection<TableViewPreset> TableViewPresetCreated { get; set; } = new List<TableViewPreset>();

    public virtual ICollection<TableViewPreset> TableViewPresetUpdated { get; set; } = new List<TableViewPreset>();

    public virtual ICollection<Monitor> MonitorsCreated { get; set; } = new List<Monitor>();

    public virtual ICollection<Monitor> MonitorsUpdated { get; set; } = new List<Monitor>();

    public virtual ICollection<TopicConfig> TopicConfigsCreated { get; set; } = new List<TopicConfig>();

    public virtual ICollection<TopicConfig> TopicConfigsUpdated { get; set; } = new List<TopicConfig>();

    public virtual ICollection<TracingReadinessSettings> TracingReadinessCreated { get; set; } = new List<TracingReadinessSettings>();

    public virtual ICollection<TracingReadinessSettings> TracingReadinessUpdated { get; set; } = new List<TracingReadinessSettings>();

    public virtual ICollection<AnnotationQueueAssignment> AnnotationQueueAssignment { get; set; } = new List<AnnotationQueueAssignment>();

    public virtual ICollection<Survey> Surveys { get; set; } = new List<Survey>();

    public virtual ICollection<CommentReaction> CommentReactions { get; set; } = new List<CommentReaction>();

    public virtual ICollection<NotificationPreference> NotificationPreferences { get; set; } = new List<NotificationPreference>();

    public virtual ICollection<DefaultView> DefaultViews { get; set; } = new List<DefaultView>();

    public virtual ICollection<InAppAgentConversation> InAppAgentConversations { get; set; } = new List<InAppAgentConversation>();

    public virtual ICollection<InAppAgentRun> InAppAgentRuns { get; set; } = new List<InAppAgentRun>();

    public virtual ICollection<ApiKey> ApiKeysCreated { get; set; } = new List<ApiKey>();

    public virtual ICollection<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();

    public virtual ICollection<RoleAssignment> RoleAssignments { get; set; } = new List<RoleAssignment>();

    public virtual ICollection<Evaluator> EvaluatorsCreated { get; set; } = new List<Evaluator>();

    public virtual ICollection<EvaluatorVersion> EvaluatorVersionsCreated { get; set; } = new List<EvaluatorVersion>();

    public virtual ICollection<EvaluationRule> EvaluationRulesCreated { get; set; } = new List<EvaluationRule>();
}
