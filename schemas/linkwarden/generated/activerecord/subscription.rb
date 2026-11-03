class Subscription < ApplicationRecord
  self.table_name = "Subscription"

  attribute :quantity, default: 1
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :provider, { stripe: "STRIPE", apple: "APPLE", google: "GOOGLE" }, validate: true

  validates :active, inclusion: { in: [ true, false ] }
  validates :currentPeriodStart, presence: true
  validates :currentPeriodEnd, presence: true
  validates :storeOriginalTransactionId, uniqueness: { allow_nil: true }
  validates :googlePurchaseToken, uniqueness: { allow_nil: true }
  validates :stripeSubscriptionId, uniqueness: { allow_nil: true }
  validates :userId, uniqueness: true

  belongs_to :user, foreign_key: "userId", inverse_of: :subscriptions
  has_many :child_users, class_name: "User", foreign_key: "parentSubscriptionId", inverse_of: :parent_subscription,
                         dependent: :nullify
end
