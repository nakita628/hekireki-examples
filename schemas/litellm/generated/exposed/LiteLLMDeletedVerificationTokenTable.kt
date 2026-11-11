package litellm.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMDeletedVerificationTokenTable : IdTable<String>("\"LiteLLM_DeletedVerificationToken\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val token = text("token")
    val keyName = text("key_name").nullable()
    val keyAlias = text("key_alias").nullable()
    val softBudgetCooldown = bool("soft_budget_cooldown").default(false)
    val spend = double("spend").default(0.0)
    val totalSpend = double("total_spend").default(0.0)
    val expires = pgTimestamp("expires", 3).nullable()
    val models = array("models", TextColumnType()).nullable()
    val aliases = jsonb<String>("aliases", { it }, { it }).default("{}")
    val config = jsonb<String>("config", { it }, { it }).default("{}")
    val userId = text("user_id").nullable()
    val teamId = text("team_id").nullable()
    val agentId = text("agent_id").nullable()
    val projectId = text("project_id").nullable()
    val permissions = jsonb<String>("permissions", { it }, { it }).default("{}")
    val maxParallelRequests = integer("max_parallel_requests").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).default("{}")
    val blocked = bool("blocked").nullable()
    val tpmLimit = long("tpm_limit").nullable()
    val rpmLimit = long("rpm_limit").nullable()
    val tpdLimit = long("tpd_limit").nullable()
    val maxBudget = double("max_budget").nullable()
    val budgetDuration = text("budget_duration").nullable()
    val budgetResetAt = pgTimestamp("budget_reset_at", 3).nullable()
    val allowedCacheControls = array("allowed_cache_controls", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val allowedRoutes = array("allowed_routes", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val keyType = text("key_type").nullable()
    val policies = array("policies", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val accessGroupIds = array("access_group_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val modelSpend = jsonb<String>("model_spend", { it }, { it }).default("{}")
    val modelMaxBudget = jsonb<String>("model_max_budget", { it }, { it }).default("{}")
    val budgetFallbacks = jsonb<String>("budget_fallbacks", { it }, { it }).default("{}")
    val routerSettings = jsonb<String>("router_settings", { it }, { it }).nullable().default("{}")
    val budgetId = text("budget_id").nullable()
    val organizationId = text("organization_id").nullable()
    val objectPermissionId = text("object_permission_id").nullable()
    val createdAt = pgTimestamp("created_at", 3).nullable()
    val createdBy = text("created_by").nullable()
    val updatedAt = pgTimestamp("updated_at", 3).nullable()
    val updatedBy = text("updated_by").nullable()
    val settingsUpdatedAt = pgTimestamp("settings_updated_at", 3).nullable()
    val lastActive = pgTimestamp("last_active", 3).nullable()
    val rotationCount = integer("rotation_count").nullable().default(0)
    val autoRotate = bool("auto_rotate").nullable().default(false)
    val rotationInterval = text("rotation_interval").nullable()
    val lastRotationAt = pgTimestamp("last_rotation_at", 3).nullable()
    val keyRotationAt = pgTimestamp("key_rotation_at", 3).nullable()
    val deletedAt = pgTimestamp("deleted_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedBy = text("deleted_by").nullable()
    val deletedByApiKey = text("deleted_by_api_key").nullable()
    val litellmChangedBy = text("litellm_changed_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_DeletedVerificationToken_pkey\"")

    init {
        index("\"LiteLLM_DeletedVerificationToken_token_idx\"", false, token)
        index("\"LiteLLM_DeletedVerificationToken_deleted_at_idx\"", false, deletedAt)
        index("\"LiteLLM_DeletedVerificationToken_user_id_idx\"", false, userId)
        index("\"LiteLLM_DeletedVerificationToken_team_id_idx\"", false, teamId)
        index("\"LiteLLM_DeletedVerificationToken_organization_id_idx\"", false, organizationId)
        index("\"LiteLLM_DeletedVerificationToken_key_alias_idx\"", false, keyAlias)
        index("\"LiteLLM_DeletedVerificationToken_created_at_idx\"", false, createdAt)
    }
}
