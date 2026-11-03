class Subscription < ApplicationRecord
  self.table_name = "Subscription"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :provider, default: "stripe"
  attribute :isLifetime, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :subscriptionTier, { standard: "STANDARD", pro: "PRO", team: "TEAM", ultimate: "ULTIMATE" }, validate: true
  enum :period, { monthly: "MONTHLY", yearly: "YEARLY" }, validate: true

  validates :organizationId, uniqueness: true
  validates :totalChannels, presence: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :subscription
end
