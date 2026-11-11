class LiteLLMSkillsTable < ApplicationRecord
  self.table_name = "LiteLLM_SkillsTable"
  self.primary_key = "skill_id"
  self.implicit_order_column = "created_at"

  attribute :skill_id, default: -> { SecureRandom.uuid }
  attribute :source, default: "custom"
  attribute :metadata, default: -> { JSON.parse("{}") }
end
