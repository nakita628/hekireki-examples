class Premium < ApplicationRecord
  self.table_name = "Premium"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :pendingInvites, default: -> { [] }
  attribute :stripeInvoiceEmailsEnabled, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :adminGrantTier, { basic_monthly: "BASIC_MONTHLY", basic_annually: "BASIC_ANNUALLY", pro_monthly: "PRO_MONTHLY", pro_annually: "PRO_ANNUALLY", starter_monthly: "BUSINESS_MONTHLY", starter_annually: "BUSINESS_ANNUALLY", plus_monthly: "PLUS_MONTHLY", plus_annually: "PLUS_ANNUALLY", professional_monthly: "BUSINESS_PLUS_MONTHLY", professional_annually: "BUSINESS_PLUS_ANNUALLY", copilot_monthly: "COPILOT_MONTHLY", lifetime: "LIFETIME" },
       validate: { allow_nil: true }
  enum :tier, { basic_monthly: "BASIC_MONTHLY", basic_annually: "BASIC_ANNUALLY", pro_monthly: "PRO_MONTHLY", pro_annually: "PRO_ANNUALLY", starter_monthly: "BUSINESS_MONTHLY", starter_annually: "BUSINESS_ANNUALLY", plus_monthly: "PLUS_MONTHLY", plus_annually: "PLUS_ANNUALLY", professional_monthly: "BUSINESS_PLUS_MONTHLY", professional_annually: "BUSINESS_PLUS_ANNUALLY", copilot_monthly: "COPILOT_MONTHLY", lifetime: "LIFETIME" },
       prefix: true, validate: { allow_nil: true }

  validates :stripeCustomerId, uniqueness: { allow_nil: true }
  validates :stripeSubscriptionId, uniqueness: { allow_nil: true }
  validates :stripeSubscriptionItemId, uniqueness: { allow_nil: true }
  validates :appleOriginalTransactionId, uniqueness: { allow_nil: true }
  validates :appleLatestTransactionId, uniqueness: { allow_nil: true }

  has_many :users, foreign_key: "premiumId", inverse_of: :premium, dependent: :nullify
  has_many :admins, class_name: "User", foreign_key: "premiumAdminId", inverse_of: :premium_admin, dependent: :nullify
  has_many :payments, foreign_key: "premiumId", inverse_of: :premium, dependent: :nullify
end
