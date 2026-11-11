package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMUserProviderCredentialsTable : IdTable<String>("\"LiteLLM_UserProviderCredentials\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val userId = text("user_id")
    val credentialName = text("credential_name")
    val provider = text("provider")
    val credentialB64 = text("credential_b64")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_UserProviderCredentials_pkey\"")

    init {
        index("\"LiteLLM_UserProviderCredentials_credential_name_idx\"", false, credentialName)
        uniqueIndex("\"LiteLLM_UserProviderCredentials_user_id_credential_name_key\"", userId, credentialName)
    }
}
