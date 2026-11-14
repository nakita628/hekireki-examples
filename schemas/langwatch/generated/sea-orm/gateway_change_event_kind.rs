use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, PartialEq, Eq, EnumIter, DeriveActiveEnum, Serialize, Deserialize)]
#[sea_orm(
    rs_type = "String",
    db_type = "Enum",
    enum_name = "GatewayChangeEventKind"
)]
pub enum GatewayChangeEventKind {
    #[sea_orm(string_value = "VK_CREATED")]
    VkCreated,
    #[sea_orm(string_value = "VK_CONFIG_UPDATED")]
    VkConfigUpdated,
    #[sea_orm(string_value = "VK_REVOKED")]
    VkRevoked,
    #[sea_orm(string_value = "VK_ROTATED")]
    VkRotated,
    #[sea_orm(string_value = "VK_DISABLED")]
    VkDisabled,
    #[sea_orm(string_value = "VK_ENABLED")]
    VkEnabled,
    #[sea_orm(string_value = "BUDGET_CREATED")]
    BudgetCreated,
    #[sea_orm(string_value = "BUDGET_UPDATED")]
    BudgetUpdated,
    #[sea_orm(string_value = "BUDGET_DELETED")]
    BudgetDeleted,
    #[sea_orm(string_value = "MODEL_PROVIDER_UPDATED")]
    ModelProviderUpdated,
    #[sea_orm(string_value = "CACHE_RULE_CREATED")]
    CacheRuleCreated,
    #[sea_orm(string_value = "CACHE_RULE_UPDATED")]
    CacheRuleUpdated,
    #[sea_orm(string_value = "CACHE_RULE_DELETED")]
    CacheRuleDeleted,
    #[sea_orm(string_value = "ROUTING_POLICY_UPDATED")]
    RoutingPolicyUpdated,
    #[sea_orm(string_value = "ROUTING_POLICY_DELETED")]
    RoutingPolicyDeleted,
}
