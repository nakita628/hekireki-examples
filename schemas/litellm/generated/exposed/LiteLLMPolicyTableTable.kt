package litellm.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMPolicyTableTable : IdTable<String>("\"LiteLLM_PolicyTable\"") {
    override val id = text("policy_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val policyName = text("policy_name")
    val versionNumber = integer("version_number").default(1)
    val versionStatus = text("version_status").default("production")
    val parentVersionId = text("parent_version_id").nullable()
    val isLatest = bool("is_latest").default(true)
    val publishedAt = pgTimestamp("published_at", 3).nullable()
    val productionAt = pgTimestamp("production_at", 3).nullable()
    val inherit = text("inherit").nullable()
    val description = text("description").nullable()
    val guardrailsAdd = array("guardrails_add", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val guardrailsRemove = array("guardrails_remove", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val condition = jsonb<String>("condition", { it }, { it }).nullable().default("{}")
    val pipeline = jsonb<String>("pipeline", { it }, { it }).nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by").nullable()
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_PolicyTable_pkey\"")

    init {
        index("\"LiteLLM_PolicyTable_policy_name_version_status_idx\"", false, policyName, versionStatus)
        uniqueIndex("\"LiteLLM_PolicyTable_policy_name_version_number_key\"", policyName, versionNumber)
    }
}
