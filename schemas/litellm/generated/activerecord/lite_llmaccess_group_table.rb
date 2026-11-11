class LiteLLMAccessGroupTable < ApplicationRecord
  self.table_name = "LiteLLM_AccessGroupTable"
  self.primary_key = "access_group_id"
  self.implicit_order_column = "created_at"

  attribute :access_group_id, default: -> { SecureRandom.uuid }
  attribute :access_model_names, default: -> { [] }
  attribute :access_mcp_server_ids, default: -> { [] }
  attribute :access_agent_ids, default: -> { [] }
  attribute :access_skill_names, default: -> { [] }
  attribute :assigned_team_ids, default: -> { [] }
  attribute :assigned_key_ids, default: -> { [] }

  validates :access_group_name, presence: true, uniqueness: true
end
