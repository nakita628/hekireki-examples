package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object DiscoveredAgentTable : IdTable<String>("\"DiscoveredAgent\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val provider = text("provider")
    val rawAgentId = text("rawAgentId")
    val displayText = text("displayText")
    val metadata = jsonb<String>("metadata", { it }, { it })
    val firstSeenAt = pgTimestamp("firstSeenAt", 3)
    val lastSeenAt = pgTimestamp("lastSeenAt", 3)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DiscoveredAgent_pkey\"")

    init {
        index("\"DiscoveredAgent_organizationId_lastSeenAt_idx\"", false, organizationId, lastSeenAt)
        uniqueIndex("\"DiscoveredAgent_organizationId_provider_rawAgentId_key\"", organizationId, provider, rawAgentId)
    }
}
