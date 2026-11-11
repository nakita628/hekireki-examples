class LiteLLMManagedVectorStoresTable < ApplicationRecord
  self.table_name = "LiteLLM_ManagedVectorStoresTable"
  self.primary_key = "vector_store_id"

  validates :custom_llm_provider, presence: true
end
