package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object VerificationTokenTable : IdTable<String>("\"VerificationToken\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val identifier = text("identifier")
    val token = text("token")
    val expires = pgTimestamp("expires", 3)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"VerificationToken_pkey\"")

    init {
        uniqueIndex("\"VerificationToken_identifier_token_key\"", identifier, token)
    }
}
