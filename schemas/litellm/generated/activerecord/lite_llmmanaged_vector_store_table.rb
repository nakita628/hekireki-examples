class LiteLLMManagedVectorStoreTable < ApplicationRecord
  self.table_name = "LiteLLM_ManagedVectorStoreTable"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :flat_model_resource_ids, default: -> { [] }

  validates :unified_resource_id, presence: true, uniqueness: true
  validates :model_mappings, exclusion: { in: [ nil ] }
end
