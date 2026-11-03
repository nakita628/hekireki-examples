class WebhookScheduledTriggers < ApplicationRecord
  self.table_name = "WebhookScheduledTriggers"

  attribute :retryCount, default: 0
  alias_attribute :created_at, :createdAt

  validates :subscriberUrl, presence: true
  validates :payload, presence: true
  validates :startAfter, presence: true

  belongs_to :webhook, foreign_key: "webhookId", optional: true, inverse_of: :scheduled_triggers
  belongs_to :booking, foreign_key: "bookingId", optional: true, inverse_of: :scheduled_triggers
end
