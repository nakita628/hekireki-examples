class LiteLLMDeletedVerificationToken < ApplicationRecord
  self.table_name = "LiteLLM_DeletedVerificationToken"
  self.implicit_order_column = "created_at"

  attribute :id, default: -> { SecureRandom.uuid }
  attribute :soft_budget_cooldown, default: false
  attribute :spend, default: 0
  attribute :total_spend, default: 0
  attribute :models, default: -> { [] }
  attribute :aliases, default: -> { JSON.parse("{}") }
  attribute :config, default: -> { JSON.parse("{}") }
  attribute :permissions, default: -> { JSON.parse("{}") }
  attribute :metadata, default: -> { JSON.parse("{}") }
  attribute :allowed_cache_controls, default: -> { [] }
  attribute :allowed_routes, default: -> { [] }
  attribute :policies, default: -> { [] }
  attribute :access_group_ids, default: -> { [] }
  attribute :model_spend, default: -> { JSON.parse("{}") }
  attribute :model_max_budget, default: -> { JSON.parse("{}") }
  attribute :budget_fallbacks, default: -> { JSON.parse("{}") }
  attribute :router_settings, default: -> { JSON.parse("{}") }
  attribute :rotation_count, default: 0
  attribute :auto_rotate, default: false

  validates :token, presence: true
end
