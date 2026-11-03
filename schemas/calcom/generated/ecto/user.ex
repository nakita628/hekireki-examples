defmodule Calcom.User do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          uuid: String.t(),
          username: String.t() | nil,
          name: String.t() | nil,
          email: String.t(),
          email_verified: DateTime.t() | nil,
          bio: String.t() | nil,
          avatar_url: String.t() | nil,
          time_zone: String.t(),
          week_start: String.t(),
          buffer_time: integer(),
          hide_branding: boolean(),
          theme: String.t() | nil,
          app_theme: String.t() | nil,
          created_date: DateTime.t(),
          trial_ends_at: DateTime.t() | nil,
          last_active_at: DateTime.t() | nil,
          default_schedule_id: integer() | nil,
          completed_onboarding: boolean(),
          locale: String.t() | nil,
          time_format: integer() | nil,
          two_factor_secret: String.t() | nil,
          two_factor_enabled: boolean(),
          backup_codes: String.t() | nil,
          identity_provider: atom(),
          identity_provider_id: String.t() | nil,
          invited_to: integer() | nil,
          brand_color: String.t() | nil,
          dark_brand_color: String.t() | nil,
          allow_dynamic_booking: boolean() | nil,
          allow_seoindexing: boolean() | nil,
          receive_monthly_digest_email: boolean() | nil,
          requires_booker_email_verification: boolean() | nil,
          metadata: map() | nil,
          verified: boolean() | nil,
          role: atom(),
          locked: boolean(),
          is_platform_managed: boolean(),
          sms_lock_state: atom(),
          sms_lock_reviewed_by_admin: boolean(),
          referral_link_id: String.t() | nil,
          creation_source: atom() | nil,
          auto_opt_in_features: boolean(),
          organization: Calcom.Team.t() | nil,
          moved_to_profile: Calcom.Profile.t() | nil,
          password: Calcom.UserPassword.t() | nil,
          destination_calendar: Calcom.DestinationCalendar.t() | nil,
          credit_balance: Calcom.CreditBalance.t() | nil,
          holiday_settings: Calcom.UserHolidaySettings.t() | nil,
          travel_schedules: [Calcom.TravelSchedule.t()],
          credentials: [Calcom.Credential.t()],
          teams: [Calcom.Membership.t()],
          bookings: [Calcom.Booking.t()],
          schedules: [Calcom.Schedule.t()],
          selected_calendars: [Calcom.SelectedCalendar.t()],
          availability: [Calcom.Availability.t()],
          webhooks: [Calcom.Webhook.t()],
          api_keys: [Calcom.ApiKey.t()],
          accounts: [Calcom.Account.t()],
          sessions: [Calcom.Session.t()],
          feedback: [Calcom.Feedback.t()],
          owned_event_types: [Calcom.EventType.t()],
          verified_numbers: [Calcom.VerifiedNumber.t()],
          verified_emails: [Calcom.VerifiedEmail.t()],
          hosts: [Calcom.Host.t()],
          access_codes: [Calcom.AccessCode.t()],
          booking_redirects: [Calcom.OutOfOfficeEntry.t()],
          booking_redirects_to: [Calcom.OutOfOfficeEntry.t()],
          access_token: [Calcom.AccessToken.t()],
          refresh_token: [Calcom.RefreshToken.t()],
          platform_authorization_token: [Calcom.PlatformAuthorizationToken.t()],
          profiles: [Calcom.Profile.t()],
          secondary_emails: [Calcom.SecondaryEmail.t()],
          out_of_office_reasons: [Calcom.OutOfOfficeReason.t()],
          notifications_subscriptions: [Calcom.NotificationsSubscriptions.t()],
          features: [Calcom.UserFeatures.t()],
          reassigned_bookings: [Calcom.Booking.t()],
          created_attribute_to_users: [Calcom.AttributeToUser.t()],
          updated_attribute_to_users: [Calcom.AttributeToUser.t()],
          created_translations: [Calcom.EventTypeTranslation.t()],
          updated_translations: [Calcom.EventTypeTranslation.t()],
          reported_bookings: [Calcom.BookingReport.t()],
          wrong_assignment_reports: [Calcom.WrongAssignmentReport.t()],
          reviewed_wrong_assignment_reports: [Calcom.WrongAssignmentReport.t()],
          booking_internal_note: [Calcom.BookingInternalNote.t()],
          created_organization_onboardings: [Calcom.OrganizationOnboarding.t()],
          filter_segments: [Calcom.FilterSegment.t()],
          filter_segment_preferences: [Calcom.UserFilterSegmentPreference.t()],
          cal_ai_phone_numbers: [Calcom.CalAiPhoneNumber.t()],
          agents: [Calcom.Agent.t()],
          o_auth_clients: [Calcom.OAuthClient.t()],
          event_types: [Calcom.EventType.t()],
          platform_oauth_clients: [Calcom.PlatformOAuthClient.t()]
        }

  schema "users" do
    field(:uuid, :string, autogenerate: {Ecto.UUID, :generate, []})
    field(:username, :string)
    field(:name, :string)
    field(:email, :string)
    field(:email_verified, Calcom.PrismaDateTime, source: :emailVerified)
    field(:bio, :string)
    field(:avatar_url, :string, source: :avatarUrl)
    field(:time_zone, :string, default: "Europe/London", source: :timeZone)
    field(:week_start, :string, default: "Sunday", source: :weekStart)
    field(:buffer_time, :integer, default: 0, source: :bufferTime)
    field(:hide_branding, :boolean, default: false, source: :hideBranding)
    field(:theme, :string)
    field(:app_theme, :string, source: :appTheme)
    field(:created_date, Calcom.PrismaDateTime, autogenerate: true, source: :created)
    field(:trial_ends_at, Calcom.PrismaDateTime, source: :trialEndsAt)
    field(:last_active_at, Calcom.PrismaDateTime, source: :lastActiveAt)
    field(:default_schedule_id, :integer, source: :defaultScheduleId)
    field(:completed_onboarding, :boolean, default: false, source: :completedOnboarding)
    field(:locale, :string)
    field(:time_format, :integer, default: 12, source: :timeFormat)
    field(:two_factor_secret, :string, source: :twoFactorSecret)
    field(:two_factor_enabled, :boolean, default: false, source: :twoFactorEnabled)
    field(:backup_codes, :string, source: :backupCodes)
    field(:identity_provider, Ecto.Enum, values: [:CAL, :GOOGLE, :SAML, :AZUREAD], default: :CAL, source: :identityProvider)
    field(:identity_provider_id, :string, source: :identityProviderId)
    field(:invited_to, :integer, source: :invitedTo)
    field(:brand_color, :string, source: :brandColor)
    field(:dark_brand_color, :string, source: :darkBrandColor)
    field(:allow_dynamic_booking, :boolean, default: true, source: :allowDynamicBooking)
    field(:allow_seoindexing, :boolean, default: true, source: :allowSEOIndexing)
    field(:receive_monthly_digest_email, :boolean, default: true, source: :receiveMonthlyDigestEmail)
    field(:requires_booker_email_verification, :boolean, default: false, source: :requiresBookerEmailVerification)
    field(:metadata, :map)
    field(:verified, :boolean, default: false)
    field(:role, Ecto.Enum, values: [:USER, :ADMIN], default: :USER)
    field(:locked, :boolean, default: false)
    field(:is_platform_managed, :boolean, default: false, source: :isPlatformManaged)
    field(:sms_lock_state, Ecto.Enum, values: [:LOCKED, :UNLOCKED, :REVIEW_NEEDED], default: :UNLOCKED, source: :smsLockState)
    field(:sms_lock_reviewed_by_admin, :boolean, default: false, source: :smsLockReviewedByAdmin)
    field(:referral_link_id, :string, source: :referralLinkId)
    field(:creation_source, Ecto.Enum, values: [API_V1: "api_v1", API_V2: "api_v2", WEBAPP: "webapp"], source: :creationSource)
    field(:auto_opt_in_features, :boolean, default: false, source: :autoOptInFeatures)
    field(:organization_id, :id, source: :organizationId)
    field(:moved_to_profile_id, :id, source: :movedToProfileId)
    belongs_to(:organization, Calcom.Team, foreign_key: :organization_id, define_field: false)
    belongs_to(:moved_to_profile, Calcom.Profile, foreign_key: :moved_to_profile_id, define_field: false)
    has_one(:password, Calcom.UserPassword, foreign_key: :user_id)
    has_one(:destination_calendar, Calcom.DestinationCalendar, foreign_key: :user_id)
    has_one(:credit_balance, Calcom.CreditBalance, foreign_key: :user_id)
    has_one(:holiday_settings, Calcom.UserHolidaySettings, foreign_key: :user_id)
    has_many(:travel_schedules, Calcom.TravelSchedule, foreign_key: :user_id)
    has_many(:credentials, Calcom.Credential, foreign_key: :user_id)
    has_many(:teams, Calcom.Membership, foreign_key: :user_id)
    has_many(:bookings, Calcom.Booking, foreign_key: :user_id)
    has_many(:schedules, Calcom.Schedule, foreign_key: :user_id)
    has_many(:selected_calendars, Calcom.SelectedCalendar, foreign_key: :user_id)
    has_many(:availability, Calcom.Availability, foreign_key: :user_id)
    has_many(:webhooks, Calcom.Webhook, foreign_key: :user_id)
    has_many(:api_keys, Calcom.ApiKey, foreign_key: :user_id)
    has_many(:accounts, Calcom.Account, foreign_key: :user_id)
    has_many(:sessions, Calcom.Session, foreign_key: :user_id)
    has_many(:feedback, Calcom.Feedback, foreign_key: :user_id)
    has_many(:owned_event_types, Calcom.EventType, foreign_key: :user_id)
    has_many(:verified_numbers, Calcom.VerifiedNumber, foreign_key: :user_id)
    has_many(:verified_emails, Calcom.VerifiedEmail, foreign_key: :user_id)
    has_many(:hosts, Calcom.Host, foreign_key: :user_id)
    has_many(:access_codes, Calcom.AccessCode, foreign_key: :user_id)
    has_many(:booking_redirects, Calcom.OutOfOfficeEntry, foreign_key: :user_id)
    has_many(:booking_redirects_to, Calcom.OutOfOfficeEntry, foreign_key: :to_user_id)
    has_many(:access_token, Calcom.AccessToken, foreign_key: :user_id)
    has_many(:refresh_token, Calcom.RefreshToken, foreign_key: :user_id)
    has_many(:platform_authorization_token, Calcom.PlatformAuthorizationToken, foreign_key: :user_id)
    has_many(:profiles, Calcom.Profile, foreign_key: :user_id)
    has_many(:secondary_emails, Calcom.SecondaryEmail, foreign_key: :user_id)
    has_many(:out_of_office_reasons, Calcom.OutOfOfficeReason, foreign_key: :user_id)
    has_many(:notifications_subscriptions, Calcom.NotificationsSubscriptions, foreign_key: :user_id)
    has_many(:features, Calcom.UserFeatures, foreign_key: :user_id)
    has_many(:reassigned_bookings, Calcom.Booking, foreign_key: :reassign_by_id)
    has_many(:created_attribute_to_users, Calcom.AttributeToUser, foreign_key: :created_by_id)
    has_many(:updated_attribute_to_users, Calcom.AttributeToUser, foreign_key: :updated_by_id)
    has_many(:created_translations, Calcom.EventTypeTranslation, foreign_key: :created_by)
    has_many(:updated_translations, Calcom.EventTypeTranslation, foreign_key: :updated_by)
    has_many(:reported_bookings, Calcom.BookingReport, foreign_key: :reported_by_id)
    has_many(:wrong_assignment_reports, Calcom.WrongAssignmentReport, foreign_key: :reported_by_id)
    has_many(:reviewed_wrong_assignment_reports, Calcom.WrongAssignmentReport, foreign_key: :reviewed_by_id)
    has_many(:booking_internal_note, Calcom.BookingInternalNote, foreign_key: :created_by_id)
    has_many(:created_organization_onboardings, Calcom.OrganizationOnboarding, foreign_key: :created_by_id)
    has_many(:filter_segments, Calcom.FilterSegment, foreign_key: :user_id)
    has_many(:filter_segment_preferences, Calcom.UserFilterSegmentPreference, foreign_key: :user_id)
    has_many(:cal_ai_phone_numbers, Calcom.CalAiPhoneNumber, foreign_key: :user_id)
    has_many(:agents, Calcom.Agent, foreign_key: :user_id)
    has_many(:o_auth_clients, Calcom.OAuthClient, foreign_key: :user_id)
    many_to_many(:event_types, Calcom.EventType, join_through: "_user_eventtype", join_keys: [B: :id, A: :id])
    many_to_many(:platform_oauth_clients, Calcom.PlatformOAuthClient, join_through: "_PlatformOAuthClientToUser", join_keys: [B: :id, A: :id])
  end
end