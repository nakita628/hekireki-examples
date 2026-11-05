class LoopsEmailSource < ApplicationRecord
  self.table_name = "LoopsEmailSource"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :emailMessageId, uniqueness: { allow_nil: true }
end
