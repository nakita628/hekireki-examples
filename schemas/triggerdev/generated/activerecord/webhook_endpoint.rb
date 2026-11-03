class WebhookEndpoint < ApplicationRecord
  self.table_name = "WebhookEndpoint"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :endpointTenantId, default: ""
  attribute :endpointExternalRef, default: ""
  attribute :routingTargets, default: -> { JSON.parse("[]") }
  attribute :metadata, default: -> { JSON.parse("{}") }
  attribute :secretProvisioning, default: "either"
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :environmentType, { production: "PRODUCTION", staging: "STAGING", development: "DEVELOPMENT", preview: "PREVIEW" },
       validate: true
  enum :status, { active: "ACTIVE", inactive: "INACTIVE", deleting: "DELETING" }, default: :active, validate: true

  validates :friendlyId, presence: true, uniqueness: true
  validates :opaqueId, presence: true, uniqueness: true
  validates :organizationId, presence: true
  validates :projectId, presence: true
  validates :runtimeEnvironmentId, presence: true,
                                   uniqueness: { scope: [ :declaredId, :endpointTenantId, :endpointExternalRef ] }
  validates :source, presence: true
  validates :declaredId, presence: true
  validates :verifierArtifact, exclusion: { in: [ nil ] }
end
