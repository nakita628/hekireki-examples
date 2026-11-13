class LiteLLMDailyModelUsage < ApplicationRecord
  self.table_name = "LiteLLM_DailyModelUsage"
  self.primary_key = %w[date model_group model custom_llm_provider task_type]

  attribute :spend, default: 0
  attribute :prompt_tokens, default: 0
  attribute :completion_tokens, default: 0
  attribute :request_count, default: 0
  attribute :successful_requests, default: 0
  attribute :failed_requests, default: 0
  attribute :total_response_time_ms, default: 0
  attribute :timed_requests, default: 0
end
