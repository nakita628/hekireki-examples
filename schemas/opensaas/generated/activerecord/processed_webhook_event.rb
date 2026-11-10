class ProcessedWebhookEvent < ApplicationRecord
  self.table_name = "ProcessedWebhookEvent"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  alias_attribute :created_at, :createdAt

  validates :paymentProcessorEventId, presence: true, uniqueness: true
  validates :eventType, presence: true
end
