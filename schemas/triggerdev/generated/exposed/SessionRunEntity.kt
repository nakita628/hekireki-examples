package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SessionRunEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SessionRunEntity>(SessionRunTable)

    var sessionId by SessionRunTable.sessionId
    var runId by SessionRunTable.runId
    var reason by SessionRunTable.reason
    var triggeredAt by SessionRunTable.triggeredAt
    var session by SessionEntity referencedOn SessionRunTable.sessionId
}
