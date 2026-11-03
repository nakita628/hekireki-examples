package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AssignmentReasonEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, AssignmentReasonEntity>(AssignmentReasonTable)

    var createdAt by AssignmentReasonTable.createdAt
    var bookingId by AssignmentReasonTable.bookingId
    var booking by BookingEntity referencedOn AssignmentReasonTable.bookingId
    var reasonEnum by AssignmentReasonTable.reasonEnum
    var reasonString by AssignmentReasonTable.reasonString
}
