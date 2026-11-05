package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ScheduleEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScheduleEntity>(ScheduleTable)

    var createdAt by ScheduleTable.createdAt
    var updatedAt by ScheduleTable.updatedAt
    var intervalDays by ScheduleTable.intervalDays
    var occurrences by ScheduleTable.occurrences
    var daysOfWeek by ScheduleTable.daysOfWeek
    var timeOfDay by ScheduleTable.timeOfDay
    var emailAccountId by ScheduleTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn ScheduleTable.emailAccountId
    var lastOccurrenceAt by ScheduleTable.lastOccurrenceAt
    var nextOccurrenceAt by ScheduleTable.nextOccurrenceAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ScheduleTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
