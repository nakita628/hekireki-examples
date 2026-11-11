package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMDeprecatedVerificationTokenTable : IdTable<String>("\"LiteLLM_DeprecatedVerificationToken\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val token = text("token").uniqueIndex("\"LiteLLM_DeprecatedVerificationToken_token_key\"")
    val activeTokenId = text("active_token_id")
    val revokeAt = pgTimestamp("revoke_at", 3)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_DeprecatedVerificationToken_pkey\"")

    init {
        index("\"LiteLLM_DeprecatedVerificationToken_token_revoke_at_idx\"", false, token, revokeAt)
        index("\"LiteLLM_DeprecatedVerificationToken_revoke_at_idx\"", false, revokeAt)
    }
}
