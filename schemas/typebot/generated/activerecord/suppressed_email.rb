class SuppressedEmail < ApplicationRecord
  self.table_name = "SuppressedEmail"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  attribute :transientGeneralBounceCount, default: 0
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :email, presence: true, uniqueness: true
end
