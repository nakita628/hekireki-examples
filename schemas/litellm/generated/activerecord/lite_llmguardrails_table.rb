class LiteLLMGuardrailsTable < ApplicationRecord
  self.table_name = "LiteLLM_GuardrailsTable"
  self.primary_key = "guardrail_id"
  self.implicit_order_column = "created_at"

  attribute :guardrail_id, default: -> { SecureRandom.uuid }
  attribute :status, default: "active"

  validates :guardrail_name, presence: true, uniqueness: true
  validates :litellm_params, exclusion: { in: [ nil ] }
end
