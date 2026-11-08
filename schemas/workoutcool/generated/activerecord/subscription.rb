class Subscription < ApplicationRecord
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { active: "ACTIVE", trial: "TRIAL", cancelled: "CANCELLED", expired: "EXPIRED", paused: "PAUSED" },
       validate: true
  enum :platform, { web: "WEB", ios: "IOS", android: "ANDROID" }, validate: { allow_nil: true }

  validates :userId, uniqueness: { scope: :platform, conditions: -> { where.not(platform: nil) } }
  validates :startedAt, presence: true

  belongs_to :user, foreign_key: "userId", inverse_of: :subscriptions
  belongs_to :plan, class_name: "SubscriptionPlan", foreign_key: "planId", inverse_of: :subscriptions
end
