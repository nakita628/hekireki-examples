package linkwarden.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object EmailVerificationCodeTable : IdTable<String>("\"EmailVerificationCode\"") {
    override val id = text("identifier").entityId()
    val code = text("code")
    val attempts = integer("attempts").default(0)
    val expires = pgTimestamp("expires", 3)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }

    init {
        uniqueIndex("\"EmailVerificationCode_identifier_key\"", id)
    }
}
