package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMUserTableTable : IdTable<String>("\"LiteLLM_UserTable\"") {
    override val id = text("user_id").entityId()
    val userAlias = text("user_alias").nullable()
    val teamId = text("team_id").nullable()
    val ssoUserId = text("sso_user_id").nullable().uniqueIndex("\"LiteLLM_UserTable_sso_user_id_key\"")
    val organizationId =
        optReference(
            "organization_id",
            LiteLLMOrganizationTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_UserTable_organization_id_fkey\"",
        )
    val objectPermissionId =
        optReference(
            "object_permission_id",
            LiteLLMObjectPermissionTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_UserTable_object_permission_id_fkey\"",
        )
    val password = text("password").nullable()
    val passwordResetRequired = bool("password_reset_required").nullable()
    val lastBreachCheckAt = pgTimestamp("last_breach_check_at", 3).nullable()
    val teams = array("teams", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val userRole = text("user_role").nullable()
    val maxBudget = double("max_budget").nullable()
    val spend = double("spend").default(0.0)
    val userEmail = text("user_email").nullable()
    val models = array("models", TextColumnType()).nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).default("{}")
    val maxParallelRequests = integer("max_parallel_requests").nullable()
    val tpmLimit = long("tpm_limit").nullable()
    val rpmLimit = long("rpm_limit").nullable()
    val budgetDuration = text("budget_duration").nullable()
    val budgetResetAt = pgTimestamp("budget_reset_at", 3).nullable()
    val allowedCacheControls = array("allowed_cache_controls", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val policies = array("policies", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val modelSpend = jsonb<String>("model_spend", { it }, { it }).default("{}")
    val modelMaxBudget = jsonb<String>("model_max_budget", { it }, { it }).default("{}")
    val createdAt =
        pgTimestamp("created_at", 3).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt =
        pgTimestamp("updated_at", 3).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_UserTable_pkey\"")
}
