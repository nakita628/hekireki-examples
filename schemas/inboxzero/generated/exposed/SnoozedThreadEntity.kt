package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SnoozedThreadEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SnoozedThreadEntity>(SnoozedThreadTable)

    var updatedAt by SnoozedThreadTable.updatedAt
    var threadId by SnoozedThreadTable.threadId
    var scheduledFor by SnoozedThreadTable.scheduledFor
    var status by SnoozedThreadTable.status
    var clientMutationId by SnoozedThreadTable.clientMutationId
    var emailAccountId by SnoozedThreadTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn SnoozedThreadTable.emailAccountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SnoozedThreadTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
