class WebhookDelivery < ApplicationRecord
  self.table_name = "WebhookDelivery"
  self.primary_key = %w[id createdAt retentionDays]

  attribute :id, default: -> { Cuid.generate }
  attribute :isTest, default: false
  attribute :targets, default: -> { JSON.parse("[]") }
  attribute :retentionDays, default: 30
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :environmentType, { production: "PRODUCTION", staging: "STAGING", development: "DEVELOPMENT", preview: "PREVIEW" },
       validate: true
  enum :status, { pending: "PENDING", processing: "PROCESSING", succeeded: "SUCCEEDED", failed: "FAILED", filtered: "FILTERED", unmatched: "UNMATCHED" },
       default: :pending, validate: true

  validates :friendlyId, presence: true
  validates :webhookEndpointId, presence: true
  validates :organizationId, presence: true
  validates :projectId, presence: true
  validates :runtimeEnvironmentId, presence: true
  validates :externalDeliveryId, presence: true
  validates :idempotencyKey, presence: true
end
