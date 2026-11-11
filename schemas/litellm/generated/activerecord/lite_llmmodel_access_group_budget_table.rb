class LiteLLMModelAccessGroupBudgetTable < ApplicationRecord
  self.table_name = "LiteLLM_ModelAccessGroupBudgetTable"
  self.primary_key = "access_group_name"

  attribute :spend, default: 0

  belongs_to :litellm_budget_table, class_name: "LiteLLMBudgetTable", foreign_key: "budget_id", optional: true,
                                    inverse_of: :model_access_groups
end
