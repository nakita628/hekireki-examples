class WebhookEndpointDelivery < ApplicationRecord
  self.table_name = "WebhookEndpointDelivery"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt

  enum :channel, { platform: "platform", automations: "automations" }, default: :platform, validate: true
  enum :outcome, { success: "success", retryable: "retryable", terminal: "terminal", pending: "pending" },
       validate: true

  validates :dispatchId, presence: true

  belongs_to :endpoint, class_name: "WebhookEndpoint", foreign_key: "endpointId", optional: true,
                        inverse_of: :deliveries
  belongs_to :project, foreign_key: "projectId", optional: true, inverse_of: :webhook_deliveries
  belongs_to :trigger, foreign_key: "triggerId", optional: true, inverse_of: :webhook_deliveries
end
