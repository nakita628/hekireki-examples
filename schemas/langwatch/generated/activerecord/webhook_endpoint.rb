class WebhookEndpoint < ApplicationRecord
  self.table_name = "WebhookEndpoint"

  attribute :enabledEvents, default: -> { [] }
  attribute :maxBatchSize, default: 100
  attribute :maxBatchDelayMs, default: 250
  attribute :maxInFlight, default: 4
  attribute :allowSelfSignedCertificate, default: false
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :destinationKind, { http: "http", sqs: "sqs" }, default: :http, validate: true
  enum :status, { active: "ACTIVE", disabled: "DISABLED" }, default: :active, validate: true

  validates :secretEncrypted, presence: true
  validates :idempotencyKey, uniqueness: { scope: :organizationId, allow_nil: true }

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :webhook_endpoints
  has_many :deliveries, class_name: "WebhookEndpointDelivery", foreign_key: "endpointId", inverse_of: :endpoint,
                        dependent: :destroy
end
