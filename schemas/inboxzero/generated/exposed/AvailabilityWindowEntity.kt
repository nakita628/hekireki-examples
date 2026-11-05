package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AvailabilityWindowEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AvailabilityWindowEntity>(AvailabilityWindowTable)

    var createdAt by AvailabilityWindowTable.createdAt
    var updatedAt by AvailabilityWindowTable.updatedAt
    var weekday by AvailabilityWindowTable.weekday
    var startMinutes by AvailabilityWindowTable.startMinutes
    var endMinutes by AvailabilityWindowTable.endMinutes
    var availabilityScheduleId by AvailabilityWindowTable.availabilityScheduleId
    var availabilitySchedule by AvailabilityScheduleEntity referencedOn AvailabilityWindowTable.availabilityScheduleId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AvailabilityWindowTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
