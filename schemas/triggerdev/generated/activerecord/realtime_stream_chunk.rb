class RealtimeStreamChunk < ApplicationRecord
  self.table_name = "RealtimeStreamChunk"
  self.implicit_order_column = "createdAt"

  attribute :id, default: -> { Cuid.generate }
  alias_attribute :created_at, :createdAt

  validates :key, presence: true
  validates :value, presence: true
  validates :sequence, presence: true
  validates :runId, presence: true
end
