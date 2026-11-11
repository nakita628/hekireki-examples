class LiteLLMSpendLogGuardrailIndex < ApplicationRecord
  self.table_name = "LiteLLM_SpendLogGuardrailIndex"
  self.primary_key = %w[request_id guardrail_id]

  validates :start_time, presence: true
end
