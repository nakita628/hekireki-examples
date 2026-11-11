class LiteLLMRetiredAgent < ApplicationRecord
  self.table_name = "LiteLLM_RetiredAgent"
  self.primary_key = "original_agent_id"
end
