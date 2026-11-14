package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class JoinRequestEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, JoinRequestEntity>(JoinRequestTable)

    var userId by JoinRequestTable.userId
    var organizationId by JoinRequestTable.organizationId
    var domain by JoinRequestTable.domain
    var state by JoinRequestTable.state
    var matchedVia by JoinRequestTable.matchedVia
    var origin by JoinRequestTable.origin
    var expiresAt by JoinRequestTable.expiresAt
    var resolvedAt by JoinRequestTable.resolvedAt
    var resolvedByType by JoinRequestTable.resolvedByType
    var resolvedById by JoinRequestTable.resolvedById
    var withdrawalCause by JoinRequestTable.withdrawalCause
    var connectionId by JoinRequestTable.connectionId
    var occurredAt by JoinRequestTable.occurredAt
    var lastEventId by JoinRequestTable.lastEventId
    var acceptedAt by JoinRequestTable.acceptedAt
    var projectionVersion by JoinRequestTable.projectionVersion
    var createdAt by JoinRequestTable.createdAt
    var updatedAt by JoinRequestTable.updatedAt
}
