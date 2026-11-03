class ChatSession < ApplicationRecord
  self.table_name = "ChatSession"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt
  alias_attribute :updated_at, :updatedAt

  validates :state, exclusion: { in: [ nil ] }
end
