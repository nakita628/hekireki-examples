defmodule LangWatch.Project do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          slug: String.t(),
          api_key: String.t(),
          legacy_api_key_last_used_at: DateTime.t() | nil,
          lwql_key: String.t(),
          language: String.t(),
          framework: String.t(),
          kind: String.t(),
          aggregate_rule: map() | nil,
          first_message: boolean(),
          integrated: boolean(),
          user_link_template: String.t() | nil,
          trace_sharing_enabled: boolean(),
          presence_enabled: boolean(),
          s3_endpoint: String.t() | nil,
          s3_access_key_id: String.t() | nil,
          s3_secret_access_key: String.t() | nil,
          s3_bucket: String.t() | nil,
          archived_at: DateTime.t() | nil,
          is_personal: boolean(),
          personal_features: map(),
          department_id: String.t() | nil,
          langy_egress_allowlist: map() | nil,
          last_coding_agent_session_at: DateTime.t() | nil,
          last_coding_agent_pull_request_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          team: LangWatch.Team.t() | nil,
          owner_user: LangWatch.User.t() | nil,
          checks: [LangWatch.Monitor.t()],
          costs: [LangWatch.Cost.t()],
          topics: [LangWatch.Topic.t()],
          datasets: [LangWatch.Dataset.t()],
          dataset_records: [LangWatch.DatasetRecord.t()],
          custom_graphs: [LangWatch.CustomGraph.t()],
          dashboards: [LangWatch.Dashboard.t()],
          batch_evaluations: [LangWatch.BatchEvaluation.t()],
          triggers: [LangWatch.Trigger.t()],
          experiments: [LangWatch.Experiment.t()],
          annotations: [LangWatch.Annotation.t()],
          project_secrets: [LangWatch.ProjectSecret.t()],
          trigger_sent: [LangWatch.TriggerSent.t()],
          webhook_deliveries: [LangWatch.WebhookEndpointDelivery.t()],
          annotation_scores: [LangWatch.AnnotationScore.t()],
          share_links: [LangWatch.ShareLink.t()],
          workflows: [LangWatch.Workflow.t()],
          workflow_version: [LangWatch.WorkflowVersion.t()],
          annotation_queue: [LangWatch.AnnotationQueue.t()],
          annotation_queue_item: [LangWatch.AnnotationQueueItem.t()],
          llm_prompt_configs: [LangWatch.LlmPromptConfig.t()],
          analytics: [LangWatch.Analytics.t()],
          notifications: [LangWatch.Notification.t()],
          agents: [LangWatch.Agent.t()],
          evaluators: [LangWatch.Evaluator.t()],
          scenarios: [LangWatch.Scenario.t()],
          suite_configurations: [LangWatch.SimulationSuite.t()],
          saved_views: [LangWatch.SavedView.t()],
          gateway_guardrails: [LangWatch.GatewayGuardrail.t()],
          gateway_change_events: [LangWatch.GatewayChangeEvent.t()],
          pinned_traces: [LangWatch.PinnedTrace.t()],
          trace_edit_overlays: [LangWatch.TraceEditOverlay.t()],
          email_suppressions: [LangWatch.EmailSuppression.t()]
        }

  schema "Project" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:slug, :string)
    field(:api_key, :string, source: :apiKey)
    field(:legacy_api_key_last_used_at, LangWatch.PrismaDateTime, source: :legacyApiKeyLastUsedAt)
    field(:lwql_key, :string, read_after_writes: true, source: :lwqlKey)
    field(:language, :string)
    field(:framework, :string)
    field(:kind, :string, default: "application")
    field(:aggregate_rule, :map, source: :aggregateRule)
    field(:first_message, :boolean, default: false, source: :firstMessage)
    field(:integrated, :boolean, default: false)
    field(:user_link_template, :string, source: :userLinkTemplate)
    field(:trace_sharing_enabled, :boolean, default: true, source: :traceSharingEnabled)
    field(:presence_enabled, :boolean, default: true, source: :presenceEnabled)
    field(:s3_endpoint, :string, source: :s3Endpoint)
    field(:s3_access_key_id, :string, source: :s3AccessKeyId)
    field(:s3_secret_access_key, :string, source: :s3SecretAccessKey)
    field(:s3_bucket, :string, source: :s3Bucket)
    field(:archived_at, LangWatch.PrismaDateTime, source: :archivedAt)
    field(:is_personal, :boolean, default: false, source: :isPersonal)
    field(:personal_features, :map, default: %{}, source: :personalFeatures)
    field(:department_id, :string, source: :departmentId)
    field(:langy_egress_allowlist, :map, source: :langyEgressAllowlist)
    field(:last_coding_agent_session_at, LangWatch.PrismaDateTime, source: :lastCodingAgentSessionAt)
    field(:last_coding_agent_pull_request_at, LangWatch.PrismaDateTime, source: :lastCodingAgentPullRequestAt)
    field(:team_id, :string, source: :teamId)
    field(:owner_user_id, :string, source: :ownerUserId)
    belongs_to(:team, LangWatch.Team, foreign_key: :team_id, define_field: false, type: :string)
    belongs_to(:owner_user, LangWatch.User, foreign_key: :owner_user_id, define_field: false, type: :string)
    has_many(:checks, LangWatch.Monitor, foreign_key: :project_id, references: :id)
    has_many(:costs, LangWatch.Cost, foreign_key: :project_id, references: :id)
    has_many(:topics, LangWatch.Topic, foreign_key: :project_id, references: :id)
    has_many(:datasets, LangWatch.Dataset, foreign_key: :project_id, references: :id)
    has_many(:dataset_records, LangWatch.DatasetRecord, foreign_key: :project_id, references: :id)
    has_many(:custom_graphs, LangWatch.CustomGraph, foreign_key: :project_id, references: :id)
    has_many(:dashboards, LangWatch.Dashboard, foreign_key: :project_id, references: :id)
    has_many(:batch_evaluations, LangWatch.BatchEvaluation, foreign_key: :project_id, references: :id)
    has_many(:triggers, LangWatch.Trigger, foreign_key: :project_id, references: :id)
    has_many(:experiments, LangWatch.Experiment, foreign_key: :project_id, references: :id)
    has_many(:annotations, LangWatch.Annotation, foreign_key: :project_id, references: :id)
    has_many(:project_secrets, LangWatch.ProjectSecret, foreign_key: :project_id, references: :id)
    has_many(:trigger_sent, LangWatch.TriggerSent, foreign_key: :project_id, references: :id)
    has_many(:webhook_deliveries, LangWatch.WebhookEndpointDelivery, foreign_key: :project_id, references: :id)
    has_many(:annotation_scores, LangWatch.AnnotationScore, foreign_key: :project_id, references: :id)
    has_many(:share_links, LangWatch.ShareLink, foreign_key: :project_id, references: :id)
    has_many(:workflows, LangWatch.Workflow, foreign_key: :project_id, references: :id)
    has_many(:workflow_version, LangWatch.WorkflowVersion, foreign_key: :project_id, references: :id)
    has_many(:annotation_queue, LangWatch.AnnotationQueue, foreign_key: :project_id, references: :id)
    has_many(:annotation_queue_item, LangWatch.AnnotationQueueItem, foreign_key: :project_id, references: :id)
    has_many(:llm_prompt_configs, LangWatch.LlmPromptConfig, foreign_key: :project_id, references: :id)
    has_many(:analytics, LangWatch.Analytics, foreign_key: :project_id, references: :id)
    has_many(:notifications, LangWatch.Notification, foreign_key: :project_id, references: :id)
    has_many(:agents, LangWatch.Agent, foreign_key: :project_id, references: :id)
    has_many(:evaluators, LangWatch.Evaluator, foreign_key: :project_id, references: :id)
    has_many(:scenarios, LangWatch.Scenario, foreign_key: :project_id, references: :id)
    has_many(:suite_configurations, LangWatch.SimulationSuite, foreign_key: :project_id, references: :id)
    has_many(:saved_views, LangWatch.SavedView, foreign_key: :project_id, references: :id)
    has_many(:gateway_guardrails, LangWatch.GatewayGuardrail, foreign_key: :project_id, references: :id)
    has_many(:gateway_change_events, LangWatch.GatewayChangeEvent, foreign_key: :project_id, references: :id)
    has_many(:pinned_traces, LangWatch.PinnedTrace, foreign_key: :project_id, references: :id)
    has_many(:trace_edit_overlays, LangWatch.TraceEditOverlay, foreign_key: :project_id, references: :id)
    has_many(:email_suppressions, LangWatch.EmailSuppression, foreign_key: :project_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end