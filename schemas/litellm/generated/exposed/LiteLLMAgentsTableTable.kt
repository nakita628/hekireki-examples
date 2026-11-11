package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMAgentsTableTable : IdTable<String>("\"LiteLLM_AgentsTable\"") {
    override val id = text("agent_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val agentName = text("agent_name").uniqueIndex("\"LiteLLM_AgentsTable_agent_name_key\"")
    val litellmParams = jsonb<String>("litellm_params", { it }, { it }).nullable()
    val agentCardParams = jsonb<String>("agent_card_params", { it }, { it })
    val staticHeaders = jsonb<String>("static_headers", { it }, { it }).nullable().default("{}")
    val extraHeaders = array("extra_headers", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val killSwitch = jsonb<String>("kill_switch", { it }, { it }).nullable()
    val agentAccessGroups = array("agent_access_groups", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val accessGroupIds = array("access_group_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val objectPermissionId =
        optReference(
            "object_permission_id",
            LiteLLMObjectPermissionTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_AgentsTable_object_permission_id_fkey\"",
        )
    val spend = double("spend").default(0.0)
    val identityManaged = bool("identity_managed").default(false)
    val enabled = bool("enabled").default(true)
    val executionMode = text("execution_mode").default("autonomous")
    val tpmLimit = integer("tpm_limit").nullable()
    val rpmLimit = integer("rpm_limit").nullable()
    val sessionTpmLimit = integer("session_tpm_limit").nullable()
    val sessionRpmLimit = integer("session_rpm_limit").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by")
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_AgentsTable_pkey\"")
}
