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
  has_many :dataset, dependent: :destroy
  has_many :invitations, class_name: "MembershipInvitation", inverse_of: :project, dependent: :nullify
  has_many :sessions, class_name: "TraceSession", inverse_of: :project, dependent: :destroy
  has_many :prompt, dependent: :destroy
  has_many :skill, dependent: :destroy
  has_many :skill_file, dependent: :destroy
  has_many :skill_blob, dependent: :destroy
  has_many :model, dependent: :destroy
  has_many :eval_template, dependent: :destroy
  has_many :job_configuration, dependent: :destroy
  has_many :job_execution, dependent: :destroy
  has_many :evaluator, dependent: :destroy
  has_many :evaluation_rule, dependent: :destroy
  has_many :evaluation_rule_evaluator_assignment, dependent: :destroy
  has_many :llm_api_keys, class_name: "LlmApiKeys", inverse_of: :project, dependent: :destroy
  has_many :posthog_integration, dependent: :destroy
  has_many :mixpanel_integration, dependent: :destroy
  has_many :blob_storage_integration, dependent: :destroy
  has_many :web_callout_endpoints, dependent: :destroy
  has_many :score_config, dependent: :destroy
  has_many :batch_export, dependent: :destroy
  has_many :batch_action, dependent: :destroy
  has_many :comment, dependent: :destroy
  has_many :comment_reactions, dependent: :destroy
  has_many :annotation_queue, dependent: :destroy
  has_many :annotation_queue_item, dependent: :destroy
  has_many :media, class_name: "Media", inverse_of: :project, dependent: :destroy
  has_many :prompt_dependency, dependent: :destroy
  has_many :llm_schema, dependent: :destroy
  has_many :llm_tool, dependent: :destroy
  has_many :prompt_protected_labels, class_name: "PromptProtectedLabels", inverse_of: :project, dependent: :destroy
  has_many :dashboard, dependent: :destroy
  has_many :dashboard_widget, dependent: :destroy
  has_many :table_view_preset, dependent: :destroy
  has_many :session_views, dependent: :destroy
  has_many :session_view_pin_overrides, dependent: :destroy
  has_many :actions, dependent: :destroy
  has_many :triggers, dependent: :destroy
  has_many :automation_executions, dependent: :destroy
  has_many :automation, dependent: :destroy
  has_many :monitors, class_name: "MonitorRecord", inverse_of: :project, dependent: :destroy
  has_many :default_llm_model, dependent: :destroy
  has_many :price, dependent: :destroy
  has_many :pending_deletion, dependent: :destroy
  has_many :annotation_queue_assignment, dependent: :destroy
  has_many :notification_preference, dependent: :destroy
  has_many :default_view, dependent: :destroy
  has_many :in_app_agent_conversation, dependent: :destroy
  has_many :in_app_agent_event, dependent: :destroy
  has_many :in_app_agent_run, dependent: :destroy
  has_many :in_app_agent_pending_tool_approval, dependent: :destroy
  has_many :topic_clustering_runs, dependent: :destroy
  has_many :gateway_configs, foreign_key: "default_ingestion_project_id", inverse_of: :default_ingestion_project,
                             dependent: :nullify
end
