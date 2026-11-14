class GatewayChangeEvent < ApplicationRecord
  self.table_name = "GatewayChangeEvent"
  self.primary_key = "revision"

  alias_attribute :created_at, :createdAt

  enum :kind, { vk_created: "VK_CREATED", vk_config_updated: "VK_CONFIG_UPDATED", vk_revoked: "VK_REVOKED", vk_rotated: "VK_ROTATED", vk_disabled: "VK_DISABLED", vk_enabled: "VK_ENABLED", budget_created: "BUDGET_CREATED", budget_updated: "BUDGET_UPDATED", budget_deleted: "BUDGET_DELETED", model_provider_updated: "MODEL_PROVIDER_UPDATED", cache_rule_created: "CACHE_RULE_CREATED", cache_rule_updated: "CACHE_RULE_UPDATED", cache_rule_deleted: "CACHE_RULE_DELETED", routing_policy_updated: "ROUTING_POLICY_UPDATED", routing_policy_deleted: "ROUTING_POLICY_DELETED" },
       validate: true

  belongs_to :organization, foreign_key: "organizationId", inverse_of: :gateway_change_events
  belongs_to :project, foreign_key: "projectId", optional: true, inverse_of: :gateway_change_events
end
