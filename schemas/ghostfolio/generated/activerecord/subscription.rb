class Subscription < ApplicationRecord
  self.table_name = "Subscription"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :expiresAt, presence: true
  validates :stripeCheckoutSessionId, uniqueness: { allow_nil: true }

  belongs_to :user, foreign_key: "userId", inverse_of: :subscriptions
end
