class LiteLLMBudgetWindowSpend < ApplicationRecord
  self.table_name = "LiteLLM_BudgetWindowSpend"
  self.primary_key = %w[entity_type entity_id window_duration]

  attribute :spend, default: 0

  validates :window_start, presence: true
end
