class PendingPremiumGrant < ApplicationRecord
  self.table_name = "PendingPremiumGrant"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :count, default: 1
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :tier, { basic_monthly: "BASIC_MONTHLY", basic_annually: "BASIC_ANNUALLY", pro_monthly: "PRO_MONTHLY", pro_annually: "PRO_ANNUALLY", starter_monthly: "BUSINESS_MONTHLY", starter_annually: "BUSINESS_ANNUALLY", plus_monthly: "PLUS_MONTHLY", plus_annually: "PLUS_ANNUALLY", professional_monthly: "BUSINESS_PLUS_MONTHLY", professional_annually: "BUSINESS_PLUS_ANNUALLY", copilot_monthly: "COPILOT_MONTHLY", lifetime: "LIFETIME" },
       validate: true

  validates :email, presence: true, uniqueness: true
end
