package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ScimSyncStateEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScimSyncStateEntity>(ScimSyncStateTable)

    var connectionId by ScimSyncStateTable.connectionId
    var organizationId by ScimSyncStateTable.organizationId
    var state by ScimSyncStateTable.state
    var lastPushedAt by ScimSyncStateTable.lastPushedAt
    var lastFailure by ScimSyncStateTable.lastFailure
    var deadLetters by ScimSyncStateTable.deadLetters
    var revokedCause by ScimSyncStateTable.revokedCause
    var occurredAt by ScimSyncStateTable.occurredAt
    var lastEventId by ScimSyncStateTable.lastEventId
    var acceptedAt by ScimSyncStateTable.acceptedAt
    var projectionVersion by ScimSyncStateTable.projectionVersion
    var createdAt by ScimSyncStateTable.createdAt
    var updatedAt by ScimSyncStateTable.updatedAt
}
