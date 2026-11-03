package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object VerificationTokenTable : IdTable<String>("\"VerificationToken\"") {
    val identifier = text("identifier")
    override val id = text("token").entityId()
    val value = text("value").nullable()
    val failedAttempts = integer("failedAttempts").default(0)
    val expires = pgTimestamp("expires", 3)

    init {
        uniqueIndex("\"VerificationToken_token_key\"", id)
        uniqueIndex("\"VerificationToken_identifier_token_key\"", identifier, id)
    }
}
