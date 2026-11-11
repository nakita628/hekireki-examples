class LiteLLMSpendLogs < ApplicationRecord
  self.table_name = "LiteLLM_SpendLogs"
  self.primary_key = "request_id"

  attribute :api_key, default: ""
  attribute :spend, default: 0
  attribute :total_tokens, default: 0
  attribute :prompt_tokens, default: 0
  attribute :completion_tokens, default: 0
  attribute :model, default: ""
  attribute :model_id, default: ""
  attribute :model_group, default: ""
  attribute :custom_llm_provider, default: ""
  attribute :api_base, default: ""
  attribute :user, default: ""
  attribute :metadata, default: -> { JSON.parse("{}") }
  attribute :cache_hit, default: ""
  attribute :cache_key, default: ""
  attribute :request_tags, default: -> { JSON.parse("[]") }
  attribute :messages, default: -> { JSON.parse("{}") }
  attribute :response, default: -> { JSON.parse("{}") }
  attribute :proxy_server_request, default: -> { JSON.parse("{}") }

  validates :call_type, presence: true
  validates :startTime, presence: true
  validates :endTime, presence: true
end
