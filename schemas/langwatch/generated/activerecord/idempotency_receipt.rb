class IdempotencyReceipt < ApplicationRecord
  self.table_name = "IdempotencyReceipt"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Nanoid.generate }
  alias_attribute :created_at, :createdAt

  validates :scopeId, presence: true, uniqueness: { scope: :key }
  validates :key, presence: true
  validates :claimId, presence: true
  validates :requestFingerprint, presence: true
  validates :heartbeatAt, presence: true
  validates :expiresAt, presence: true
end
