class LiteLLMProjectTable < ApplicationRecord
  self.table_name = "LiteLLM_ProjectTable"
  self.primary_key = "project_id"
  self.implicit_order_column = "created_at"

  attribute :project_id, default: -> { SecureRandom.uuid }
  attribute :metadata, default: -> { JSON.parse("{}") }
  attribute :models, default: -> { [] }
  attribute :spend, default: 0
  attribute :model_spend, default: -> { JSON.parse("{}") }
  attribute :model_rpm_limit, default: -> { JSON.parse("{}") }
  attribute :model_tpm_limit, default: -> { JSON.parse("{}") }
  attribute :blocked, default: false

  validates :created_by, presence: true
  validates :updated_by, presence: true

  belongs_to :litellm_team_table, class_name: "LiteLLMTeamTable", foreign_key: "team_id", optional: true,
                                  inverse_of: :projects
  belongs_to :litellm_budget_table, class_name: "LiteLLMBudgetTable", foreign_key: "budget_id", optional: true,
                                    inverse_of: :projects
  belongs_to :object_permission, class_name: "LiteLLMObjectPermissionTable", optional: true, inverse_of: :projects
  has_many :keys, class_name: "LiteLLMVerificationToken", foreign_key: "project_id", inverse_of: :litellm_project_table,
                  dependent: :nullify
end
