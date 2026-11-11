class LiteLLMMemoryTable < ApplicationRecord
  self.table_name = "LiteLLM_MemoryTable"
  self.primary_key = "memory_id"
  self.implicit_order_column = "created_at"

  attribute :memory_id, default: -> { SecureRandom.uuid }

  validates :key, presence: true, uniqueness: true
  validates :value, presence: true
end
