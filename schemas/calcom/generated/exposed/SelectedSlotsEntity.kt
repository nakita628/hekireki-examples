package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SelectedSlotsEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, SelectedSlotsEntity>(SelectedSlotsTable)

    var eventTypeId by SelectedSlotsTable.eventTypeId
    var userId by SelectedSlotsTable.userId
    var slotUtcStartDate by SelectedSlotsTable.slotUtcStartDate
    var slotUtcEndDate by SelectedSlotsTable.slotUtcEndDate
    var uid by SelectedSlotsTable.uid
    var releaseAt by SelectedSlotsTable.releaseAt
    var isSeat by SelectedSlotsTable.isSeat
}
