class LiteLLMDailyGuardrailMetrics < ApplicationRecord
  self.table_name = "LiteLLM_DailyGuardrailMetrics"
  self.primary_key = %w[guardrail_id date]

  attribute :requests_evaluated, default: 0
  attribute :passed_count, default: 0
  attribute :blocked_count, default: 0
  attribute :flagged_count, default: 0
end
