class SpaceWebhook < ApplicationRecord
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { Cuid.generate }
  attribute :events, default: -> { [] }
  attribute :enabled, default: true
  attribute :consecutive_failures, default: 0

  validates :url, presence: true
  validates :secret, presence: true
  validates :version, presence: true

  belongs_to :space, inverse_of: :webhooks
  has_many :deliveries, class_name: "WebhookDelivery", foreign_key: "webhook_id", inverse_of: :webhook,
                        dependent: :destroy
end
