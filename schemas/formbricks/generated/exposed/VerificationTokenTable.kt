package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object VerificationTokenTable : CompositeIdTable("\"VerificationToken\"") {
    val identifier = text("identifier").entityId()
    val token = text("token").entityId()
    val expires = pgTimestamp("expires", 3)

    init {
        uniqueIndex("\"VerificationToken_identifier_token_key\"", identifier, token)
    }
}
