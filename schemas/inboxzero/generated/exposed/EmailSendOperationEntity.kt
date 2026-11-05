package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class EmailSendOperationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EmailSendOperationEntity>(EmailSendOperationTable)

    var createdAt by EmailSendOperationTable.createdAt
    var updatedAt by EmailSendOperationTable.updatedAt
    var clientMutationId by EmailSendOperationTable.clientMutationId
    var payloadHash by EmailSendOperationTable.payloadHash
    var status by EmailSendOperationTable.status
    var processingStartedAt by EmailSendOperationTable.processingStartedAt
    var result by EmailSendOperationTable.result
    var emailAccountId by EmailSendOperationTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn EmailSendOperationTable.emailAccountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EmailSendOperationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
