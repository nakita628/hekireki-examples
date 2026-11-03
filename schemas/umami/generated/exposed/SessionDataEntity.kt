package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass
import java.util.UUID

class SessionDataEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, SessionDataEntity>(SessionDataTable)

    var websiteId by SessionDataTable.websiteId
    var sessionId by SessionDataTable.sessionId
    var dataKey by SessionDataTable.dataKey
    var stringValue by SessionDataTable.stringValue
    var numberValue by SessionDataTable.numberValue
    var dateValue by SessionDataTable.dateValue
    var dataType by SessionDataTable.dataType
    var distinctId by SessionDataTable.distinctId
    var createdAt by SessionDataTable.createdAt
}
