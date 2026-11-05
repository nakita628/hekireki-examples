class User < ApplicationRecord
  self.table_name = "User"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :emailVerified, default: false
  attribute :dismissedHints, default: -> { [] }
  attribute :surveyFeatures, default: -> { [] }
  attribute :mcpServerEnabled, default: false
  attribute :mcpTokenVersion, default: 0
  attribute :emailOtpEnabled, default: false
  attribute :emailOtpVersion, default: 0
  attribute :scimAccessDisabled, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :signupIntent, { ai_assistant: "AI_ASSISTANT", mail_client: "MAIL_CLIENT", both: "BOTH" },
       validate: { allow_nil: true }

  validates :email, presence: true, uniqueness: true
  validates :referralCode, uniqueness: { allow_nil: true }

  belongs_to :premium, foreign_key: "premiumId", optional: true, inverse_of: :users
  belongs_to :premium_admin, class_name: "Premium", foreign_key: "premiumAdminId", optional: true, inverse_of: :admins
  has_one :referral_received, class_name: "Referral", foreign_key: "referredUserId", inverse_of: :referred_user,
                              dependent: :destroy
  has_one :scim_subject, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :accounts, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :sessions, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :api_keys, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :email_accounts, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :referrals_made, class_name: "Referral", foreign_key: "referrerUserId", inverse_of: :referrer_user,
                            dependent: :destroy
  has_many :mobile_push_tokens, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :oauth_client_records, class_name: "OauthClient", foreign_key: "userId", inverse_of: :user,
                                  dependent: :destroy
  has_many :oauth_refresh_token_records, class_name: "OauthRefreshToken", foreign_key: "userId", inverse_of: :user,
                                         dependent: :destroy
  has_many :oauth_access_token_records, class_name: "OauthAccessToken", foreign_key: "userId", inverse_of: :user,
                                        dependent: :destroy
  has_many :oauth_consent_records, class_name: "OauthConsent", foreign_key: "userId", inverse_of: :user,
                                   dependent: :destroy
  has_many :sso_providers, foreign_key: "userId", inverse_of: :user, dependent: :nullify
  has_many :scim_identity_tombstones, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :scim_users, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :scim_projection_grants, foreign_key: "userId", inverse_of: :user, dependent: :destroy
  has_many :scim_identity_links, foreign_key: "userId", inverse_of: :user, dependent: :destroy
end
