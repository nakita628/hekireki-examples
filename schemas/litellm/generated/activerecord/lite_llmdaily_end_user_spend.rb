class LiteLLMDailyEndUserSpend < ApplicationRecord
  self.table_name = "LiteLLM_DailyEndUserSpend"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :prompt_tokens, default: 0
  attribute :completion_tokens, default: 0
  attribute :cache_read_input_tokens, default: 0
  attribute :cache_creation_input_tokens, default: 0
  attribute :compression_saved_tokens, default: 0
  attribute :compression_savings_spend, default: 0
  attribute :prompt_caching_savings_spend, default: 0
  attribute :gateway_injected_caching_savings_spend, default: 0
  attribute :autorouter_savings_spend, default: 0
  attribute :guardrail_spend, default: 0
  attribute :spend, default: 0
  attribute :api_requests, default: 0
  attribute :successful_requests, default: 0
  attribute :failed_requests, default: 0
  attribute :total_response_time_ms, default: 0
  attribute :timed_requests, default: 0

  validates :end_user_id,
            uniqueness: { scope: %i[date api_key model custom_llm_provider mcp_namespaced_tool_name endpoint], conditions: -> { where.not(model: nil).where.not(custom_llm_provider: nil).where.not(mcp_namespaced_tool_name: nil).where.not(endpoint: nil) }, allow_nil: true }
  validates :date, presence: true
  validates :api_key, presence: true
end
