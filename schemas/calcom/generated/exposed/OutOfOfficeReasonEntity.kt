package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OutOfOfficeReasonEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, OutOfOfficeReasonEntity>(OutOfOfficeReasonTable)

    var emoji by OutOfOfficeReasonTable.emoji
    var reason by OutOfOfficeReasonTable.reason
    var enabled by OutOfOfficeReasonTable.enabled
    var userId by OutOfOfficeReasonTable.userId
    var user by UserEntity optionalReferencedOn OutOfOfficeReasonTable.userId
    val entries by OutOfOfficeEntryEntity optionalReferrersOn OutOfOfficeEntryTable.reasonId
}
