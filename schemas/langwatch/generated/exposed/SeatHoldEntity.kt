package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SeatHoldEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SeatHoldEntity>(SeatHoldTable)

    var organizationId by SeatHoldTable.organizationId
    var userId by SeatHoldTable.userId
    var email by SeatHoldTable.email
    var name by SeatHoldTable.name
    var seatTakenAt by SeatHoldTable.seatTakenAt
    var heldUntil by SeatHoldTable.heldUntil
    var reason by SeatHoldTable.reason
    var createdAt by SeatHoldTable.createdAt
}
