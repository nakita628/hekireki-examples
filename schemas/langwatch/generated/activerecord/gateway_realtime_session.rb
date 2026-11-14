class GatewayRealtimeSession < ApplicationRecord
  self.table_name = "GatewayRealtimeSession"

  attribute :reportedCostNanoUsd, default: 0
  attribute :reportCount, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  enum :status, { open: "OPEN", closed: "CLOSED", failed: "FAILED", expired: "EXPIRED" }, default: :open, prefix: true,
                                                                                          validate: true

  validates :projectId, presence: true
  validates :organizationId, presence: true
  validates :virtualKeyId, presence: true
  validates :modelProviderId, presence: true
  validates :vendor, presence: true,
                     uniqueness: { scope: :vendorConversationId, conditions: -> { where.not(vendorConversationId: nil) } }
  validates :model, presence: true
end
