class User < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :admin, default: false
  attribute :v4_beta_enabled, default: false
  attribute :feature_flags, default: -> { [] }

  validates :email, uniqueness: { allow_nil: true }

  has_many :accounts, dependent: :destroy
  has_many :sessions, dependent: :destroy
  has_many :organization_memberships, dependent: :destroy
  has_many :project_memberships, dependent: :destroy
  has_many :invitations, class_name: "MembershipInvitation", foreign_key: "invited_by_user_id",
                         inverse_of: :invited_by_user, dependent: :nullify
  has_many :annotated_locked_item, class_name: "AnnotationQueueItem", foreign_key: "locked_by_user_id",
                                   inverse_of: :locked_by_user, dependent: :nullify
  has_many :annotated_completed_item, class_name: "AnnotationQueueItem", foreign_key: "annotator_user_id",
                                      inverse_of: :annotator_user, dependent: :nullify
  has_many :dashboard_widgets_created, class_name: "DashboardWidget", foreign_key: "created_by",
                                       inverse_of: :created_by_user, dependent: :nullify
  has_many :dashboard_widgets_updated, class_name: "DashboardWidget", foreign_key: "updated_by",
                                       inverse_of: :updated_by_user, dependent: :nullify
  has_many :dashboard_created, class_name: "Dashboard", foreign_key: "created_by", inverse_of: :created_by_user,
                               dependent: :nullify
  has_many :dashboard_updated, class_name: "Dashboard", foreign_key: "updated_by", inverse_of: :updated_by_user,
                               dependent: :nullify
  has_many :table_view_preset_created, class_name: "TableViewPreset", foreign_key: "created_by",
                                       inverse_of: :created_by_user, dependent: :nullify
  has_many :table_view_preset_updated, class_name: "TableViewPreset", foreign_key: "updated_by",
                                       inverse_of: :updated_by_user, dependent: :nullify
  has_many :session_views_created, class_name: "SessionView", foreign_key: "created_by", inverse_of: :created_by_user,
                                   dependent: :nullify
  has_many :session_view_pin_overrides, dependent: :destroy
  has_many :monitors_created, class_name: "MonitorRecord", foreign_key: "created_by", inverse_of: :created_by_user,
                              dependent: :nullify
  has_many :monitors_updated, class_name: "MonitorRecord", foreign_key: "updated_by", inverse_of: :updated_by_user,
                              dependent: :nullify
  has_many :topic_configs_created, class_name: "TopicConfig", foreign_key: "created_by", inverse_of: :created_by_user,
                                   dependent: :nullify
  has_many :topic_configs_updated, class_name: "TopicConfig", foreign_key: "updated_by", inverse_of: :updated_by_user,
                                   dependent: :nullify
  has_many :tracing_readiness_created, class_name: "TracingReadinessSettings", foreign_key: "created_by",
                                       inverse_of: :created_by_user, dependent: :nullify
  has_many :tracing_readiness_updated, class_name: "TracingReadinessSettings", foreign_key: "updated_by",
                                       inverse_of: :updated_by_user, dependent: :nullify
  has_many :annotation_queue_assignment, dependent: :destroy
  has_many :surveys, dependent: :destroy
  has_many :comment_reactions, dependent: :destroy
  has_many :notification_preferences, dependent: :destroy
  has_many :default_views, dependent: :destroy
  has_many :in_app_agent_conversations, foreign_key: "created_by_user_id", inverse_of: :created_by_user,
                                        dependent: :nullify
  has_many :in_app_agent_runs, foreign_key: "triggered_by_user_id", inverse_of: :triggered_by_user, dependent: :nullify
  has_many :api_keys_created, class_name: "ApiKey", foreign_key: "created_by_user_id", inverse_of: :created_by_user,
                              dependent: :nullify
  has_many :api_keys, dependent: :destroy
  has_many :role_assignments, dependent: :destroy
  has_many :evaluators_created, class_name: "Evaluator", foreign_key: "created_by_user_id", inverse_of: :created_by_user,
                                dependent: :nullify
  has_many :evaluator_versions_created, class_name: "EvaluatorVersion", foreign_key: "created_by_user_id",
                                        inverse_of: :created_by_user, dependent: :nullify
  has_many :evaluation_rules_created, class_name: "EvaluationRule", foreign_key: "created_by_user_id",
                                      inverse_of: :created_by_user, dependent: :nullify
end
