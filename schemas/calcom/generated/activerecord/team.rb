class Team < ApplicationRecord
  self.table_name = "Team"

  attribute :hideBranding, default: false
  attribute :hideTeamProfileLink, default: false
  attribute :isPrivate, default: false
  attribute :hideBookATeamMember, default: false
  attribute :timeZone, default: "Europe/London"
  attribute :weekStart, default: "Sunday"
  attribute :isOrganization, default: false
  attribute :pendingPayment, default: false
  attribute :isPlatform, default: false
  attribute :smsLockReviewedByAdmin, default: false
  attribute :includeManagedEventsInLimits, default: false
  attribute :autoOptInFeatures, default: false
  alias_attribute :created_at, :createdAt

  enum :rrResetInterval, { month: "MONTH", day: "DAY" }, default: :month, validate: { allow_nil: true }
  enum :rrTimestampBasis, { created_at: "CREATED_AT", start_time: "START_TIME" }, default: :created_at, validate: true
  enum :smsLockState, { locked: "LOCKED", unlocked: "UNLOCKED", review_needed: "REVIEW_NEEDED" }, default: :unlocked,
                                                                                                  validate: true

  validates :name, presence: true
  validates :slug, uniqueness: { scope: :parentId, conditions: -> { where.not(parentId: nil) }, allow_nil: true }

  belongs_to :parent, class_name: "Team", foreign_key: "parentId", optional: true, inverse_of: :children
  belongs_to :created_by_oauth_client, class_name: "PlatformOAuthClient", foreign_key: "createdByOAuthClientId",
                                       optional: true, inverse_of: :teams
  has_one :organization_settings, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_one :platform_billing, foreign_key: "id", inverse_of: :team, dependent: :destroy
  has_one :credit_balance, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_one :organization_onboarding, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_one :managed_organization, foreign_key: "managedOrganizationId", inverse_of: :managed_organization,
                                 dependent: :destroy
  has_one :team_billing, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_one :organization_billing, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :members, class_name: "Membership", foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :event_types, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :verified_numbers, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :verified_emails, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :children, class_name: "Team", foreign_key: "parentId", inverse_of: :parent, dependent: :destroy
  has_many :org_users, class_name: "User", foreign_key: "organizationId", inverse_of: :organization, dependent: :nullify
  has_many :invite_tokens, class_name: "VerificationToken", foreign_key: "teamId", inverse_of: :team,
                           dependent: :nullify
  has_many :webhooks, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :api_keys, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :credentials, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :access_codes, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :instant_meeting_tokens, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :org_profiles, class_name: "Profile", foreign_key: "organizationId", inverse_of: :organization,
                          dependent: :destroy
  has_many :dsync_team_group_mapping, class_name: "DSyncTeamGroupMapping", foreign_key: "teamId", inverse_of: :team,
                                      dependent: :destroy
  has_many :platform_oauth_client, class_name: "PlatformOAuthClient", foreign_key: "organizationId",
                                   inverse_of: :organization, dependent: :destroy
  has_many :attributes, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :delegation_credentials, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :roles, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :cal_ai_phone_numbers, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :agents, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :booking_reports, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :wrong_assignment_reports, foreign_key: "teamId", inverse_of: :team, dependent: :nullify
  has_many :features, class_name: "TeamFeatures", foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :internal_note_presets, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :managed_organizations, foreign_key: "managerOrganizationId", inverse_of: :manager_organization,
                                   dependent: :destroy
  has_many :filter_segments, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :integration_attribute_syncs, foreign_key: "organizationId", inverse_of: :organization, dependent: :destroy
  has_many :seat_change_logs, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
  has_many :monthly_prorations, foreign_key: "teamId", inverse_of: :team, dependent: :destroy
end
