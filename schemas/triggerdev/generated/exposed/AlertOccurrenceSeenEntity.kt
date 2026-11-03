package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class AlertOccurrenceSeenEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<AlertOccurrenceSeenEntity>(AlertOccurrenceSeenTable)

    val alertId by AlertOccurrenceSeenTable.alertId
    val sourceEventId by AlertOccurrenceSeenTable.sourceEventId
    var rowTime by AlertOccurrenceSeenTable.rowTime
    var seenAt by AlertOccurrenceSeenTable.seenAt
    var expiresAt by AlertOccurrenceSeenTable.expiresAt
}
