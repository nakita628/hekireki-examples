class SubscriptionPlan < ApplicationRecord
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :currency, default: "EUR"
  attribute :interval, default: "month"
  attribute :isActive, default: true
  attribute :availableRegions, default: -> { [] }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  has_many :subscriptions, foreign_key: "planId", inverse_of: :plan, dependent: :restrict_with_error
  has_many :provider_mappings, class_name: "PlanProviderMapping", foreign_key: "planId", inverse_of: :plan,
                               dependent: :destroy
end
