class LiteLLMTeamMembership < ApplicationRecord
  self.table_name = "LiteLLM_TeamMembership"
  self.primary_key = %w[user_id team_id]

  attribute :spend, default: 0
  attribute :total_spend, default: 0

  belongs_to :litellm_budget_table, class_name: "LiteLLMBudgetTable", foreign_key: "budget_id", optional: true,
                                    inverse_of: :team_membership
end
