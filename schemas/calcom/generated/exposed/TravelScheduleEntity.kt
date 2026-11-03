package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TravelScheduleEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, TravelScheduleEntity>(TravelScheduleTable)

    var userId by TravelScheduleTable.userId
    var user by UserEntity referencedOn TravelScheduleTable.userId
    var timeZone by TravelScheduleTable.timeZone
    var startDate by TravelScheduleTable.startDate
    var endDate by TravelScheduleTable.endDate
    var prevTimeZone by TravelScheduleTable.prevTimeZone
}
