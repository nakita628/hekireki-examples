package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DiscoveredPersonTable : IdTable<String>("\"DiscoveredPerson\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val provider = text("provider")
    val rawActorId = text("rawActorId")
    val displayText = text("displayText")
    val kind = text("kind")
    val department = text("department").nullable()
    val firstSeenAt = pgTimestamp("firstSeenAt", 3)
    val lastSeenAt = pgTimestamp("lastSeenAt", 3)
    val erasedAt = pgTimestamp("erasedAt", 3).nullable()
    val moneyRowsPendingAt = pgTimestamp("moneyRowsPendingAt", 3).nullable()
    val moneyRebuildSince = text("moneyRebuildSince").nullable()
    val suspendedAt = pgTimestamp("suspendedAt", 3).nullable()
    val suspendedReason = text("suspendedReason").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DiscoveredPerson_pkey\"")

    init {
        index("\"DiscoveredPerson_organizationId_lastSeenAt_idx\"", false, organizationId, lastSeenAt)
        uniqueIndex("\"DiscoveredPerson_organizationId_provider_rawActorId_key\"", organizationId, provider, rawActorId)
    }
}
