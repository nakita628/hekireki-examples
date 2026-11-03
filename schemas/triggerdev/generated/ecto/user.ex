defmodule Trigger.User do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          email: String.t(),
          pending_email: String.t() | nil,
          authentication_method: atom(),
          authentication_profile: map() | nil,
          authentication_extra_params: map() | nil,
          auth_identifier: String.t() | nil,
          display_name: String.t() | nil,
          name: String.t() | nil,
          avatar_url: String.t() | nil,
          admin: boolean(),
          dashboard_preferences: map() | nil,
          is_on_cloud_waitlist: boolean(),
          feature_cloud: boolean(),
          is_on_hosted_repo_waitlist: boolean(),
          marketing_emails: boolean(),
          confirmed_basic_details: boolean(),
          referral_source: String.t() | nil,
          onboarding_data: map() | nil,
          mfa_enabled_at: DateTime.t() | nil,
          mfa_last_used_code: String.t() | nil,
          session_duration: integer(),
          next_session_end: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          mfa_secret_reference: Trigger.SecretReference.t() | nil,
          invitation_code: Trigger.InvitationCode.t() | nil,
          org_memberships: [Trigger.OrgMember.t()],
          sent_invites: [Trigger.OrgMemberInvite.t()],
          personal_access_tokens: [Trigger.PersonalAccessToken.t()],
          created_api_keys: [Trigger.ApiKey.t()],
          deployments: [Trigger.WorkerDeployment.t()],
          backup_codes: [Trigger.MfaBackupCode.t()],
          bulk_actions: [Trigger.BulkActionGroup.t()],
          impersonations_performed: [Trigger.ImpersonationAuditLog.t()],
          impersonations_received: [Trigger.ImpersonationAuditLog.t()],
          support_access_requests_made: [Trigger.SupportAccessRequest.t()],
          support_access_requests_approved: [Trigger.SupportAccessRequest.t()],
          customer_queries: [Trigger.CustomerQuery.t()],
          metrics_dashboards: [Trigger.MetricsDashboard.t()],
          platform_notifications: [Trigger.PlatformNotification.t()],
          platform_notification_interactions: [Trigger.PlatformNotificationInteraction.t()]
        }

  schema "User" do
    field(:id, :string, primary_key: true)
    field(:email, :string)
    field(:pending_email, :string, source: :pendingEmail)
    field(:authentication_method, Ecto.Enum, values: [:GITHUB, :MAGIC_LINK, :GOOGLE, :SSO], source: :authenticationMethod)
    field(:authentication_profile, :map, source: :authenticationProfile)
    field(:authentication_extra_params, :map, source: :authenticationExtraParams)
    field(:auth_identifier, :string, source: :authIdentifier)
    field(:display_name, :string, source: :displayName)
    field(:name, :string)
    field(:avatar_url, :string, source: :avatarUrl)
    field(:admin, :boolean, default: false)
    field(:dashboard_preferences, :map, source: :dashboardPreferences)
    field(:is_on_cloud_waitlist, :boolean, default: false, source: :isOnCloudWaitlist)
    field(:feature_cloud, :boolean, default: false, source: :featureCloud)
    field(:is_on_hosted_repo_waitlist, :boolean, default: false, source: :isOnHostedRepoWaitlist)
    field(:marketing_emails, :boolean, default: true, source: :marketingEmails)
    field(:confirmed_basic_details, :boolean, default: false, source: :confirmedBasicDetails)
    field(:referral_source, :string, source: :referralSource)
    field(:onboarding_data, :map, source: :onboardingData)
    field(:mfa_enabled_at, Trigger.PrismaDateTime, source: :mfaEnabledAt)
    field(:mfa_last_used_code, :string, source: :mfaLastUsedCode)
    field(:session_duration, :integer, default: 31556952, source: :sessionDuration)
    field(:next_session_end, Trigger.PrismaDateTime, source: :nextSessionEnd)
    field(:mfa_secret_reference_id, :string, source: :mfaSecretReferenceId)
    field(:invitation_code_id, :string, source: :invitationCodeId)
    belongs_to(:mfa_secret_reference, Trigger.SecretReference, foreign_key: :mfa_secret_reference_id, define_field: false, type: :string)
    belongs_to(:invitation_code, Trigger.InvitationCode, foreign_key: :invitation_code_id, define_field: false, type: :string)
    has_many(:org_memberships, Trigger.OrgMember, foreign_key: :user_id)
    has_many(:sent_invites, Trigger.OrgMemberInvite, foreign_key: :inviter_id)
    has_many(:personal_access_tokens, Trigger.PersonalAccessToken, foreign_key: :user_id)
    has_many(:created_api_keys, Trigger.ApiKey, foreign_key: :created_by_user_id)
    has_many(:deployments, Trigger.WorkerDeployment, foreign_key: :triggered_by_id)
    has_many(:backup_codes, Trigger.MfaBackupCode, foreign_key: :user_id)
    has_many(:bulk_actions, Trigger.BulkActionGroup, foreign_key: :user_id)
    has_many(:impersonations_performed, Trigger.ImpersonationAuditLog, foreign_key: :admin_id)
    has_many(:impersonations_received, Trigger.ImpersonationAuditLog, foreign_key: :target_id)
    has_many(:support_access_requests_made, Trigger.SupportAccessRequest, foreign_key: :requested_by_id)
    has_many(:support_access_requests_approved, Trigger.SupportAccessRequest, foreign_key: :approved_by_id)
    has_many(:customer_queries, Trigger.CustomerQuery, foreign_key: :user_id)
    has_many(:metrics_dashboards, Trigger.MetricsDashboard, foreign_key: :owner_id)
    has_many(:platform_notifications, Trigger.PlatformNotification, foreign_key: :user_id)
    has_many(:platform_notification_interactions, Trigger.PlatformNotificationInteraction, foreign_key: :user_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end