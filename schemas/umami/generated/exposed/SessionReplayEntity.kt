package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass
import java.util.UUID

class SessionReplayEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, SessionReplayEntity>(SessionReplayTable)

    var websiteId by SessionReplayTable.websiteId
    var sessionId by SessionReplayTable.sessionId
    var visitId by SessionReplayTable.visitId
    var chunkIndex by SessionReplayTable.chunkIndex
    var events by SessionReplayTable.events
    var eventCount by SessionReplayTable.eventCount
    var startedAt by SessionReplayTable.startedAt
    var endedAt by SessionReplayTable.endedAt
    var createdAt by SessionReplayTable.createdAt
}
