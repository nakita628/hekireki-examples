package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMVerificationTokenTable : IdTable<String>("\"LiteLLM_VerificationToken\"") {
    override val id = text("token").entityId()
    val keyName = text("key_name").nullable()
    val keyAlias = text("key_alias").nullable()
    val softBudgetCooldown = bool("soft_budget_cooldown").default(false)
    val spend = double("spend").default(0.0)
    val totalSpend = double("total_spend").default(0.0)
    val expires = pgTimestamp("expires", 3).nullable()
    val models = array("models", TextColumnType()).nullable()
    val aliases = jsonb<String>("aliases", { it }, { it }).default("{}")
    val config = jsonb<String>("config", { it }, { it }).default("{}")
    val routerSettings = jsonb<String>("router_settings", { it }, { it }).nullable().default("{}")
    val userId = text("user_id").nullable()
    val teamId = text("team_id").nullable()
    val agentId = text("agent_id").nullable()
    val projectId =
        optReference(
            "project_id",
            LiteLLMProjectTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_VerificationToken_project_id_fkey\"",
        )
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
    val budgetId =
        optReference(
            "budget_id",
            LiteLLMBudgetTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_VerificationToken_budget_id_fkey\"",
        )
    val organizationId =
        optReference(
            "organization_id",
            LiteLLMOrganizationTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_VerificationToken_organization_id_fkey\"",
        )
    val objectPermissionId =
        optReference(
            "object_permission_id",
            LiteLLMObjectPermissionTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_VerificationToken_object_permission_id_fkey\"",
        )
    val createdAt =
        pgTimestamp("created_at", 3).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by").nullable()
    val updatedAt =
        pgTimestamp("updated_at", 3).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by").nullable()
    val settingsUpdatedAt = pgTimestamp("settings_updated_at", 3).nullable()
    val lastActive = pgTimestamp("last_active", 3).nullable()
    val rotationCount = integer("rotation_count").nullable().default(0)
    val autoRotate = bool("auto_rotate").nullable().default(false)
    val rotationInterval = text("rotation_interval").nullable()
    val lastRotationAt = pgTimestamp("last_rotation_at", 3).nullable()
    val keyRotationAt = pgTimestamp("key_rotation_at", 3).nullable()
    val budgetLimits = jsonb<String>("budget_limits", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_VerificationToken_pkey\"")

    init {
        index("\"LiteLLM_VerificationToken_user_id_team_id_idx\"", false, userId, teamId)
        index("\"LiteLLM_VerificationToken_team_id_idx\"", false, teamId)
        index("\"LiteLLM_VerificationToken_budget_reset_at_expires_idx\"", false, budgetResetAt, expires)
    }
}
