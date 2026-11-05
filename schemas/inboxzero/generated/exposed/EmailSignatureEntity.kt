package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class EmailSignatureEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EmailSignatureEntity>(EmailSignatureTable)

    var createdAt by EmailSignatureTable.createdAt
    var updatedAt by EmailSignatureTable.updatedAt
    var name by EmailSignatureTable.name
    var html by EmailSignatureTable.html
    var emailAccountId by EmailSignatureTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn EmailSignatureTable.emailAccountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EmailSignatureTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
