package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class HostEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<HostEntity>(HostTable)

    val user by UserEntity referencedOn HostTable.userId
    val userId by HostTable.userId
    val eventType by EventTypeEntity referencedOn HostTable.eventTypeId
    val eventTypeId by HostTable.eventTypeId
    var isFixed by HostTable.isFixed
    var priority by HostTable.priority
    var weight by HostTable.weight
    var weightAdjustment by HostTable.weightAdjustment
    var schedule by ScheduleEntity optionalReferencedOn HostTable.scheduleId
    var scheduleId by HostTable.scheduleId
    var createdAt by HostTable.createdAt
    var group by HostGroupEntity optionalReferencedOn HostTable.groupId
    var groupId by HostTable.groupId
    var memberId by HostTable.memberId
    var member by MembershipEntity optionalReferencedOn HostTable.memberId
    val location by HostLocationEntity optionalBackReferencedOn HostLocationTable
}
