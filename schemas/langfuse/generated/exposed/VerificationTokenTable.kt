package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object VerificationTokenTable : IdTable<String>("verification_tokens") {
    val identifier = text("identifier")
    override val id = text("token").entityId()
    val expires = pgTimestamp("expires", 3)

    init {
        uniqueIndex("verification_tokens_token_key", id)
        uniqueIndex("verification_tokens_identifier_token_key", identifier, id)
    }
}
