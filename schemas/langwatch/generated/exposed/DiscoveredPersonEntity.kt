package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DiscoveredPersonEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DiscoveredPersonEntity>(DiscoveredPersonTable)

    var organizationId by DiscoveredPersonTable.organizationId
    var provider by DiscoveredPersonTable.provider
    var rawActorId by DiscoveredPersonTable.rawActorId
    var displayText by DiscoveredPersonTable.displayText
    var kind by DiscoveredPersonTable.kind
    var department by DiscoveredPersonTable.department
    var firstSeenAt by DiscoveredPersonTable.firstSeenAt
    var lastSeenAt by DiscoveredPersonTable.lastSeenAt
    var erasedAt by DiscoveredPersonTable.erasedAt
    var moneyRowsPendingAt by DiscoveredPersonTable.moneyRowsPendingAt
    var moneyRebuildSince by DiscoveredPersonTable.moneyRebuildSince
    var suspendedAt by DiscoveredPersonTable.suspendedAt
    var suspendedReason by DiscoveredPersonTable.suspendedReason
    var createdAt by DiscoveredPersonTable.createdAt
    var updatedAt by DiscoveredPersonTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DiscoveredPersonTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
