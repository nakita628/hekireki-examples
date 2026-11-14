class AuditLog < ApplicationRecord
  self.table_name = "AuditLog"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :action, presence: true
  validates :idempotencyKey, uniqueness: { allow_nil: true }
end
