class LiteLLMManagedFileTable < ApplicationRecord
  self.table_name = "LiteLLM_ManagedFileTable"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :flat_model_file_ids, default: -> { [] }

  validates :unified_file_id, presence: true, uniqueness: true
  validates :model_mappings, exclusion: { in: [ nil ] }
end
