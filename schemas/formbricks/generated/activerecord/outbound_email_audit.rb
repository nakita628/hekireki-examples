class OutboundEmailAudit < ApplicationRecord
  self.table_name = "OutboundEmailAudit"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :status, default: "prepared"
  attribute :auditGap, default: false
  attribute :contentDeleteFailures, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :expiresAt, presence: true
  validates :contentExpiresAt, presence: true
  validates :environment, presence: true
  validates :emailType, presence: true
  validates :trigger, presence: true
  validates :actorKind, presence: true
end
