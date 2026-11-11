class LiteLLMDailyPolicyMetrics < ApplicationRecord
  self.table_name = "LiteLLM_DailyPolicyMetrics"
  self.primary_key = %w[policy_id date]

  attribute :requests_evaluated, default: 0
  attribute :passed_count, default: 0
  attribute :blocked_count, default: 0
  attribute :flagged_count, default: 0
end
