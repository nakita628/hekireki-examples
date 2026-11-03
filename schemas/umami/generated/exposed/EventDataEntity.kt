package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass
import java.util.UUID

class EventDataEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, EventDataEntity>(EventDataTable)

    var websiteId by EventDataTable.websiteId
    var websiteEventId by EventDataTable.websiteEventId
    var dataKey by EventDataTable.dataKey
    var stringValue by EventDataTable.stringValue
    var numberValue by EventDataTable.numberValue
    var dateValue by EventDataTable.dateValue
    var dataType by EventDataTable.dataType
    var createdAt by EventDataTable.createdAt
}
