class LiteLLMDailyGuardrailUsageUnits < ApplicationRecord
  self.table_name = "LiteLLM_DailyGuardrailUsageUnits"
  self.primary_key = %w[guardrail_id date team_id api_key usage_unit]

  attribute :units, default: 0
  attribute :untracked_units, default: 0
end
