package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class RescheduledEventDateEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RescheduledEventDateEntity>(RescheduledEventDateTable)

    var scheduledEventId by RescheduledEventDateTable.scheduledEventId
    var start by RescheduledEventDateTable.start
    var end by RescheduledEventDateTable.end
    var allDay by RescheduledEventDateTable.allDay
    var createdAt by RescheduledEventDateTable.createdAt
    var scheduledEvent by ScheduledEventEntity referencedOn RescheduledEventDateTable.scheduledEventId
}
