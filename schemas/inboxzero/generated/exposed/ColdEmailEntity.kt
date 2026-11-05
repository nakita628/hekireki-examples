package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ColdEmailEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ColdEmailEntity>(ColdEmailTable)

    var createdAt by ColdEmailTable.createdAt
    var updatedAt by ColdEmailTable.updatedAt
    var fromEmail by ColdEmailTable.fromEmail
    var messageId by ColdEmailTable.messageId
    var threadId by ColdEmailTable.threadId
    var status by ColdEmailTable.status
    var reason by ColdEmailTable.reason
    var emailAccountId by ColdEmailTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn ColdEmailTable.emailAccountId
    val digestItems by DigestItemEntity optionalReferrersOn DigestItemTable.coldEmailId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ColdEmailTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
