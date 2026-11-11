package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMCredentialsTableTable : IdTable<String>("\"LiteLLM_CredentialsTable\"") {
    override val id = text("credential_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val credentialName = text("credential_name").uniqueIndex("\"LiteLLM_CredentialsTable_credential_name_key\"")
    val displayName = text("display_name").nullable()
    val credentialValues = jsonb<String>("credential_values", { it }, { it })
    val credentialInfo = jsonb<String>("credential_info", { it }, { it }).nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by")
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_CredentialsTable_pkey\"")
}
