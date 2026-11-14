#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class Project
{
    public string Id { get; set; } = null!;

    public string OrgId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string Name { get; set; } = null!;

    public int? RetentionDays { get; set; }

    public bool HasTraces { get; set; } = false;

    public string? Metadata { get; set; }

    public string? HomeDashboardId { get; set; }

    public virtual Dashboard? HomeDashboard { get; set; }

    public virtual ICollection<ProjectMembership> ProjectMembers { get; set; } = new List<ProjectMembership>();

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();

    public virtual ICollection<RoleAssignment> RoleAssignments { get; set; } = new List<RoleAssignment>();

    public virtual ICollection<Dataset> Dataset { get; set; } = new List<Dataset>();

    public virtual ICollection<MembershipInvitation> Invitations { get; set; } = new List<MembershipInvitation>();

    public virtual ICollection<TraceSession> Sessions { get; set; } = new List<TraceSession>();

    public virtual ICollection<Prompt> Prompt { get; set; } = new List<Prompt>();

    public virtual ICollection<Skill> Skill { get; set; } = new List<Skill>();

    public virtual ICollection<SkillFile> SkillFile { get; set; } = new List<SkillFile>();

    public virtual ICollection<SkillBlob> SkillBlob { get; set; } = new List<SkillBlob>();

    public virtual ICollection<Model> Model { get; set; } = new List<Model>();

    public virtual ICollection<EvalTemplate> EvalTemplate { get; set; } = new List<EvalTemplate>();

    public virtual ICollection<JobConfiguration> JobConfiguration { get; set; } = new List<JobConfiguration>();

    public virtual ICollection<JobExecution> JobExecution { get; set; } = new List<JobExecution>();

    public virtual ICollection<Evaluator> Evaluator { get; set; } = new List<Evaluator>();

    public virtual ICollection<EvaluationRule> EvaluationRule { get; set; } = new List<EvaluationRule>();

    public virtual ICollection<EvaluationRuleEvaluatorAssignment> EvaluationRuleEvaluatorAssignment { get; set; } = new List<EvaluationRuleEvaluatorAssignment>();

    public virtual TracingReadinessSettings? TracingReadinessSettings { get; set; }

    public virtual ICollection<LlmApiKeys> LlmApiKeys { get; set; } = new List<LlmApiKeys>();

    public virtual ICollection<PosthogIntegration> PosthogIntegration { get; set; } = new List<PosthogIntegration>();

    public virtual ICollection<MixpanelIntegration> MixpanelIntegration { get; set; } = new List<MixpanelIntegration>();

    public virtual ICollection<BlobStorageIntegration> BlobStorageIntegration { get; set; } = new List<BlobStorageIntegration>();

    public virtual ExternalMediaStorageIntegration? ExternalMediaStorageIntegration { get; set; }

    public virtual ICollection<WebCalloutEndpoint> WebCalloutEndpoints { get; set; } = new List<WebCalloutEndpoint>();

    public virtual ICollection<ScoreConfig> ScoreConfig { get; set; } = new List<ScoreConfig>();

    public virtual ICollection<BatchExport> BatchExport { get; set; } = new List<BatchExport>();

    public virtual ICollection<BatchAction> BatchAction { get; set; } = new List<BatchAction>();

    public virtual ICollection<Comment> Comment { get; set; } = new List<Comment>();

    public virtual ICollection<CommentReaction> CommentReactions { get; set; } = new List<CommentReaction>();

    public virtual ICollection<AnnotationQueue> AnnotationQueue { get; set; } = new List<AnnotationQueue>();

    public virtual ICollection<AnnotationQueueItem> AnnotationQueueItem { get; set; } = new List<AnnotationQueueItem>();

    public virtual ICollection<Media> Media { get; set; } = new List<Media>();

    public virtual ICollection<PromptDependency> PromptDependency { get; set; } = new List<PromptDependency>();

    public virtual ICollection<LlmSchema> LlmSchema { get; set; } = new List<LlmSchema>();

    public virtual ICollection<LlmTool> LlmTool { get; set; } = new List<LlmTool>();

    public virtual ICollection<PromptProtectedLabels> PromptProtectedLabels { get; set; } = new List<PromptProtectedLabels>();

    public virtual ICollection<Dashboard> Dashboard { get; set; } = new List<Dashboard>();

    public virtual ICollection<DashboardWidget> DashboardWidget { get; set; } = new List<DashboardWidget>();

    public virtual ICollection<TableViewPreset> TableViewPreset { get; set; } = new List<TableViewPreset>();

    public virtual ICollection<SessionView> SessionViews { get; set; } = new List<SessionView>();

    public virtual ICollection<SessionViewPinOverride> SessionViewPinOverrides { get; set; } = new List<SessionViewPinOverride>();

    public virtual ICollection<Action> Actions { get; set; } = new List<Action>();

    public virtual ICollection<Trigger> Triggers { get; set; } = new List<Trigger>();

    public virtual ICollection<AutomationExecution> AutomationExecutions { get; set; } = new List<AutomationExecution>();

    public virtual ICollection<Automation> Automation { get; set; } = new List<Automation>();

    public virtual ICollection<Monitor> Monitors { get; set; } = new List<Monitor>();

    public virtual ICollection<DefaultLlmModel> DefaultLlmModel { get; set; } = new List<DefaultLlmModel>();

    public virtual TopicConfig? TopicConfig { get; set; }

    public virtual ICollection<Price> Price { get; set; } = new List<Price>();

    public virtual SlackIntegration? SlackIntegration { get; set; }

    public virtual ICollection<PendingDeletion> PendingDeletion { get; set; } = new List<PendingDeletion>();

    public virtual ICollection<AnnotationQueueAssignment> AnnotationQueueAssignment { get; set; } = new List<AnnotationQueueAssignment>();

    public virtual ICollection<NotificationPreference> NotificationPreference { get; set; } = new List<NotificationPreference>();

    public virtual ICollection<DefaultView> DefaultView { get; set; } = new List<DefaultView>();

    public virtual ICollection<InAppAgentConversation> InAppAgentConversation { get; set; } = new List<InAppAgentConversation>();

    public virtual ICollection<InAppAgentEvent> InAppAgentEvent { get; set; } = new List<InAppAgentEvent>();

    public virtual ICollection<InAppAgentRun> InAppAgentRun { get; set; } = new List<InAppAgentRun>();

    public virtual ICollection<InAppAgentPendingToolApproval> InAppAgentPendingToolApproval { get; set; } = new List<InAppAgentPendingToolApproval>();

    public virtual ICollection<TopicClusteringRun> TopicClusteringRuns { get; set; } = new List<TopicClusteringRun>();

    public virtual ICollection<GatewayConfig> GatewayConfigs { get; set; } = new List<GatewayConfig>();
}
