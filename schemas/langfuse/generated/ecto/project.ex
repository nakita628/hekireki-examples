defmodule Langfuse.Project do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          deleted_at: DateTime.t() | nil,
          name: String.t(),
          retention_days: integer() | nil,
          has_traces: boolean(),
          metadata: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          home_dashboard: Langfuse.Dashboard.t() | nil,
          organization: Langfuse.Organization.t() | nil,
          tracing_readiness_settings: Langfuse.TracingReadinessSettings.t() | nil,
          external_media_storage_integration: Langfuse.ExternalMediaStorageIntegration.t() | nil,
          topic_config: Langfuse.TopicConfig.t() | nil,
          slack_integration: Langfuse.SlackIntegration.t() | nil,
          project_members: [Langfuse.ProjectMembership.t()],
          api_keys: [Langfuse.ApiKey.t()],
          role_assignments: [Langfuse.RoleAssignment.t()],
          dataset: [Langfuse.Dataset.t()],
          invitations: [Langfuse.MembershipInvitation.t()],
          sessions: [Langfuse.TraceSession.t()],
          prompt: [Langfuse.Prompt.t()],
          skill: [Langfuse.Skill.t()],
          skill_file: [Langfuse.SkillFile.t()],
          skill_blob: [Langfuse.SkillBlob.t()],
          model: [Langfuse.Model.t()],
          eval_template: [Langfuse.EvalTemplate.t()],
          job_configuration: [Langfuse.JobConfiguration.t()],
          job_execution: [Langfuse.JobExecution.t()],
          evaluator: [Langfuse.Evaluator.t()],
          evaluation_rule: [Langfuse.EvaluationRule.t()],
          evaluation_rule_evaluator_assignment: [Langfuse.EvaluationRuleEvaluatorAssignment.t()],
          llm_api_keys: [Langfuse.LlmApiKeys.t()],
          posthog_integration: [Langfuse.PosthogIntegration.t()],
          mixpanel_integration: [Langfuse.MixpanelIntegration.t()],
          blob_storage_integration: [Langfuse.BlobStorageIntegration.t()],
          web_callout_endpoints: [Langfuse.WebCalloutEndpoint.t()],
          score_config: [Langfuse.ScoreConfig.t()],
          batch_export: [Langfuse.BatchExport.t()],
          batch_action: [Langfuse.BatchAction.t()],
          comment: [Langfuse.Comment.t()],
          comment_reactions: [Langfuse.CommentReaction.t()],
          annotation_queue: [Langfuse.AnnotationQueue.t()],
          annotation_queue_item: [Langfuse.AnnotationQueueItem.t()],
          media: [Langfuse.Media.t()],
          prompt_dependency: [Langfuse.PromptDependency.t()],
          llm_schema: [Langfuse.LlmSchema.t()],
          llm_tool: [Langfuse.LlmTool.t()],
          prompt_protected_labels: [Langfuse.PromptProtectedLabels.t()],
          dashboard: [Langfuse.Dashboard.t()],
          dashboard_widget: [Langfuse.DashboardWidget.t()],
          table_view_preset: [Langfuse.TableViewPreset.t()],
          session_views: [Langfuse.SessionView.t()],
          session_view_pin_overrides: [Langfuse.SessionViewPinOverride.t()],
          actions: [Langfuse.Action.t()],
          triggers: [Langfuse.Trigger.t()],
          automation_executions: [Langfuse.AutomationExecution.t()],
          automation: [Langfuse.Automation.t()],
          monitors: [Langfuse.Monitor.t()],
          default_llm_model: [Langfuse.DefaultLlmModel.t()],
          price: [Langfuse.Price.t()],
          pending_deletion: [Langfuse.PendingDeletion.t()],
          annotation_queue_assignment: [Langfuse.AnnotationQueueAssignment.t()],
          notification_preference: [Langfuse.NotificationPreference.t()],
          default_view: [Langfuse.DefaultView.t()],
          in_app_agent_conversation: [Langfuse.InAppAgentConversation.t()],
          in_app_agent_event: [Langfuse.InAppAgentEvent.t()],
          in_app_agent_run: [Langfuse.InAppAgentRun.t()],
          in_app_agent_pending_tool_approval: [Langfuse.InAppAgentPendingToolApproval.t()],
          topic_clustering_runs: [Langfuse.TopicClusteringRun.t()],
          gateway_configs: [Langfuse.GatewayConfig.t()]
        }

  schema "projects" do
    field(:id, :string, primary_key: true)
    field(:deleted_at, Langfuse.PrismaDateTime)
    field(:name, :string)
    field(:retention_days, :integer)
    field(:has_traces, :boolean, default: false)
    field(:metadata, :map)
    belongs_to(:home_dashboard, Langfuse.Dashboard, foreign_key: :home_dashboard_id, type: :string)
    belongs_to(:organization, Langfuse.Organization, foreign_key: :org_id, type: :string)
    has_one(:tracing_readiness_settings, Langfuse.TracingReadinessSettings, foreign_key: :project_id, references: :id)
    has_one(:external_media_storage_integration, Langfuse.ExternalMediaStorageIntegration, foreign_key: :project_id, references: :id)
    has_one(:topic_config, Langfuse.TopicConfig, foreign_key: :project_id, references: :id)
    has_one(:slack_integration, Langfuse.SlackIntegration, foreign_key: :project_id, references: :id)
    has_many(:project_members, Langfuse.ProjectMembership, foreign_key: :project_id, references: :id)
    has_many(:api_keys, Langfuse.ApiKey, foreign_key: :project_id, references: :id)
    has_many(:role_assignments, Langfuse.RoleAssignment, foreign_key: :project_id, references: :id)
    has_many(:dataset, Langfuse.Dataset, foreign_key: :project_id, references: :id)
    has_many(:invitations, Langfuse.MembershipInvitation, foreign_key: :project_id, references: :id)
    has_many(:sessions, Langfuse.TraceSession, foreign_key: :project_id, references: :id)
    has_many(:prompt, Langfuse.Prompt, foreign_key: :project_id, references: :id)
    has_many(:skill, Langfuse.Skill, foreign_key: :project_id, references: :id)
    has_many(:skill_file, Langfuse.SkillFile, foreign_key: :project_id, references: :id)
    has_many(:skill_blob, Langfuse.SkillBlob, foreign_key: :project_id, references: :id)
    has_many(:model, Langfuse.Model, foreign_key: :project_id, references: :id)
    has_many(:eval_template, Langfuse.EvalTemplate, foreign_key: :project_id, references: :id)
    has_many(:job_configuration, Langfuse.JobConfiguration, foreign_key: :project_id, references: :id)
    has_many(:job_execution, Langfuse.JobExecution, foreign_key: :project_id, references: :id)
    has_many(:evaluator, Langfuse.Evaluator, foreign_key: :project_id, references: :id)
    has_many(:evaluation_rule, Langfuse.EvaluationRule, foreign_key: :project_id, references: :id)
    has_many(:evaluation_rule_evaluator_assignment, Langfuse.EvaluationRuleEvaluatorAssignment, foreign_key: :project_id, references: :id)
    has_many(:llm_api_keys, Langfuse.LlmApiKeys, foreign_key: :project_id, references: :id)
    has_many(:posthog_integration, Langfuse.PosthogIntegration, foreign_key: :project_id, references: :id)
    has_many(:mixpanel_integration, Langfuse.MixpanelIntegration, foreign_key: :project_id, references: :id)
    has_many(:blob_storage_integration, Langfuse.BlobStorageIntegration, foreign_key: :project_id, references: :id)
    has_many(:web_callout_endpoints, Langfuse.WebCalloutEndpoint, foreign_key: :project_id, references: :id)
    has_many(:score_config, Langfuse.ScoreConfig, foreign_key: :project_id, references: :id)
    has_many(:batch_export, Langfuse.BatchExport, foreign_key: :project_id, references: :id)
    has_many(:batch_action, Langfuse.BatchAction, foreign_key: :project_id, references: :id)
    has_many(:comment, Langfuse.Comment, foreign_key: :project_id, references: :id)
    has_many(:comment_reactions, Langfuse.CommentReaction, foreign_key: :project_id, references: :id)
    has_many(:annotation_queue, Langfuse.AnnotationQueue, foreign_key: :project_id, references: :id)
    has_many(:annotation_queue_item, Langfuse.AnnotationQueueItem, foreign_key: :project_id, references: :id)
    has_many(:media, Langfuse.Media, foreign_key: :project_id, references: :id)
    has_many(:prompt_dependency, Langfuse.PromptDependency, foreign_key: :project_id, references: :id)
    has_many(:llm_schema, Langfuse.LlmSchema, foreign_key: :project_id, references: :id)
    has_many(:llm_tool, Langfuse.LlmTool, foreign_key: :project_id, references: :id)
    has_many(:prompt_protected_labels, Langfuse.PromptProtectedLabels, foreign_key: :project_id, references: :id)
    has_many(:dashboard, Langfuse.Dashboard, foreign_key: :project_id, references: :id)
    has_many(:dashboard_widget, Langfuse.DashboardWidget, foreign_key: :project_id, references: :id)
    has_many(:table_view_preset, Langfuse.TableViewPreset, foreign_key: :project_id, references: :id)
    has_many(:session_views, Langfuse.SessionView, foreign_key: :project_id, references: :id)
    has_many(:session_view_pin_overrides, Langfuse.SessionViewPinOverride, foreign_key: :project_id, references: :id)
    has_many(:actions, Langfuse.Action, foreign_key: :project_id, references: :id)
    has_many(:triggers, Langfuse.Trigger, foreign_key: :project_id, references: :id)
    has_many(:automation_executions, Langfuse.AutomationExecution, foreign_key: :project_id, references: :id)
    has_many(:automation, Langfuse.Automation, foreign_key: :project_id, references: :id)
    has_many(:monitors, Langfuse.Monitor, foreign_key: :project_id, references: :id)
    has_many(:default_llm_model, Langfuse.DefaultLlmModel, foreign_key: :project_id, references: :id)
    has_many(:price, Langfuse.Price, foreign_key: :project_id, references: :id)
    has_many(:pending_deletion, Langfuse.PendingDeletion, foreign_key: :project_id, references: :id)
    has_many(:annotation_queue_assignment, Langfuse.AnnotationQueueAssignment, foreign_key: :project_id, references: :id)
    has_many(:notification_preference, Langfuse.NotificationPreference, foreign_key: :project_id, references: :id)
    has_many(:default_view, Langfuse.DefaultView, foreign_key: :project_id, references: :id)
    has_many(:in_app_agent_conversation, Langfuse.InAppAgentConversation, foreign_key: :project_id, references: :id)
    has_many(:in_app_agent_event, Langfuse.InAppAgentEvent, foreign_key: :project_id, references: :id)
    has_many(:in_app_agent_run, Langfuse.InAppAgentRun, foreign_key: :project_id, references: :id)
    has_many(:in_app_agent_pending_tool_approval, Langfuse.InAppAgentPendingToolApproval, foreign_key: :project_id, references: :id)
    has_many(:topic_clustering_runs, Langfuse.TopicClusteringRun, foreign_key: :project_id, references: :id)
    has_many(:gateway_configs, Langfuse.GatewayConfig, foreign_key: :default_ingestion_project_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end