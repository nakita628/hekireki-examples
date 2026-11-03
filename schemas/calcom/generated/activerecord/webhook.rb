class Webhook < ApplicationRecord
  self.table_name = "Webhook"

  attribute :active, default: true
  attribute :eventTriggers, default: -> { [] }
  attribute :platform, default: false
  attribute :version, default: "2021-10-20"
  alias_attribute :created_at, :createdAt

  enum :timeUnit, { day: "day", hour: "hour", minute: "minute" }, validate: { allow_nil: true }

  validates :subscriberUrl, presence: true, uniqueness: { scope: :userId, conditions: -> { where.not(userId: nil) } }
  validates :subscriberUrl,
            uniqueness: { scope: :platformOAuthClientId, conditions: -> { where.not(platformOAuthClientId: nil) } }

  belongs_to :user, foreign_key: "userId", optional: true, inverse_of: :webhooks
  belongs_to :team, foreign_key: "teamId", optional: true, inverse_of: :webhooks
  belongs_to :event_type, foreign_key: "eventTypeId", optional: true, inverse_of: :webhooks
  belongs_to :platform_oauth_client, class_name: "PlatformOAuthClient", foreign_key: "platformOAuthClientId",
                                     optional: true, inverse_of: :webhook
  belongs_to :app, foreign_key: "appId", optional: true, inverse_of: :webhook
  has_many :scheduled_triggers, class_name: "WebhookScheduledTriggers", foreign_key: "webhookId", inverse_of: :webhook,
                                dependent: :destroy
end
