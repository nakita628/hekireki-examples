class LiteLLMOrganizationMembership < ApplicationRecord
  self.table_name = "LiteLLM_OrganizationMembership"
  self.primary_key = %w[user_id organization_id]

  attribute :spend, default: 0

  validates :user_id, uniqueness: { scope: :organization_id }

  belongs_to :user, class_name: "LiteLLMUserTable", inverse_of: :organization_memberships
  belongs_to :organization, class_name: "LiteLLMOrganizationTable", inverse_of: :members
  belongs_to :litellm_budget_table, class_name: "LiteLLMBudgetTable", foreign_key: "budget_id", optional: true,
                                    inverse_of: :organization_membership
end
