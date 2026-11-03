package linkwarden.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object VerificationTokenTable : IdTable<String>("\"VerificationToken\"") {
    val identifier = text("identifier")
    override val id = text("token").entityId()
    val expires = pgTimestamp("expires", 3)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }

    init {
        uniqueIndex("\"VerificationToken_token_key\"", id)
        uniqueIndex("\"VerificationToken_identifier_token_key\"", identifier, id)
    }
}
