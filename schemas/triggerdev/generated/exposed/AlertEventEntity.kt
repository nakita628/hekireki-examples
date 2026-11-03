package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class AlertEventEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<AlertEventEntity>(AlertEventTable)

    val id1 by AlertEventTable.id1
    var alertId by AlertEventTable.alertId
    var type by AlertEventTable.type
    val at by AlertEventTable.at
    var facts by AlertEventTable.facts
    var actorId by AlertEventTable.actorId
    var sourceEventId by AlertEventTable.sourceEventId
}
