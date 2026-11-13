package umami.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class ErrorEventEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<ErrorEventEntity>(ErrorEventTable)

    val websiteId by ErrorEventTable.websiteId
    val id1 by ErrorEventTable.id1
    var issueId by ErrorEventTable.issueId
    var sessionId by ErrorEventTable.sessionId
    var visitId by ErrorEventTable.visitId
    var createdAt by ErrorEventTable.createdAt
    var receivedAt by ErrorEventTable.receivedAt
    var name by ErrorEventTable.name
    var message by ErrorEventTable.message
    var stack by ErrorEventTable.stack
    var frames by ErrorEventTable.frames
    var tags by ErrorEventTable.tags
    var handled by ErrorEventTable.handled
    var urlPath by ErrorEventTable.urlPath
    var release by ErrorEventTable.release
    var environment by ErrorEventTable.environment
    var browser by ErrorEventTable.browser
    var os by ErrorEventTable.os
    var device by ErrorEventTable.device
}
