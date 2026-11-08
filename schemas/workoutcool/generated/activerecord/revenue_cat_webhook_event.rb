class RevenueCatWebhookEvent < ApplicationRecord
  self.table_name = "revenuecat_webhook_events"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :processed, default: false
  attribute :retryCount, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :eventType, presence: true
  validates :eventTimestamp, presence: true
  validates :appUserId, presence: true
  validates :environment, presence: true
  validates :rawEventData, exclusion: { in: [ nil ] }
end
