package litellm.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMDeletedTeamTableTable : IdTable<String>("\"LiteLLM_DeletedTeamTable\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val teamId = text("team_id")
    val teamAlias = text("team_alias").nullable()
    val organizationId = text("organization_id").nullable()
    val objectPermissionId = text("object_permission_id").nullable()
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
    val modelSpend = jsonb<String>("model_spend", { it }, { it }).default("{}")
    val modelMaxBudget = jsonb<String>("model_max_budget", { it }, { it }).default("{}")
    val routerSettings = jsonb<String>("router_settings", { it }, { it }).nullable().default("{}")
    val teamMemberPermissions = array("team_member_permissions", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val accessGroupIds = array("access_group_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val policies = array("policies", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val modelId = integer("model_id").nullable()
    val allowTeamGuardrailConfig = bool("allow_team_guardrail_config").default(false)
    val createdAt = pgTimestamp("created_at", 3).nullable()
    val updatedAt = pgTimestamp("updated_at", 3).nullable()
    val deletedAt = pgTimestamp("deleted_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedBy = text("deleted_by").nullable()
    val deletedByApiKey = text("deleted_by_api_key").nullable()
    val litellmChangedBy = text("litellm_changed_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_DeletedTeamTable_pkey\"")

    init {
        index("\"LiteLLM_DeletedTeamTable_team_id_idx\"", false, teamId)
        index("\"LiteLLM_DeletedTeamTable_deleted_at_idx\"", false, deletedAt)
        index("\"LiteLLM_DeletedTeamTable_organization_id_idx\"", false, organizationId)
        index("\"LiteLLM_DeletedTeamTable_team_alias_idx\"", false, teamAlias)
        index("\"LiteLLM_DeletedTeamTable_created_at_idx\"", false, createdAt)
    }
}
