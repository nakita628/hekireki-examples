package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object IdentityMatchTable : IdTable<String>("\"IdentityMatch\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val discoveredPersonId = text("discoveredPersonId")
    val userId = text("userId").nullable()
    val evidenceKind = text("evidenceKind")
    val validFrom = pgTimestamp("validFrom", 3)
    val validTo = pgTimestamp("validTo", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"IdentityMatch_pkey\"")

    init {
        index("\"IdentityMatch_organizationId_discoveredPersonId_idx\"", false, organizationId, discoveredPersonId)
        index("\"IdentityMatch_organizationId_userId_idx\"", false, organizationId, userId)
    }
}
