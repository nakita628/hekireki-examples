class User < ApplicationRecord
  attribute :uuid, default: -> { SecureRandom.uuid }
  attribute :timeZone, default: "Europe/London"
  attribute :weekStart, default: "Sunday"
  attribute :bufferTime, default: 0
  attribute :hideBranding, default: false
  attribute :completedOnboarding, default: false
  attribute :timeFormat, default: 12
  attribute :twoFactorEnabled, default: false
  attribute :allowDynamicBooking, default: true
  attribute :allowSEOIndexing, default: true
  attribute :receiveMonthlyDigestEmail, default: true
  attribute :requiresBookerEmailVerification, default: false
  attribute :verified, default: false
  attribute :locked, default: false
  attribute :isPlatformManaged, default: false
  attribute :smsLockReviewedByAdmin, default: false
  attribute :autoOptInFeatures, default: false

  enum :identityProvider, { cal: "CAL", google: "GOOGLE", saml: "SAML", azuread: "AZUREAD" }, default: :cal,
                                                                                              validate: true
  enum :role, { user: "USER", admin: "ADMIN" }, default: :user, validate: true
  enum :smsLockState, { locked: "LOCKED", unlocked: "UNLOCKED", review_needed: "REVIEW_NEEDED" }, default: :unlocked,
                                                                                                  validate: true
  enum :creationSource, { api_v1: "api_v1", api_v2: "api_v2", webapp: "webapp" }, validate: { allow_nil: true }

  validates :uuid, uniqueness: true
  validates :username, uniqueness: { scope: :email, allow_nil: true }
  validates :username,
            uniqueness: { scope: :organizationId, conditions: -> { where.not(organizationId: nil) }, allow_nil: true }
  validates :email, presence: true, uniqueness: true
  validates :email, uniqueness: true
  validates :movedToProfileId, uniqueness: { allow_nil: true }
  validates :movedToProfileId, uniqueness: { allow_nil: true }

  belongs_to :organization, class_name: "Team", foreign_key: "organizationId", optional: true, inverse_of: :org_users
  belongs_to :moved_to_profile, class_name: "Profile", foreign_key: "movedToProfileId", optional: true,
                                inverse_of: :moved_from_user
  has_one :password, class_name: "UserPassword", foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_one :destination_calendar, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_one :credit_balance, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_one :holiday_settings, class_name: "UserHolidaySettings", foreign_key: "userId", inverse_of: :user,
                             dependent: :destroy
  has_many :travel_schedules, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :credentials, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :teams, class_name: "Membership", foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :bookings, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :schedules, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :selected_calendars, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :availability, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :webhooks, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :api_keys, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :accounts, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :sessions, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :feedback, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :owned_event_types, class_name: "EventType", foreign_key: "userId", inverse_of: :owner, dependent: :destroy
  has_many :verified_numbers, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :verified_emails, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :hosts, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :access_codes, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :booking_redirects, class_name: "OutOfOfficeEntry", foreign_key: "userId", inverse_of: :user,
                               dependent: :destroy
  has_many :booking_redirects_to, class_name: "OutOfOfficeEntry", foreign_key: "toUserId", inverse_of: :to_user,
                                  dependent: :destroy
  has_many :access_token, foreign_key: "userId", inverse_of: :owner, dependent: :destroy
  has_many :refresh_token, foreign_key: "userId", inverse_of: :owner, dependent: :destroy
  has_many :platform_authorization_token, foreign_key: "userId", inverse_of: :owner, dependent: :destroy
  has_many :profiles, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :secondary_emails, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :out_of_office_reasons, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :notifications_subscriptions, class_name: "NotificationsSubscriptions", foreign_key: "userId",
                                         inverse_of: :user, dependent: :destroy
  has_many :features, class_name: "UserFeatures", foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :reassigned_bookings, class_name: "Booking", foreign_key: "reassignById", inverse_of: :reassign_by,
                                 dependent: :nullify
  has_many :created_attribute_to_users, class_name: "AttributeToUser", foreign_key: "createdById",
                                        inverse_of: :created_by, dependent: :nullify
  has_many :updated_attribute_to_users, class_name: "AttributeToUser", foreign_key: "updatedById",
                                        inverse_of: :updated_by, dependent: :nullify
  has_many :created_translations, class_name: "EventTypeTranslation", foreign_key: "createdBy", inverse_of: :creator,
                                  dependent: :restrict_with_error
  has_many :updated_translations, class_name: "EventTypeTranslation", foreign_key: "updatedBy", inverse_of: :updater,
                                  dependent: :nullify
  has_many :reported_bookings, class_name: "BookingReport", foreign_key: "reportedById", inverse_of: :reported_by,
                               dependent: :nullify
  has_many :wrong_assignment_reports, foreign_key: "reportedById", inverse_of: :reported_by, dependent: :nullify
  has_many :reviewed_wrong_assignment_reports, class_name: "WrongAssignmentReport", foreign_key: "reviewedById",
                                               inverse_of: :reviewed_by, dependent: :nullify
  has_many :booking_internal_note, foreign_key: "createdById", inverse_of: :created_by, dependent: :restrict_with_error
  has_many :created_organization_onboardings, class_name: "OrganizationOnboarding", foreign_key: "createdById",
                                              inverse_of: :created_by, dependent: :destroy
  has_many :filter_segments, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :filter_segment_preferences, class_name: "UserFilterSegmentPreference", foreign_key: "userId",
                                        inverse_of: :user, dependent: :destroy
  has_many :cal_ai_phone_numbers, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :agents, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :o_auth_clients, foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_and_belongs_to_many :event_types, join_table: "_user_eventtype", foreign_key: "B", association_foreign_key: "A"
  has_and_belongs_to_many :platform_oauth_clients, class_name: "PlatformOAuthClient",
                                                   join_table: "_PlatformOAuthClientToUser", foreign_key: "B",
                                                   association_foreign_key: "A"
end
