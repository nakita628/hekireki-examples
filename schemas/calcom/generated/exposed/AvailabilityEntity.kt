package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AvailabilityEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, AvailabilityEntity>(AvailabilityTable)

    var user by UserEntity optionalReferencedOn AvailabilityTable.userId
    var userId by AvailabilityTable.userId
    var eventType by EventTypeEntity optionalReferencedOn AvailabilityTable.eventTypeId
    var eventTypeId by AvailabilityTable.eventTypeId
    var days by AvailabilityTable.days
    var startTime by AvailabilityTable.startTime
    var endTime by AvailabilityTable.endTime
    var date by AvailabilityTable.date
    var schedule by ScheduleEntity optionalReferencedOn AvailabilityTable.scheduleId
    var scheduleId by AvailabilityTable.scheduleId
}
