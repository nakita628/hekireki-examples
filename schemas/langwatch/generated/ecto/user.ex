defmodule LangWatch.User do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t() | nil,
          email: String.t() | nil,
          email_verified: boolean(),
          signup_confirmation_pending: boolean(),
          passkey_signup_claim_hash: String.t() | nil,
          image: String.t() | nil,
          pending_sso_setup: boolean(),
          user_hash_key: String.t() | nil,
          two_factor_enabled: boolean(),
          last_login_at: DateTime.t() | nil,
          deactivated_at: DateTime.t() | nil,
          last_home_path: String.t() | nil,
          traces_explorer_tour_dismissed_at: DateTime.t() | nil,
          langy_code_access_preference: String.t() | nil,
          passkey_nudge_dismissed_at: DateTime.t() | nil,
          join_offer_dismissed_domains: [String.t()],
          notification_preferences: map(),
          password_reset_required_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          accounts: [LangWatch.Account.t()],
          account_credentials: [LangWatch.AccountCredential.t()],
          sessions: [LangWatch.Session.t()],
          two_factors: [LangWatch.TwoFactor.t()],
          passkeys: [LangWatch.Passkey.t()],
          team_memberships: [LangWatch.TeamUser.t()],
          org_memberships: [LangWatch.OrganizationUser.t()],
          scim_user_resources: [LangWatch.ScimUserResource.t()],
          annotation: [LangWatch.Annotation.t()],
          share_links: [LangWatch.ShareLink.t()],
          workflow: [LangWatch.Workflow.t()],
          workflow_version: [LangWatch.WorkflowVersion.t()],
          annotation_queues: [LangWatch.AnnotationQueueMembers.t()],
          assigned_queue_items: [LangWatch.AnnotationQueueItem.t()],
          created_queue_items: [LangWatch.AnnotationQueueItem.t()],
          llm_prompt_config_versions: [LangWatch.LlmPromptConfigVersion.t()],
          scenarios: [LangWatch.Scenario.t()],
          invite_requests: [LangWatch.OrganizationInvite.t()],
          secrets_created: [LangWatch.ProjectSecret.t()],
          secrets_updated: [LangWatch.ProjectSecret.t()],
          saved_views: [LangWatch.SavedView.t()],
          version_tags_created: [LangWatch.PromptTagAssignment.t()],
          version_tags_updated: [LangWatch.PromptTagAssignment.t()],
          prompt_tags_created: [LangWatch.PromptTag.t()],
          prompt_tags_updated: [LangWatch.PromptTag.t()],
          group_memberships: [LangWatch.GroupMembership.t()],
          role_bindings: [LangWatch.RoleBinding.t()],
          scim_external_ids: [LangWatch.ScimExternalId.t()],
          api_keys: [LangWatch.ApiKey.t()],
          gateway_budgets_created: [LangWatch.GatewayBudget.t()],
          principal_virtual_keys: [LangWatch.VirtualKey.t()],
          personal_teams: [LangWatch.Team.t()],
          personal_projects: [LangWatch.Project.t()],
          routing_policies_created: [LangWatch.RoutingPolicy.t()],
          routing_policies_updated: [LangWatch.RoutingPolicy.t()],
          gateway_guardrails_created: [LangWatch.GatewayGuardrail.t()],
          gateway_guardrails_updated: [LangWatch.GatewayGuardrail.t()],
          ingestion_sources_created: [LangWatch.IngestionSource.t()],
          anomaly_rules_created: [LangWatch.AnomalyRule.t()],
          pinned_traces: [LangWatch.PinnedTrace.t()],
          trace_edit_overlays_created: [LangWatch.TraceEditOverlay.t()],
          trace_edit_overlays_updated: [LangWatch.TraceEditOverlay.t()]
        }

  schema "User" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:email, :string)
    field(:email_verified, :boolean, default: false, source: :emailVerified)
    field(:signup_confirmation_pending, :boolean, default: false, source: :signupConfirmationPending)
    field(:passkey_signup_claim_hash, :string, source: :passkeySignupClaimHash)
    field(:image, :string)
    field(:pending_sso_setup, :boolean, default: false, source: :pendingSsoSetup)
    field(:user_hash_key, :string, source: :userHashKey)
    field(:two_factor_enabled, :boolean, default: false, source: :twoFactorEnabled)
    field(:last_login_at, LangWatch.PrismaDateTime, source: :lastLoginAt)
    field(:deactivated_at, LangWatch.PrismaDateTime, source: :deactivatedAt)
    field(:last_home_path, :string, source: :lastHomePath)
    field(:traces_explorer_tour_dismissed_at, LangWatch.PrismaDateTime, source: :tracesExplorerTourDismissedAt)
    field(:langy_code_access_preference, :string, source: :langyCodeAccessPreference)
    field(:passkey_nudge_dismissed_at, LangWatch.PrismaDateTime, source: :passkeyNudgeDismissedAt)
    field(:join_offer_dismissed_domains, {:array, :string}, source: :joinOfferDismissedDomains)
    field(:notification_preferences, :map, default: %{}, source: :notificationPreferences)
    field(:password_reset_required_at, LangWatch.PrismaDateTime, source: :passwordResetRequiredAt)
    has_many(:accounts, LangWatch.Account, foreign_key: :user_id, references: :id)
    has_many(:account_credentials, LangWatch.AccountCredential, foreign_key: :user_id, references: :id)
    has_many(:sessions, LangWatch.Session, foreign_key: :user_id, references: :id)
    has_many(:two_factors, LangWatch.TwoFactor, foreign_key: :user_id, references: :id)
    has_many(:passkeys, LangWatch.Passkey, foreign_key: :user_id, references: :id)
    has_many(:team_memberships, LangWatch.TeamUser, foreign_key: :user_id, references: :id)
    has_many(:org_memberships, LangWatch.OrganizationUser, foreign_key: :user_id, references: :id)
    has_many(:scim_user_resources, LangWatch.ScimUserResource, foreign_key: :user_id, references: :id)
    has_many(:annotation, LangWatch.Annotation, foreign_key: :user_id, references: :id)
    has_many(:share_links, LangWatch.ShareLink, foreign_key: :user_id, references: :id)
    has_many(:workflow, LangWatch.Workflow, foreign_key: :published_by_id, references: :id)
    has_many(:workflow_version, LangWatch.WorkflowVersion, foreign_key: :author_id, references: :id)
    has_many(:annotation_queues, LangWatch.AnnotationQueueMembers, foreign_key: :user_id, references: :id)
    has_many(:assigned_queue_items, LangWatch.AnnotationQueueItem, foreign_key: :user_id, references: :id)
    has_many(:created_queue_items, LangWatch.AnnotationQueueItem, foreign_key: :created_by_user_id, references: :id)
    has_many(:llm_prompt_config_versions, LangWatch.LlmPromptConfigVersion, foreign_key: :author_id, references: :id)
    has_many(:scenarios, LangWatch.Scenario, foreign_key: :last_updated_by_id, references: :id)
    has_many(:invite_requests, LangWatch.OrganizationInvite, foreign_key: :requested_by, references: :id)
    has_many(:secrets_created, LangWatch.ProjectSecret, foreign_key: :created_by_id, references: :id)
    has_many(:secrets_updated, LangWatch.ProjectSecret, foreign_key: :updated_by_id, references: :id)
    has_many(:saved_views, LangWatch.SavedView, foreign_key: :user_id, references: :id)
    has_many(:version_tags_created, LangWatch.PromptTagAssignment, foreign_key: :created_by_id, references: :id)
    has_many(:version_tags_updated, LangWatch.PromptTagAssignment, foreign_key: :updated_by_id, references: :id)
    has_many(:prompt_tags_created, LangWatch.PromptTag, foreign_key: :created_by_id, references: :id)
    has_many(:prompt_tags_updated, LangWatch.PromptTag, foreign_key: :updated_by_id, references: :id)
    has_many(:group_memberships, LangWatch.GroupMembership, foreign_key: :user_id, references: :id)
    has_many(:role_bindings, LangWatch.RoleBinding, foreign_key: :user_id, references: :id)
    has_many(:scim_external_ids, LangWatch.ScimExternalId, foreign_key: :user_id, references: :id)
    has_many(:api_keys, LangWatch.ApiKey, foreign_key: :user_id, references: :id)
    has_many(:gateway_budgets_created, LangWatch.GatewayBudget, foreign_key: :created_by_id, references: :id)
    has_many(:principal_virtual_keys, LangWatch.VirtualKey, foreign_key: :principal_user_id, references: :id)
    has_many(:personal_teams, LangWatch.Team, foreign_key: :owner_user_id, references: :id)
    has_many(:personal_projects, LangWatch.Project, foreign_key: :owner_user_id, references: :id)
    has_many(:routing_policies_created, LangWatch.RoutingPolicy, foreign_key: :created_by_id, references: :id)
    has_many(:routing_policies_updated, LangWatch.RoutingPolicy, foreign_key: :updated_by_id, references: :id)
    has_many(:gateway_guardrails_created, LangWatch.GatewayGuardrail, foreign_key: :created_by_id, references: :id)
    has_many(:gateway_guardrails_updated, LangWatch.GatewayGuardrail, foreign_key: :updated_by_id, references: :id)
    has_many(:ingestion_sources_created, LangWatch.IngestionSource, foreign_key: :created_by_id, references: :id)
    has_many(:anomaly_rules_created, LangWatch.AnomalyRule, foreign_key: :created_by_id, references: :id)
    has_many(:pinned_traces, LangWatch.PinnedTrace, foreign_key: :user_id, references: :id)
    has_many(:trace_edit_overlays_created, LangWatch.TraceEditOverlay, foreign_key: :created_by_id, references: :id)
    has_many(:trace_edit_overlays_updated, LangWatch.TraceEditOverlay, foreign_key: :updated_by_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end