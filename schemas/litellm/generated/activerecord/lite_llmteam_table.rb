class LiteLLMTeamTable < ApplicationRecord
  self.table_name = "LiteLLM_TeamTable"
  self.primary_key = "team_id"
  self.implicit_order_column = "created_at"

  attribute :team_id, default: -> { SecureRandom.uuid }
  attribute :admins, default: -> { [] }
  attribute :members, default: -> { [] }
  attribute :members_with_roles, default: -> { JSON.parse("{}") }
  attribute :metadata, default: -> { JSON.parse("{}") }
  attribute :spend, default: 0
  attribute :models, default: -> { [] }
  attribute :blocked, default: false
  attribute :model_spend, default: -> { JSON.parse("{}") }
  attribute :model_max_budget, default: -> { JSON.parse("{}") }
  attribute :router_settings, default: -> { JSON.parse("{}") }
  attribute :team_member_permissions, default: -> { [] }
  attribute :access_group_ids, default: -> { [] }
  attribute :policies, default: -> { [] }
  attribute :default_team_member_models, default: -> { [] }
  attribute :allow_team_guardrail_config, default: false

  validates :model_id, uniqueness: { allow_nil: true }

  belongs_to :litellm_organization_table, class_name: "LiteLLMOrganizationTable", foreign_key: "organization_id",
                                          optional: true, inverse_of: :teams
  belongs_to :litellm_model_table, class_name: "LiteLLMModelTable", foreign_key: "model_id", optional: true,
                                   inverse_of: :team
  belongs_to :object_permission, class_name: "LiteLLMObjectPermissionTable", optional: true, inverse_of: :teams
  has_many :projects, class_name: "LiteLLMProjectTable", foreign_key: "team_id", inverse_of: :litellm_team_table,
                      dependent: :nullify
end
