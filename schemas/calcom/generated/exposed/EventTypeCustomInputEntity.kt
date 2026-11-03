package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class EventTypeCustomInputEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, EventTypeCustomInputEntity>(EventTypeCustomInputTable)

    var eventTypeId by EventTypeCustomInputTable.eventTypeId
    var eventType by EventTypeEntity referencedOn EventTypeCustomInputTable.eventTypeId
    var label by EventTypeCustomInputTable.label
    var type by EventTypeCustomInputTable.type
    var options by EventTypeCustomInputTable.options1
    var required by EventTypeCustomInputTable.required
    var placeholder by EventTypeCustomInputTable.placeholder
}
