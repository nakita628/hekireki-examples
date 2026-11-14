#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum GatewayChangeEventKind
{
    [PgName("VK_CREATED")]
    VkCreated,

    [PgName("VK_CONFIG_UPDATED")]
    VkConfigUpdated,

    [PgName("VK_REVOKED")]
    VkRevoked,

    [PgName("VK_ROTATED")]
    VkRotated,

    [PgName("VK_DISABLED")]
    VkDisabled,

    [PgName("VK_ENABLED")]
    VkEnabled,

    [PgName("BUDGET_CREATED")]
    BudgetCreated,

    [PgName("BUDGET_UPDATED")]
    BudgetUpdated,

    [PgName("BUDGET_DELETED")]
    BudgetDeleted,

    [PgName("MODEL_PROVIDER_UPDATED")]
    ModelProviderUpdated,

    [PgName("CACHE_RULE_CREATED")]
    CacheRuleCreated,

    [PgName("CACHE_RULE_UPDATED")]
    CacheRuleUpdated,

    [PgName("CACHE_RULE_DELETED")]
    CacheRuleDeleted,

    [PgName("ROUTING_POLICY_UPDATED")]
    RoutingPolicyUpdated,

    [PgName("ROUTING_POLICY_DELETED")]
    RoutingPolicyDeleted,
}
