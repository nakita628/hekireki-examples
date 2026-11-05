class Verification < ApplicationRecord
  self.table_name = "verification"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :identifier, presence: true
  validates :value, presence: true
  validates :expiresAt, presence: true
end
