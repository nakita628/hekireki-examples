class ConnectedBillingAccount < ApplicationRecord
  self.table_name = "ConnectedBillingAccount"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :commitUsdCents, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :seatCurrency, { usd: "USD", eur: "EUR" }, validate: true

  validates :organizationId, uniqueness: true
  validates :stripeCustomerId, presence: true, uniqueness: true
  validates :termStartsAt, presence: true
  validates :termEndsAt, presence: true
  validates :seatRateCents, presence: true
  validates :seats, presence: true
  validates :billingEmail, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :connected_billing
end
