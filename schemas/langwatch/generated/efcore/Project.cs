#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class Project
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string ApiKey { get; set; } = null!;

    public DateTime? LegacyApiKeyLastUsedAt { get; set; }

    public string LwqlKey { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public string Language { get; set; } = null!;

    public string Framework { get; set; } = null!;

    public string Kind { get; set; } = "application";

    public string? AggregateRule { get; set; }

    public bool FirstMessage { get; set; } = false;

    public bool Integrated { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UserLinkTemplate { get; set; }

    public virtual ICollection<Monitor> Checks { get; set; } = new List<Monitor>();

    public virtual ICollection<Cost> Costs { get; set; } = new List<Cost>();

    public virtual ICollection<Topic> Topics { get; set; } = new List<Topic>();

    public virtual ICollection<Dataset> Datasets { get; set; } = new List<Dataset>();

    public virtual ICollection<DatasetRecord> DatasetRecords { get; set; } = new List<DatasetRecord>();

    public virtual ICollection<CustomGraph> CustomGraphs { get; set; } = new List<CustomGraph>();

    public virtual ICollection<Dashboard> Dashboards { get; set; } = new List<Dashboard>();

    public virtual ICollection<BatchEvaluation> BatchEvaluations { get; set; } = new List<BatchEvaluation>();

    public bool TraceSharingEnabled { get; set; } = true;

    public bool PresenceEnabled { get; set; } = true;

    public virtual ICollection<Trigger> Triggers { get; set; } = new List<Trigger>();

    public virtual ICollection<Experiment> Experiments { get; set; } = new List<Experiment>();

    public virtual ICollection<Annotation> Annotations { get; set; } = new List<Annotation>();

    public virtual ICollection<ProjectSecret> ProjectSecrets { get; set; } = new List<ProjectSecret>();

    public virtual ICollection<TriggerSent> TriggerSent { get; set; } = new List<TriggerSent>();

    public virtual ICollection<WebhookEndpointDelivery> WebhookDeliveries { get; set; } = new List<WebhookEndpointDelivery>();

    public virtual ICollection<AnnotationScore> AnnotationScores { get; set; } = new List<AnnotationScore>();

    public virtual ICollection<ShareLink> ShareLinks { get; set; } = new List<ShareLink>();

    public virtual ICollection<Workflow> Workflows { get; set; } = new List<Workflow>();

    public virtual ICollection<WorkflowVersion> WorkflowVersion { get; set; } = new List<WorkflowVersion>();

    public virtual ICollection<AnnotationQueue> AnnotationQueue { get; set; } = new List<AnnotationQueue>();

    public virtual ICollection<AnnotationQueueItem> AnnotationQueueItem { get; set; } = new List<AnnotationQueueItem>();

    public string? S3Endpoint { get; set; }

    public string? S3AccessKeyId { get; set; }

    public string? S3SecretAccessKey { get; set; }

    public string? S3Bucket { get; set; }

    public virtual ICollection<LlmPromptConfig> LlmPromptConfigs { get; set; } = new List<LlmPromptConfig>();

    public virtual ICollection<Analytics> Analytics { get; set; } = new List<Analytics>();

    public DateTime? ArchivedAt { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();

    public virtual ICollection<Evaluator> Evaluators { get; set; } = new List<Evaluator>();

    public virtual ICollection<Scenario> Scenarios { get; set; } = new List<Scenario>();

    public virtual ICollection<SimulationSuite> SuiteConfigurations { get; set; } = new List<SimulationSuite>();

    public virtual ICollection<SavedView> SavedViews { get; set; } = new List<SavedView>();

    public virtual ICollection<GatewayGuardrail> GatewayGuardrails { get; set; } = new List<GatewayGuardrail>();

    public virtual ICollection<GatewayChangeEvent> GatewayChangeEvents { get; set; } = new List<GatewayChangeEvent>();

    public virtual ICollection<PinnedTrace> PinnedTraces { get; set; } = new List<PinnedTrace>();

    public virtual ICollection<TraceEditOverlay> TraceEditOverlays { get; set; } = new List<TraceEditOverlay>();

    public virtual ICollection<EmailSuppression> EmailSuppressions { get; set; } = new List<EmailSuppression>();

    public bool IsPersonal { get; set; } = false;

    public string? OwnerUserId { get; set; }

    public virtual User? OwnerUser { get; set; }

    public string PersonalFeatures { get; set; } = "{}";

    public string? DepartmentId { get; set; }

    public string? LangyEgressAllowlist { get; set; }

    public DateTime? LastCodingAgentSessionAt { get; set; }

    public DateTime? LastCodingAgentPullRequestAt { get; set; }
}
