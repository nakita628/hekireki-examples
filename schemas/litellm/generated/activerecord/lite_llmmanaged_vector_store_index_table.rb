class LiteLLMManagedVectorStoreIndexTable < ApplicationRecord
  self.table_name = "LiteLLM_ManagedVectorStoreIndexTable"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }

  validates :index_name, presence: true, uniqueness: true
  validates :litellm_params, exclusion: { in: [ nil ] }
end
