package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class BookingAuditEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, BookingAuditEntity>(BookingAuditTable)

    var bookingUid by BookingAuditTable.bookingUid
    var actorId by BookingAuditTable.actorId
    var actor by AuditActorEntity referencedOn BookingAuditTable.actorId
    var type by BookingAuditTable.type
    var action by BookingAuditTable.action
    var timestamp by BookingAuditTable.timestamp
    var createdAt by BookingAuditTable.createdAt
    var updatedAt by BookingAuditTable.updatedAt
    var source by BookingAuditTable.source1
    var operationId by BookingAuditTable.operationId
    var data by BookingAuditTable.data
    var context by BookingAuditTable.context

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BookingAuditTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
