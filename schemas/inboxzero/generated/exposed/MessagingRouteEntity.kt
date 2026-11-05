package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class MessagingRouteEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MessagingRouteEntity>(MessagingRouteTable)

    var createdAt by MessagingRouteTable.createdAt
    var updatedAt by MessagingRouteTable.updatedAt
    var purpose by MessagingRouteTable.purpose
    var targetType by MessagingRouteTable.targetType
    var targetId by MessagingRouteTable.targetId
    var messagingChannelId by MessagingRouteTable.messagingChannelId
    var messagingChannel by MessagingChannelEntity referencedOn MessagingRouteTable.messagingChannelId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MessagingRouteTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
