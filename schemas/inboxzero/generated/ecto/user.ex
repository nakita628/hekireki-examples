defmodule InboxZero.User do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: InboxZero.PrismaDateTime, autogenerate: {InboxZero.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t() | nil,
          email: String.t(),
          email_verified: boolean() | nil,
          image: String.t() | nil,
          completed_onboarding_at: DateTime.t() | nil,
          signup_intent: atom() | nil,
          onboarding_answers: map() | nil,
          last_login: DateTime.t() | nil,
          utms: map() | nil,
          error_messages: map() | nil,
          announcement_dismissed_at: DateTime.t() | nil,
          dismissed_hints: [String.t()],
          survey_features: [String.t()],
          survey_role: String.t() | nil,
          survey_goal: String.t() | nil,
          survey_company_size: integer() | nil,
          survey_source: String.t() | nil,
          survey_improvements: String.t() | nil,
          mcp_server_enabled: boolean(),
          mcp_token_version: integer(),
          email_otp_enabled: boolean(),
          email_otp_version: integer(),
          ai_provider: String.t() | nil,
          ai_model: String.t() | nil,
          ai_api_key: String.t() | nil,
          decision_model_enabled: boolean() | nil,
          webhook_secret: String.t() | nil,
          referral_code: String.t() | nil,
          scim_access_disabled: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          premium: InboxZero.Premium.t() | nil,
          premium_admin: InboxZero.Premium.t() | nil,
          referral_received: InboxZero.Referral.t() | nil,
          scim_subject: InboxZero.ScimSubject.t() | nil,
          accounts: [InboxZero.Account.t()],
          sessions: [InboxZero.Session.t()],
          api_keys: [InboxZero.ApiKey.t()],
          email_accounts: [InboxZero.EmailAccount.t()],
          referrals_made: [InboxZero.Referral.t()],
          mobile_push_tokens: [InboxZero.MobilePushToken.t()],
          oauth_client_records: [InboxZero.OauthClient.t()],
          oauth_refresh_token_records: [InboxZero.OauthRefreshToken.t()],
          oauth_access_token_records: [InboxZero.OauthAccessToken.t()],
          oauth_consent_records: [InboxZero.OauthConsent.t()],
          sso_providers: [InboxZero.SsoProvider.t()],
          scim_identity_tombstones: [InboxZero.ScimIdentityTombstone.t()],
          scim_users: [InboxZero.ScimUser.t()],
          scim_projection_grants: [InboxZero.ScimProjectionGrant.t()],
          scim_identity_links: [InboxZero.ScimIdentityLink.t()]
        }

  schema "User" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:email, :string)
    field(:email_verified, :boolean, default: false, source: :emailVerified)
    field(:image, :string)
    field(:completed_onboarding_at, InboxZero.PrismaDateTime, source: :completedOnboardingAt)
    field(:signup_intent, Ecto.Enum, values: [:AI_ASSISTANT, :MAIL_CLIENT, :BOTH], source: :signupIntent)
    field(:onboarding_answers, :map, source: :onboardingAnswers)
    field(:last_login, InboxZero.PrismaDateTime, source: :lastLogin)
    field(:utms, :map)
    field(:error_messages, :map, source: :errorMessages)
    field(:announcement_dismissed_at, InboxZero.PrismaDateTime, source: :announcementDismissedAt)
    field(:dismissed_hints, {:array, :string}, source: :dismissedHints)
    field(:survey_features, {:array, :string}, source: :surveyFeatures)
    field(:survey_role, :string, source: :surveyRole)
    field(:survey_goal, :string, source: :surveyGoal)
    field(:survey_company_size, :integer, source: :surveyCompanySize)
    field(:survey_source, :string, source: :surveySource)
    field(:survey_improvements, :string, source: :surveyImprovements)
    field(:mcp_server_enabled, :boolean, default: false, source: :mcpServerEnabled)
    field(:mcp_token_version, :integer, default: 0, source: :mcpTokenVersion)
    field(:email_otp_enabled, :boolean, default: false, source: :emailOtpEnabled)
    field(:email_otp_version, :integer, default: 0, source: :emailOtpVersion)
    field(:ai_provider, :string, source: :aiProvider)
    field(:ai_model, :string, source: :aiModel)
    field(:ai_api_key, :string, source: :aiApiKey)
    field(:decision_model_enabled, :boolean, source: :decisionModelEnabled)
    field(:webhook_secret, :string, source: :webhookSecret)
    field(:referral_code, :string, source: :referralCode)
    field(:scim_access_disabled, :boolean, default: false, source: :scimAccessDisabled)
    field(:premium_id, :string, source: :premiumId)
    field(:premium_admin_id, :string, source: :premiumAdminId)
    belongs_to(:premium, InboxZero.Premium, foreign_key: :premium_id, define_field: false, type: :string)
    belongs_to(:premium_admin, InboxZero.Premium, foreign_key: :premium_admin_id, define_field: false, type: :string)
    has_one(:referral_received, InboxZero.Referral, foreign_key: :referred_user_id, references: :id)
    has_one(:scim_subject, InboxZero.ScimSubject, foreign_key: :user_id, references: :id)
    has_many(:accounts, InboxZero.Account, foreign_key: :user_id, references: :id)
    has_many(:sessions, InboxZero.Session, foreign_key: :user_id, references: :id)
    has_many(:api_keys, InboxZero.ApiKey, foreign_key: :user_id, references: :id)
    has_many(:email_accounts, InboxZero.EmailAccount, foreign_key: :user_id, references: :id)
    has_many(:referrals_made, InboxZero.Referral, foreign_key: :referrer_user_id, references: :id)
    has_many(:mobile_push_tokens, InboxZero.MobilePushToken, foreign_key: :user_id, references: :id)
    has_many(:oauth_client_records, InboxZero.OauthClient, foreign_key: :user_id, references: :id)
    has_many(:oauth_refresh_token_records, InboxZero.OauthRefreshToken, foreign_key: :user_id, references: :id)
    has_many(:oauth_access_token_records, InboxZero.OauthAccessToken, foreign_key: :user_id, references: :id)
    has_many(:oauth_consent_records, InboxZero.OauthConsent, foreign_key: :user_id, references: :id)
    has_many(:sso_providers, InboxZero.SsoProvider, foreign_key: :user_id, references: :id)
    has_many(:scim_identity_tombstones, InboxZero.ScimIdentityTombstone, foreign_key: :user_id, references: :id)
    has_many(:scim_users, InboxZero.ScimUser, foreign_key: :user_id, references: :id)
    has_many(:scim_projection_grants, InboxZero.ScimProjectionGrant, foreign_key: :user_id, references: :id)
    has_many(:scim_identity_links, InboxZero.ScimIdentityLink, foreign_key: :user_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end