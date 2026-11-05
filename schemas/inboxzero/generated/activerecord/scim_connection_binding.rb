class ScimConnectionBinding < ApplicationRecord
  self.table_name = "scimConnectionBinding"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :decommissionStatus, default: "active"
  attribute :decommissionReconciledUserCount, default: 0
  attribute :decommissionBatchCount, default: 0
  attribute :decommissionRevision, default: 0
  alias_attribute :created_at, :createdAt

  validates :connectionId, presence: true
  validates :connectionKey, presence: true, uniqueness: true
  validates :connectionKey, uniqueness: true
  validates :provisioningDomainId, presence: true
end
