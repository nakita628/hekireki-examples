package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMSSOIdentityAssertionTable : IdTable<String>("\"LiteLLM_SSOIdentityAssertion\"") {
    override val id = text("user_id").entityId()
    val assertionB64 = text("assertion_b64")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_SSOIdentityAssertion_pkey\"")
}
