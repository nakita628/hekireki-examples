package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class IdentifierReservationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, IdentifierReservationEntity>(IdentifierReservationTable)

    var userId by IdentifierReservationTable.userId
    var identifierId by IdentifierReservationTable.identifierId
    var commandId by IdentifierReservationTable.commandId
    var createdAt by IdentifierReservationTable.createdAt
}
