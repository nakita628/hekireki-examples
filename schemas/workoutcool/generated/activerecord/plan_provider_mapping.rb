class PlanProviderMapping < ApplicationRecord
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :isActive, default: true
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :provider, { stripe: "STRIPE", paypal: "PAYPAL", lemonsqueezy: "LEMONSQUEEZY", paddle: "PADDLE", apple_pay: "APPLE_PAY", google_pay: "GOOGLE_PAY", revenuecat: "REVENUECAT", none: "NONE", other: "OTHER" },
       prefix: true, validate: true

  validates :externalId, presence: true
  validates :region, uniqueness: { scope: [ :planId, :provider ], allow_nil: true }

  belongs_to :plan, class_name: "SubscriptionPlan", foreign_key: "planId", inverse_of: :provider_mappings
end
