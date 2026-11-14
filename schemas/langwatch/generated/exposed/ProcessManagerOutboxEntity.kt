package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ProcessManagerOutboxEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProcessManagerOutboxEntity>(ProcessManagerOutboxTable)

    var processName by ProcessManagerOutboxTable.processName
    var projectId by ProcessManagerOutboxTable.projectId
    var processKey by ProcessManagerOutboxTable.processKey
    var tenantId by ProcessManagerOutboxTable.tenantId
    var userId by ProcessManagerOutboxTable.userId
    var messageKey by ProcessManagerOutboxTable.messageKey
    var intentType by ProcessManagerOutboxTable.intentType
    var payload by ProcessManagerOutboxTable.payload
    var traceCarrier by ProcessManagerOutboxTable.traceCarrier
    var sourceEventId by ProcessManagerOutboxTable.sourceEventId
    var status by ProcessManagerOutboxTable.status
    var attempts by ProcessManagerOutboxTable.attempts
    var nextAttemptAt by ProcessManagerOutboxTable.nextAttemptAt
    var leasedUntil by ProcessManagerOutboxTable.leasedUntil
    var leaseToken by ProcessManagerOutboxTable.leaseToken
    var dispatchedAt by ProcessManagerOutboxTable.dispatchedAt
    var createdAt by ProcessManagerOutboxTable.createdAt
    var updatedAt by ProcessManagerOutboxTable.updatedAt
}
