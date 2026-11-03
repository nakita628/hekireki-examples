package umami.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class SessionLinkEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<SessionLinkEntity>(SessionLinkTable)

    val websiteId by SessionLinkTable.websiteId
    val sessionId by SessionLinkTable.sessionId
    val distinctId by SessionLinkTable.distinctId
    var createdAt by SessionLinkTable.createdAt
}
