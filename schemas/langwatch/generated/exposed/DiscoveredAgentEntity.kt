package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DiscoveredAgentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DiscoveredAgentEntity>(DiscoveredAgentTable)

    var organizationId by DiscoveredAgentTable.organizationId
    var provider by DiscoveredAgentTable.provider
    var rawAgentId by DiscoveredAgentTable.rawAgentId
    var displayText by DiscoveredAgentTable.displayText
    var metadata by DiscoveredAgentTable.metadata
    var firstSeenAt by DiscoveredAgentTable.firstSeenAt
    var lastSeenAt by DiscoveredAgentTable.lastSeenAt
    var createdAt by DiscoveredAgentTable.createdAt
    var updatedAt by DiscoveredAgentTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DiscoveredAgentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
