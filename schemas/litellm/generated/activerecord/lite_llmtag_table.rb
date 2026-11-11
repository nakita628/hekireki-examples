class LiteLLMTagTable < ApplicationRecord
  self.table_name = "LiteLLM_TagTable"
  self.primary_key = "tag_name"

  attribute :models, default: -> { [] }
  attribute :spend, default: 0

  belongs_to :litellm_budget_table, class_name: "LiteLLMBudgetTable", foreign_key: "budget_id", optional: true,
                                    inverse_of: :tags
end
