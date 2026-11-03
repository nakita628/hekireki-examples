package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ScheduleEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, ScheduleEntity>(ScheduleTable)

    var user by UserEntity referencedOn ScheduleTable.userId
    var userId by ScheduleTable.userId
    val eventType by EventTypeEntity optionalReferrersOn EventTypeTable.scheduleId
    val instantMeetingEvents by EventTypeEntity optionalReferrersOn EventTypeTable.instantMeetingScheduleId
    val restrictionSchedule by EventTypeEntity optionalReferrersOn EventTypeTable.restrictionScheduleId
    var name by ScheduleTable.name
    var timeZone by ScheduleTable.timeZone
    val availability by AvailabilityEntity optionalReferrersOn AvailabilityTable.scheduleId
    val host by HostEntity optionalReferrersOn HostTable.scheduleId
}
