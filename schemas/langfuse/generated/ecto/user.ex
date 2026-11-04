defmodule Langfuse.User do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t() | nil,
          email: String.t() | nil,
          email_verified: DateTime.t() | nil,
          password: String.t() | nil,
          sessions_expired_at: DateTime.t() | nil,
          image: String.t() | nil,
          admin: boolean(),
          v4_beta_enabled: boolean(),
          support_access: map() | nil,
          feature_flags: [String.t()],
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          accounts: [Langfuse.Account.t()],
          sessions: [Langfuse.Session.t()],
          organization_memberships: [Langfuse.OrganizationMembership.t()],
          project_memberships: [Langfuse.ProjectMembership.t()],
          invitations: [Langfuse.MembershipInvitation.t()],
          annotated_locked_item: [Langfuse.AnnotationQueueItem.t()],
          annotated_completed_item: [Langfuse.AnnotationQueueItem.t()],
          dashboard_widgets_created: [Langfuse.DashboardWidget.t()],
          dashboard_widgets_updated: [Langfuse.DashboardWidget.t()],
          dashboard_created: [Langfuse.Dashboard.t()],
          dashboard_updated: [Langfuse.Dashboard.t()],
          table_view_preset_created: [Langfuse.TableViewPreset.t()],
          table_view_preset_updated: [Langfuse.TableViewPreset.t()],
          monitors_created: [Langfuse.Monitor.t()],
          monitors_updated: [Langfuse.Monitor.t()],
          topic_configs_created: [Langfuse.TopicConfig.t()],
          topic_configs_updated: [Langfuse.TopicConfig.t()],
          tracing_readiness_created: [Langfuse.TracingReadinessSettings.t()],
          tracing_readiness_updated: [Langfuse.TracingReadinessSettings.t()],
          annotation_queue_assignment: [Langfuse.AnnotationQueueAssignment.t()],
          surveys: [Langfuse.Survey.t()],
          comment_reactions: [Langfuse.CommentReaction.t()],
          notification_preferences: [Langfuse.NotificationPreference.t()],
          default_views: [Langfuse.DefaultView.t()],
          in_app_agent_conversations: [Langfuse.InAppAgentConversation.t()],
          in_app_agent_runs: [Langfuse.InAppAgentRun.t()],
          api_keys_created: [Langfuse.ApiKey.t()],
          api_keys: [Langfuse.ApiKey.t()],
          role_assignments: [Langfuse.RoleAssignment.t()],
          evaluators_created: [Langfuse.Evaluator.t()],
          evaluator_versions_created: [Langfuse.EvaluatorVersion.t()],
          evaluation_rules_created: [Langfuse.EvaluationRule.t()]
        }

  schema "users" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:email, :string)
    field(:email_verified, Langfuse.PrismaDateTime)
    field(:password, :string)
    field(:sessions_expired_at, Langfuse.PrismaDateTime)
    field(:image, :string)
    field(:admin, :boolean, default: false)
    field(:v4_beta_enabled, :boolean, default: false)
    field(:support_access, :map)
    field(:feature_flags, {:array, :string})
    has_many(:accounts, Langfuse.Account, foreign_key: :user_id, references: :id)
    has_many(:sessions, Langfuse.Session, foreign_key: :user_id, references: :id)
    has_many(:organization_memberships, Langfuse.OrganizationMembership, foreign_key: :user_id, references: :id)
    has_many(:project_memberships, Langfuse.ProjectMembership, foreign_key: :user_id, references: :id)
    has_many(:invitations, Langfuse.MembershipInvitation, foreign_key: :invited_by_user_id, references: :id)
    has_many(:annotated_locked_item, Langfuse.AnnotationQueueItem, foreign_key: :locked_by_user_id, references: :id)
    has_many(:annotated_completed_item, Langfuse.AnnotationQueueItem, foreign_key: :annotator_user_id, references: :id)
    has_many(:dashboard_widgets_created, Langfuse.DashboardWidget, foreign_key: :created_by, references: :id)
    has_many(:dashboard_widgets_updated, Langfuse.DashboardWidget, foreign_key: :updated_by, references: :id)
    has_many(:dashboard_created, Langfuse.Dashboard, foreign_key: :created_by, references: :id)
    has_many(:dashboard_updated, Langfuse.Dashboard, foreign_key: :updated_by, references: :id)
    has_many(:table_view_preset_created, Langfuse.TableViewPreset, foreign_key: :created_by, references: :id)
    has_many(:table_view_preset_updated, Langfuse.TableViewPreset, foreign_key: :updated_by, references: :id)
    has_many(:monitors_created, Langfuse.Monitor, foreign_key: :created_by, references: :id)
    has_many(:monitors_updated, Langfuse.Monitor, foreign_key: :updated_by, references: :id)
    has_many(:topic_configs_created, Langfuse.TopicConfig, foreign_key: :created_by, references: :id)
    has_many(:topic_configs_updated, Langfuse.TopicConfig, foreign_key: :updated_by, references: :id)
    has_many(:tracing_readiness_created, Langfuse.TracingReadinessSettings, foreign_key: :created_by, references: :id)
    has_many(:tracing_readiness_updated, Langfuse.TracingReadinessSettings, foreign_key: :updated_by, references: :id)
    has_many(:annotation_queue_assignment, Langfuse.AnnotationQueueAssignment, foreign_key: :user_id, references: :id)
    has_many(:surveys, Langfuse.Survey, foreign_key: :user_id, references: :id)
    has_many(:comment_reactions, Langfuse.CommentReaction, foreign_key: :user_id, references: :id)
    has_many(:notification_preferences, Langfuse.NotificationPreference, foreign_key: :user_id, references: :id)
    has_many(:default_views, Langfuse.DefaultView, foreign_key: :user_id, references: :id)
    has_many(:in_app_agent_conversations, Langfuse.InAppAgentConversation, foreign_key: :created_by_user_id, references: :id)
    has_many(:in_app_agent_runs, Langfuse.InAppAgentRun, foreign_key: :triggered_by_user_id, references: :id)
    has_many(:api_keys_created, Langfuse.ApiKey, foreign_key: :created_by_user_id, references: :id)
    has_many(:api_keys, Langfuse.ApiKey, foreign_key: :user_id, references: :id)
    has_many(:role_assignments, Langfuse.RoleAssignment, foreign_key: :user_id, references: :id)
    has_many(:evaluators_created, Langfuse.Evaluator, foreign_key: :created_by_user_id, references: :id)
    has_many(:evaluator_versions_created, Langfuse.EvaluatorVersion, foreign_key: :created_by_user_id, references: :id)
    has_many(:evaluation_rules_created, Langfuse.EvaluationRule, foreign_key: :created_by_user_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end