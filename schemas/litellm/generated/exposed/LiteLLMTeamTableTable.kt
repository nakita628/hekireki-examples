package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMTeamTableTable : IdTable<String>("\"LiteLLM_TeamTable\"") {
    override val id = text("team_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val teamAlias = text("team_alias").nullable()
    val organizationId =
        optReference(
            "organization_id",
            LiteLLMOrganizationTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_TeamTable_organization_id_fkey\"",
        )
    val objectPermissionId =
        optReference(
            "object_permission_id",
            LiteLLMObjectPermissionTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_TeamTable_object_permission_id_fkey\"",
        )
    val admins = array("admins", TextColumnType()).nullable()
    val members = array("members", TextColumnType()).nullable()
    val membersWithRoles = jsonb<String>("members_with_roles", { it }, { it }).default("{}")
    val metadata = jsonb<String>("metadata", { it }, { it }).default("{}")
    val maxBudget = double("max_budget").nullable()
    val softBudget = double("soft_budget").nullable()
    val spend = double("spend").default(0.0)
    val models = array("models", TextColumnType()).nullable()
    val maxParallelRequests = integer("max_parallel_requests").nullable()
    val tpmLimit = long("tpm_limit").nullable()
    val rpmLimit = long("rpm_limit").nullable()
    val tpdLimit = long("tpd_limit").nullable()
    val budgetDuration = text("budget_duration").nullable()
    val budgetResetAt = pgTimestamp("budget_reset_at", 3).nullable()
    val blocked = bool("blocked").default(false)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val modelSpend = jsonb<String>("model_spend", { it }, { it }).default("{}")
    val modelMaxBudget = jsonb<String>("model_max_budget", { it }, { it }).default("{}")
    val routerSettings = jsonb<String>("router_settings", { it }, { it }).nullable().default("{}")
    val teamMemberPermissions = array("team_member_permissions", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val accessGroupIds = array("access_group_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val policies = array("policies", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val defaultTeamMemberModels = array("default_team_member_models", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val budgetLimits = jsonb<String>("budget_limits", { it }, { it }).nullable()
    val modelId =
        optReference(
            "model_id",
            LiteLLMModelTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_TeamTable_model_id_fkey\"",
        ).uniqueIndex("\"LiteLLM_TeamTable_model_id_key\"")
    val allowTeamGuardrailConfig = bool("allow_team_guardrail_config").default(false)
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_TeamTable_pkey\"")

    init {
        index("\"LiteLLM_TeamTable_organization_id_idx\"", false, organizationId)
        index("\"LiteLLM_TeamTable_team_alias_idx\"", false, teamAlias)
        index("\"LiteLLM_TeamTable_created_at_idx\"", false, createdAt)
    }
}
