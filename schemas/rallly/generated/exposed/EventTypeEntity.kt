package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class EventTypeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EventTypeEntity>(EventTypeTable)

    var spaceId by EventTypeTable.spaceId
    var hostId by EventTypeTable.hostId
    var name by EventTypeTable.name
    var duration by EventTypeTable.duration
    var capacity by EventTypeTable.capacity
    var description by EventTypeTable.description
    var location by EventTypeTable.location
    var createdAt by EventTypeTable.createdAt
    var updatedAt by EventTypeTable.updatedAt
    var deleted by EventTypeTable.deleted
    var deletedAt by EventTypeTable.deletedAt
    var space by SpaceEntity referencedOn EventTypeTable.spaceId
    var host by UserEntity referencedOn EventTypeTable.hostId
    val slots by SheetSlotEntity referrersOn SheetSlotTable.eventTypeId
    val scheduledEvents by ScheduledEventEntity optionalReferrersOn ScheduledEventTable.eventTypeId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EventTypeTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
