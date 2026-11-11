class LiteLLMDailyToolSpend < ApplicationRecord
  self.table_name = "LiteLLM_DailyToolSpend"
  self.primary_key = %w[date tool_name]

  attribute :spend, default: 0
  attribute :total_tokens, default: 0
  attribute :request_count, default: 0
end
