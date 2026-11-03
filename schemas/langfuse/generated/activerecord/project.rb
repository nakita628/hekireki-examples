class Project < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :has_traces, default: false

  validates :name, presence: true

  belongs_to :home_dashboard, class_name: "Dashboard", optional: true, inverse_of: :home_dashboard_projects
  belongs_to :organization, foreign_key: "org_id", inverse_of: :projects
  has_one :tracing_readiness_settings, dependent: :destroy
  has_one :external_media_storage_integration, dependent: :destroy
  has_one :topic_config, dependent: :destroy
  has_one :slack_integration, dependent: :destroy
  has_many :project_members, class_name: "ProjectMembership", inverse_of: :project, dependent: :destroy
  has_many :api_keys, dependent: :destroy
  has_many :role_assignments, dependent: :destroy
  has_many :dataset, class_name: "Dataset", inverse_of: :project, dependent: :destroy
  has_many :invitations, class_name: "MembershipInvitation", inverse_of: :project, dependent: :nullify
  has_many :sessions, class_name: "TraceSession", inverse_of: :project, dependent: :destroy
  has_many :prompt, class_name: "Prompt", inverse_of: :project, dependent: :destroy
  has_many :skill, class_name: "Skill", inverse_of: :project, dependent: :destroy
  has_many :skill_file, class_name: "SkillFile", inverse_of: :project, dependent: :destroy
  has_many :skill_blob, class_name: "SkillBlob", inverse_of: :project, dependent: :destroy
  has_many :model, class_name: "Model", inverse_of: :project, dependent: :destroy
  has_many :eval_template, class_name: "EvalTemplate", inverse_of: :project, dependent: :destroy
  has_many :job_configuration, class_name: "JobConfiguration", inverse_of: :project, dependent: :destroy
  has_many :job_execution, class_name: "JobExecution", inverse_of: :project, dependent: :destroy
  has_many :evaluator, class_name: "Evaluator", inverse_of: :project, dependent: :destroy
  has_many :evaluation_rule, class_name: "EvaluationRule", inverse_of: :project, dependent: :destroy
  has_many :evaluation_rule_evaluator_assignment, class_name: "EvaluationRuleEvaluatorAssignment", inverse_of: :project,
                                                  dependent: :destroy
  has_many :llm_api_keys, dependent: :destroy
  has_many :posthog_integration, class_name: "PosthogIntegration", inverse_of: :project, dependent: :destroy
  has_many :mixpanel_integration, class_name: "MixpanelIntegration", inverse_of: :project, dependent: :destroy
  has_many :blob_storage_integration, class_name: "BlobStorageIntegration", inverse_of: :project, dependent: :destroy
  has_many :web_callout_endpoints, dependent: :destroy
  has_many :score_config, class_name: "ScoreConfig", inverse_of: :project, dependent: :destroy
  has_many :batch_export, class_name: "BatchExport", inverse_of: :project, dependent: :destroy
  has_many :batch_action, class_name: "BatchAction", inverse_of: :project, dependent: :destroy
  has_many :comment, class_name: "Comment", inverse_of: :project, dependent: :destroy
  has_many :comment_reactions, dependent: :destroy
  has_many :annotation_queue, class_name: "AnnotationQueue", inverse_of: :project, dependent: :destroy
  has_many :annotation_queue_item, class_name: "AnnotationQueueItem", inverse_of: :project, dependent: :destroy
  has_many :media, dependent: :destroy
  has_many :prompt_dependency, class_name: "PromptDependency", inverse_of: :project, dependent: :destroy
  has_many :llm_schema, class_name: "LlmSchema", inverse_of: :project, dependent: :destroy
  has_many :llm_tool, class_name: "LlmTool", inverse_of: :project, dependent: :destroy
  has_many :prompt_protected_labels, dependent: :destroy
  has_many :dashboard, class_name: "Dashboard", inverse_of: :project, dependent: :destroy
  has_many :dashboard_widget, class_name: "DashboardWidget", inverse_of: :project, dependent: :destroy
  has_many :table_view_preset, class_name: "TableViewPreset", inverse_of: :project, dependent: :destroy
  has_many :actions, dependent: :destroy
  has_many :triggers, dependent: :destroy
  has_many :automation_executions, dependent: :destroy
  has_many :automation, class_name: "Automation", inverse_of: :project, dependent: :destroy
  has_many :monitors, dependent: :destroy
  has_many :default_llm_model, class_name: "DefaultLlmModel", inverse_of: :project, dependent: :destroy
  has_many :price, class_name: "Price", inverse_of: :project, dependent: :destroy
  has_many :pending_deletion, class_name: "PendingDeletion", inverse_of: :project, dependent: :destroy
  has_many :annotation_queue_assignment, class_name: "AnnotationQueueAssignment", inverse_of: :project,
                                         dependent: :destroy
  has_many :notification_preference, class_name: "NotificationPreference", inverse_of: :project, dependent: :destroy
  has_many :default_view, class_name: "DefaultView", inverse_of: :project, dependent: :destroy
  has_many :in_app_agent_conversation, class_name: "InAppAgentConversation", inverse_of: :project, dependent: :destroy
  has_many :in_app_agent_event, class_name: "InAppAgentEvent", inverse_of: :project, dependent: :destroy
  has_many :in_app_agent_run, class_name: "InAppAgentRun", inverse_of: :project, dependent: :destroy
  has_many :in_app_agent_pending_tool_approval, class_name: "InAppAgentPendingToolApproval", inverse_of: :project,
                                                dependent: :destroy
  has_many :topic_clustering_runs, dependent: :destroy
  has_many :gateway_configs, foreign_key: "default_ingestion_project_id", inverse_of: :default_ingestion_project,
                             dependent: :nullify
end
