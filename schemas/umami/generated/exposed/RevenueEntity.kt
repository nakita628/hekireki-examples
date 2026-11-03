package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass
import java.util.UUID

class RevenueEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, RevenueEntity>(RevenueTable)

    var websiteId by RevenueTable.websiteId
    var sessionId by RevenueTable.sessionId
    var eventId by RevenueTable.eventId
    var eventName by RevenueTable.eventName
    var currency by RevenueTable.currency
    var revenue by RevenueTable.revenue
    var createdAt by RevenueTable.createdAt
}
