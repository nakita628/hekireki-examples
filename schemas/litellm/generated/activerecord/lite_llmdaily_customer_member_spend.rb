class LiteLLMDailyCustomerMemberSpend < ApplicationRecord
  self.table_name = "LiteLLM_DailyCustomerMemberSpend"

  attribute :customer_team_id, default: ""
  attribute :customer_member_id, default: ""
  attribute :model, default: ""
  attribute :model_group, default: ""
  attribute :custom_llm_provider, default: ""
  attribute :mcp_namespaced_tool_name, default: ""
  attribute :endpoint, default: ""
  attribute :api_key, default: ""
  attribute :spend, default: 0
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
  attribute :api_requests, default: 0
  attribute :successful_requests, default: 0
  attribute :failed_requests, default: 0
  attribute :total_response_time_ms, default: 0
  attribute :timed_requests, default: 0

  validates :date, presence: true
  validates :customer_org_id, presence: true
end
