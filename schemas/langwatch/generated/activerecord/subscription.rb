class Subscription < ApplicationRecord
  self.table_name = "Subscription"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  attribute :seatLineWriteQuantities, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :plan, { free: "FREE", pro: "PRO", growth: "GROWTH", growth_seat_eur_monthly: "GROWTH_SEAT_EUR_MONTHLY", growth_seat_eur_annual: "GROWTH_SEAT_EUR_ANNUAL", growth_seat_usd_monthly: "GROWTH_SEAT_USD_MONTHLY", growth_seat_usd_annual: "GROWTH_SEAT_USD_ANNUAL", enterprise: "ENTERPRISE", launch: "LAUNCH", accelerate: "ACCELERATE", launch_annual: "LAUNCH_ANNUAL", accelerate_annual: "ACCELERATE_ANNUAL" },
       validate: true
  enum :status, { pending: "PENDING", failed: "FAILED", active: "ACTIVE", cancelled: "CANCELLED" }, validate: true

  validates :stripeSubscriptionId, uniqueness: { allow_nil: true }

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :subscriptions
  has_many :invoices, foreign_key: "subscriptionId", inverse_of: :subscription, dependent: :restrict_with_error
end
