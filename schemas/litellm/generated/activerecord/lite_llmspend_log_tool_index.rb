class LiteLLMSpendLogToolIndex < ApplicationRecord
  self.table_name = "LiteLLM_SpendLogToolIndex"
  self.primary_key = %w[request_id tool_name]

  validates :start_time, presence: true
end
