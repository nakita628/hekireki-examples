package langwatch.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SessionTable : IdTable<String>("\"Session\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val sessionToken = text("sessionToken").uniqueIndex("\"Session_sessionToken_key\"")
    val userId = text("userId")
    val expires = pgTimestamp("expires", 3)
    val ipAddress = text("ipAddress").nullable()
    val userAgent = text("userAgent").nullable()
    val identifierId = text("identifierId").nullable()
    val amr = array("amr", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val startedBySignUp = bool("startedBySignUp").default(false)
    val impersonating = jsonb<String>("impersonating", { it }, { it }).nullable()
    val actorUserId = text("actorUserId").nullable()
    val subjectUserId = text("subjectUserId").nullable()
    val impersonationReason = text("impersonationReason").nullable()
    val impersonationExpiresAt = pgTimestamp("impersonationExpiresAt", 3).nullable()
    val lastSeenAt = pgTimestamp("lastSeenAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Session_pkey\"")

    init {
        index("\"Session_userId_idx\"", false, userId)
        index("\"Session_identifierId_idx\"", false, identifierId)
    }
}
