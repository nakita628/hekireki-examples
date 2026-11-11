class LiteLLMVerificationToken < ApplicationRecord
  self.table_name = "LiteLLM_VerificationToken"
  self.primary_key = "token"

  attribute :soft_budget_cooldown, default: false
  attribute :spend, default: 0
  attribute :total_spend, default: 0
  attribute :models, default: -> { [] }
  attribute :aliases, default: -> { JSON.parse("{}") }
  attribute :config, default: -> { JSON.parse("{}") }
  attribute :router_settings, default: -> { JSON.parse("{}") }
  attribute :permissions, default: -> { JSON.parse("{}") }
  attribute :metadata, default: -> { JSON.parse("{}") }
  attribute :allowed_cache_controls, default: -> { [] }
  attribute :allowed_routes, default: -> { [] }
  attribute :policies, default: -> { [] }
  attribute :access_group_ids, default: -> { [] }
  attribute :model_spend, default: -> { JSON.parse("{}") }
  attribute :model_max_budget, default: -> { JSON.parse("{}") }
  attribute :budget_fallbacks, default: -> { JSON.parse("{}") }
  attribute :rotation_count, default: 0
  attribute :auto_rotate, default: false

  belongs_to :litellm_budget_table, class_name: "LiteLLMBudgetTable", foreign_key: "budget_id", optional: true,
                                    inverse_of: :keys
  belongs_to :litellm_organization_table, class_name: "LiteLLMOrganizationTable", foreign_key: "organization_id",
                                          optional: true, inverse_of: :keys
  belongs_to :litellm_project_table, class_name: "LiteLLMProjectTable", foreign_key: "project_id", optional: true,
                                     inverse_of: :keys
  belongs_to :object_permission, class_name: "LiteLLMObjectPermissionTable", optional: true,
                                 inverse_of: :verification_tokens
  has_many :jwt_key_mappings, class_name: "LiteLLMJWTKeyMapping", foreign_key: "token",
                              inverse_of: :litellm_verification_token, dependent: :destroy
end
