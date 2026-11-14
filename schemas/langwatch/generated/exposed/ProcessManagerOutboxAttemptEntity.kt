package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ProcessManagerOutboxAttemptEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProcessManagerOutboxAttemptEntity>(ProcessManagerOutboxAttemptTable)

    var outboxId by ProcessManagerOutboxAttemptTable.outboxId
    var projectId by ProcessManagerOutboxAttemptTable.projectId
    var attempt by ProcessManagerOutboxAttemptTable.attempt
    var occurredAt by ProcessManagerOutboxAttemptTable.occurredAt
    var outcome by ProcessManagerOutboxAttemptTable.outcome
    var errorType by ProcessManagerOutboxAttemptTable.errorType
    var errorMessage by ProcessManagerOutboxAttemptTable.errorMessage
    var retryAfterMs by ProcessManagerOutboxAttemptTable.retryAfterMs
}
