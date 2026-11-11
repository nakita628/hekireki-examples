class LiteLLMDeletedTeamTable < ApplicationRecord
  self.table_name = "LiteLLM_DeletedTeamTable"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }
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
  attribute :allow_team_guardrail_config, default: false

  validates :team_id, presence: true
end
