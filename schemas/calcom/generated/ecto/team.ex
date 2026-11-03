defmodule Calcom.Team do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: Calcom.PrismaDateTime, autogenerate: {Calcom.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          name: String.t(),
          slug: String.t() | nil,
          logo_url: String.t() | nil,
          cal_video_logo: String.t() | nil,
          app_logo: String.t() | nil,
          app_icon_logo: String.t() | nil,
          bio: String.t() | nil,
          hide_branding: boolean(),
          hide_team_profile_link: boolean(),
          is_private: boolean(),
          hide_book_ateam_member: boolean(),
          metadata: map() | nil,
          theme: String.t() | nil,
          rr_reset_interval: atom() | nil,
          rr_timestamp_basis: atom(),
          brand_color: String.t() | nil,
          dark_brand_color: String.t() | nil,
          banner_url: String.t() | nil,
          time_format: integer() | nil,
          time_zone: String.t(),
          week_start: String.t(),
          is_organization: boolean(),
          pending_payment: boolean(),
          is_platform: boolean(),
          sms_lock_state: atom(),
          sms_lock_reviewed_by_admin: boolean(),
          booking_limits: map() | nil,
          include_managed_events_in_limits: boolean(),
          auto_opt_in_features: boolean(),
          inserted_at: DateTime.t(),
          parent: Calcom.Team.t() | nil,
          created_by_oauth_client: Calcom.PlatformOAuthClient.t() | nil,
          organization_settings: Calcom.OrganizationSettings.t() | nil,
          platform_billing: Calcom.PlatformBilling.t() | nil,
          credit_balance: Calcom.CreditBalance.t() | nil,
          organization_onboarding: Calcom.OrganizationOnboarding.t() | nil,
          managed_organization: Calcom.ManagedOrganization.t() | nil,
          team_billing: Calcom.TeamBilling.t() | nil,
          organization_billing: Calcom.OrganizationBilling.t() | nil,
          members: [Calcom.Membership.t()],
          event_types: [Calcom.EventType.t()],
          verified_numbers: [Calcom.VerifiedNumber.t()],
          verified_emails: [Calcom.VerifiedEmail.t()],
          children: [Calcom.Team.t()],
          org_users: [Calcom.User.t()],
          invite_tokens: [Calcom.VerificationToken.t()],
          webhooks: [Calcom.Webhook.t()],
          api_keys: [Calcom.ApiKey.t()],
          credentials: [Calcom.Credential.t()],
          access_codes: [Calcom.AccessCode.t()],
          instant_meeting_tokens: [Calcom.InstantMeetingToken.t()],
          org_profiles: [Calcom.Profile.t()],
          dsync_team_group_mapping: [Calcom.DSyncTeamGroupMapping.t()],
          platform_oauth_client: [Calcom.PlatformOAuthClient.t()],
          attributes: [Calcom.Attribute.t()],
          delegation_credentials: [Calcom.DelegationCredential.t()],
          roles: [Calcom.Role.t()],
          cal_ai_phone_numbers: [Calcom.CalAiPhoneNumber.t()],
          agents: [Calcom.Agent.t()],
          booking_reports: [Calcom.BookingReport.t()],
          wrong_assignment_reports: [Calcom.WrongAssignmentReport.t()],
          features: [Calcom.TeamFeatures.t()],
          internal_note_presets: [Calcom.InternalNotePreset.t()],
          managed_organizations: [Calcom.ManagedOrganization.t()],
          filter_segments: [Calcom.FilterSegment.t()],
          integration_attribute_syncs: [Calcom.IntegrationAttributeSync.t()],
          seat_change_logs: [Calcom.SeatChangeLog.t()],
          monthly_prorations: [Calcom.MonthlyProration.t()]
        }

  schema "Team" do
    field(:name, :string)
    field(:slug, :string)
    field(:logo_url, :string, source: :logoUrl)
    field(:cal_video_logo, :string, source: :calVideoLogo)
    field(:app_logo, :string, source: :appLogo)
    field(:app_icon_logo, :string, source: :appIconLogo)
    field(:bio, :string)
    field(:hide_branding, :boolean, default: false, source: :hideBranding)
    field(:hide_team_profile_link, :boolean, default: false, source: :hideTeamProfileLink)
    field(:is_private, :boolean, default: false, source: :isPrivate)
    field(:hide_book_ateam_member, :boolean, default: false, source: :hideBookATeamMember)
    field(:metadata, :map)
    field(:theme, :string)
    field(:rr_reset_interval, Ecto.Enum, values: [:MONTH, :DAY], default: :MONTH, source: :rrResetInterval)
    field(:rr_timestamp_basis, Ecto.Enum, values: [:CREATED_AT, :START_TIME], default: :CREATED_AT, source: :rrTimestampBasis)
    field(:brand_color, :string, source: :brandColor)
    field(:dark_brand_color, :string, source: :darkBrandColor)
    field(:banner_url, :string, source: :bannerUrl)
    field(:time_format, :integer, source: :timeFormat)
    field(:time_zone, :string, default: "Europe/London", source: :timeZone)
    field(:week_start, :string, default: "Sunday", source: :weekStart)
    field(:is_organization, :boolean, default: false, source: :isOrganization)
    field(:pending_payment, :boolean, default: false, source: :pendingPayment)
    field(:is_platform, :boolean, default: false, source: :isPlatform)
    field(:sms_lock_state, Ecto.Enum, values: [:LOCKED, :UNLOCKED, :REVIEW_NEEDED], default: :UNLOCKED, source: :smsLockState)
    field(:sms_lock_reviewed_by_admin, :boolean, default: false, source: :smsLockReviewedByAdmin)
    field(:booking_limits, :map, source: :bookingLimits)
    field(:include_managed_events_in_limits, :boolean, default: false, source: :includeManagedEventsInLimits)
    field(:auto_opt_in_features, :boolean, default: false, source: :autoOptInFeatures)
    field(:parent_id, :id, source: :parentId)
    field(:created_by_oauth_client_id, :string, source: :createdByOAuthClientId)
    belongs_to(:parent, Calcom.Team, foreign_key: :parent_id, define_field: false)
    belongs_to(:created_by_oauth_client, Calcom.PlatformOAuthClient, foreign_key: :created_by_oauth_client_id, define_field: false, type: :string)
    has_one(:organization_settings, Calcom.OrganizationSettings, foreign_key: :organization_id)
    has_one(:platform_billing, Calcom.PlatformBilling, foreign_key: :id)
    has_one(:credit_balance, Calcom.CreditBalance, foreign_key: :team_id)
    has_one(:organization_onboarding, Calcom.OrganizationOnboarding, foreign_key: :organization_id)
    has_one(:managed_organization, Calcom.ManagedOrganization, foreign_key: :managed_organization_id)
    has_one(:team_billing, Calcom.TeamBilling, foreign_key: :team_id)
    has_one(:organization_billing, Calcom.OrganizationBilling, foreign_key: :team_id)
    has_many(:members, Calcom.Membership, foreign_key: :team_id)
    has_many(:event_types, Calcom.EventType, foreign_key: :team_id)
    has_many(:verified_numbers, Calcom.VerifiedNumber, foreign_key: :team_id)
    has_many(:verified_emails, Calcom.VerifiedEmail, foreign_key: :team_id)
    has_many(:children, Calcom.Team, foreign_key: :parent_id)
    has_many(:org_users, Calcom.User, foreign_key: :organization_id)
    has_many(:invite_tokens, Calcom.VerificationToken, foreign_key: :team_id)
    has_many(:webhooks, Calcom.Webhook, foreign_key: :team_id)
    has_many(:api_keys, Calcom.ApiKey, foreign_key: :team_id)
    has_many(:credentials, Calcom.Credential, foreign_key: :team_id)
    has_many(:access_codes, Calcom.AccessCode, foreign_key: :team_id)
    has_many(:instant_meeting_tokens, Calcom.InstantMeetingToken, foreign_key: :team_id)
    has_many(:org_profiles, Calcom.Profile, foreign_key: :organization_id)
    has_many(:dsync_team_group_mapping, Calcom.DSyncTeamGroupMapping, foreign_key: :team_id)
    has_many(:platform_oauth_client, Calcom.PlatformOAuthClient, foreign_key: :organization_id)
    has_many(:attributes, Calcom.Attribute, foreign_key: :team_id)
    has_many(:delegation_credentials, Calcom.DelegationCredential, foreign_key: :organization_id)
    has_many(:roles, Calcom.Role, foreign_key: :team_id)
    has_many(:cal_ai_phone_numbers, Calcom.CalAiPhoneNumber, foreign_key: :team_id)
    has_many(:agents, Calcom.Agent, foreign_key: :team_id)
    has_many(:booking_reports, Calcom.BookingReport, foreign_key: :organization_id)
    has_many(:wrong_assignment_reports, Calcom.WrongAssignmentReport, foreign_key: :team_id)
    has_many(:features, Calcom.TeamFeatures, foreign_key: :team_id)
    has_many(:internal_note_presets, Calcom.InternalNotePreset, foreign_key: :team_id)
    has_many(:managed_organizations, Calcom.ManagedOrganization, foreign_key: :manager_organization_id)
    has_many(:filter_segments, Calcom.FilterSegment, foreign_key: :team_id)
    has_many(:integration_attribute_syncs, Calcom.IntegrationAttributeSync, foreign_key: :organization_id)
    has_many(:seat_change_logs, Calcom.SeatChangeLog, foreign_key: :team_id)
    has_many(:monthly_prorations, Calcom.MonthlyProration, foreign_key: :team_id)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end