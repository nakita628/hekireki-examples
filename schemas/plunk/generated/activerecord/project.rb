class Project < ApplicationRecord
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :disabled, default: false
  attribute :cardVerificationAttempts, default: 0
  attribute :language, default: "en"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :disabledReason, { payment_failed: "PAYMENT_FAILED", email_reputation: "EMAIL_REPUTATION", phishing_detected: "PHISHING_DETECTED", card_verification_failed: "CARD_VERIFICATION_FAILED", card_verification_required: "CARD_VERIFICATION_REQUIRED", manual: "MANUAL" },
       validate: { allow_nil: true }
  enum :cardVerification, { pending: "PENDING", verified: "VERIFIED", requires_action: "REQUIRES_ACTION", failed: "FAILED" },
       validate: { allow_nil: true }
  enum :tracking, { enabled: "ENABLED", disabled: "DISABLED", marketing_only: "MARKETING_ONLY" }, default: :enabled,
                                                                                                  validate: true

  validates :name, presence: true
  validates :public, presence: true, uniqueness: true
  validates :secret, presence: true, uniqueness: true
  validates :customer, uniqueness: { allow_nil: true }
  validates :subscription, uniqueness: { allow_nil: true }

  has_many :members, class_name: "Membership", foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :contacts, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :templates, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :segments, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :workflows, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :campaigns, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :emails, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :events, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :domains, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :api_requests, foreign_key: "projectId", inverse_of: :project, dependent: :nullify
  has_many :idempotency_keys, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
  has_many :throttle_claims, foreign_key: "projectId", inverse_of: :project, dependent: :destroy
end
