class LiteLLMErrorLogs < ApplicationRecord
  self.table_name = "LiteLLM_ErrorLogs"
  self.primary_key = "request_id"

  attribute :request_id, default: -> { SecureRandom.uuid }
  attribute :api_base, default: ""
  attribute :model_group, default: ""
  attribute :litellm_model_name, default: ""
  attribute :model_id, default: ""
  attribute :request_kwargs, default: -> { JSON.parse("{}") }
  attribute :exception_type, default: ""
  attribute :exception_string, default: ""
  attribute :status_code, default: ""

  validates :startTime, presence: true
  validates :endTime, presence: true
end
