package litellm.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMPolicyAttachmentTableTable : IdTable<String>("\"LiteLLM_PolicyAttachmentTable\"") {
    override val id = text("attachment_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val policyName = text("policy_name")
    val scope = text("scope").nullable()
    val teams = array("teams", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val keys = array("keys", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val models = array("models", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val tags = array("tags", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val priority = integer("priority").nullable()
    val isDefault = bool("is_default").default(false)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by").nullable()
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_PolicyAttachmentTable_pkey\"")
}
